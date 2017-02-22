using System;
using Xamarin.Forms;
using System.Collections.Generic;
using DSS.Entity;
using DSS.ViewModel;
using Autofac;
using DSS.ViewControl;
using System.Threading.Tasks;
using DSS.Interfaces;
using System.IO;
using Xamarin;
using DSS.Event;
using DSS.Android;
using Android.App;
using Android.Content;
using XLabs.Platform.Services.Media;
using DSS.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DSS.Media;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace DSS
{
	public class UploadPage: ContentPage
	{

		private Entry topicInput;

		private IContainer container;

		private Button locationLabel;

		private Button uploadButton;

		private BindableCollectionPicker projectp;

		private BindableCollectionPicker cityp;

		private BindableCollectionPicker routesp;

		private BindableCollectionPicker languagesp;

		private BindableCollectionPicker genresp;

		ContentViewModel model;

		Button fileButton;

		int localId=0;

		i18n translator;

		bool fromQueue;
	
		public UploadPage (App app, string type, int localId=0, bool fromQueue=false)
		{



			this.container 	= app.getContainer ();

			this.translator = this.container.Resolve<i18n> ();

			this.Title 		= this.translator.Trans ("UploadContent");

			this.model 		= new ContentViewModel ();

			this.localId    = localId;

			this.fromQueue = fromQueue;

			this.model.ContentType   = type;
			this.model.DisplayLayout = true;


			if (!this.isConnected ().Result) {
				app.SetPage (new NavigationPage (new HomePage (app)));
			}

			this.ToolbarItems.Add ( 
				new ToolbarItem {
					Text = this.translator.Trans ("Cancel"),
					Order = ToolbarItemOrder.Primary,
					Command = new Command (() =>  Navigation.PopModalAsync () )
				} 
			);



			this.setView (app);



			this.updateTopicView ();
		



			setLocation (locationLabel);



			try{
				
				//actualiza también el modelo
				loadTaxonomyData ();

			}catch(NetworkException e){
				DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("InternetRequiredTryItLater"), this.translator.Trans ("Accept"));
				app.SetPage (new NavigationPage (new HomePage (app)));
			}

		}




		public void updateSelectedTopics(List<Term> selected){

			model.Topics = selected;
			updateTopicView ();

		}


		public void updateTopicView(){

			topicInput.Text  = "";
			foreach (Term topic in model.Topics) {
				topicInput.Text = topicInput.Text + " " + topic.Name + ",";
			}

		}


		private async void loadTaxonomyData(){
			
			TaxonomyModel taxonomyModel = this.container.Resolve<TaxonomyModel> ();

			model.Loading = true;

			await Task.Factory.StartNew (() => {

				taxonomyModel.updateData();

			});
			model.Loading = false;


			model.Projects = taxonomyModel.getProjects ();
			model.Routes   = taxonomyModel.getRoutes ();
			model.Cities   = taxonomyModel.getCities ();

			this.projectp.Collection = model.Projects;
			this.routesp.Collection  = model.Routes;
			this.cityp.Collection    = model.Cities;

			this.cityp.SelectedIndex = model.Cities.Count - 1;

			if (model.ContentType != ContentViewModel.FILE_TYPE_IMAGE) {
				this.genresp.Collection = taxonomyModel.getGenres ();
				this.languagesp.Collection = taxonomyModel.getLanguages ();
			}

			if (this.localId != 0) {
				this.loadLocalQueuedContent ();
				updateTopicView ();
			} else {
				this.model.Topics = new List<Term>();
			}


		}

		public void loadLocalQueuedContent(){

			ContentRepository repository = this.container.Resolve<ContentRepository> ();

			Content content = null;
			if (this.fromQueue && this.model.ContentType == ContentViewModel.FILE_TYPE_VIDEO) {
				 content = repository.get (localId);
			} else {
				
				content = repository.getRemote (this.container.Resolve<Client> (), localId);
			}

			if (content != null) {


				var json_item = JsonConvert.DeserializeObject (content.JSONData, typeof( JSONContent ));
				TaxonomyModel taxonomyModel = this.container.Resolve<TaxonomyModel> ();


				model.loadFromJSON (
					(JSONContent) json_item, 
					taxonomyModel.getProjects (), 
					taxonomyModel.getTopics (),
					taxonomyModel.getLanguages (),
					taxonomyModel.getGenres (),
					taxonomyModel.getRoutes ()
				);

				fileButton.Text = Path.GetFileName (model.File);

				locationLabel.Text = model.Latitude + "  " + model.Longitude;
				fileButton.IsEnabled = false;


				if (!String.IsNullOrEmpty (((JSONContent)json_item).preview)) {
					
					model.FileSource = ((JSONContent)json_item).preview;

				}

			}

		}

		private void setView(App app){



			//LAYOUT BASE

			StackLayout layout = new StackLayout (){
				Padding = new Thickness (20, 20, 20, 20),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,

			};





			//CONTROL DE COORDENADAS Y POSICION

			locationLabel = new Button () { 
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.Transparent,
				FontSize = 14
			};
			locationLabel.Clicked += (sender, e) => this.setLocation(locationLabel);


			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 0, 0, 10),
				Children = {
					new Label (){ Text = this.translator.Trans ("LocationLabel") },
					new StackLayout (){
						HorizontalOptions = LayoutOptions.FillAndExpand,
						BackgroundColor = Term.SelectedColor,
						Padding = new Thickness(2, 2, 2, 2),
						Children = {locationLabel}
					}

				}
			});






			//CONTROL DE ELEGIR ARCHIVO

			string file_button_text = this.translator.Trans ("ChooseFile");

			switch (model.ContentType) {

			case ContentViewModel.FILE_TYPE_IMAGE:
				file_button_text = this.translator.Trans ("ChooseFileImage");
				break;

			case ContentViewModel.FILE_TYPE_VIDEO:
				file_button_text = this.translator.Trans ("ChooseFileVideo");
				break;

			case ContentViewModel.FILE_TYPE_AUDIO:
				file_button_text = this.translator.Trans ("ChooseFileAudio");
				break;

			}

			fileButton = new Button { 
				Text = file_button_text, 
				TextColor = Color.White,
				BackgroundColor = Color.Gray,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			layout.Children.Add (fileButton);






			//PREVISUALIZACIÓN DE IMAGEN
			Image imagePreview = new Image (){ VerticalOptions = LayoutOptions.FillAndExpand, BindingContext = model };
			imagePreview.SetBinding (Image.SourceProperty, "FileSource");

			StackLayout fileContainer = new StackLayout (){
				Children = {imagePreview}
			};
			layout.Children.Add (fileContainer);

			fileButton.Clicked += (sender, e) => this.selectFile(fileButton, imagePreview);






			//ELEGIR PROYECTO

			projectp = new BindableCollectionPicker {Title= this.translator.Trans ("Project"), BindingContext = model, Collection = model.Projects};
			projectp.SetBinding (BindableCollectionPicker.CollectionSelectedProperty, "Project", BindingMode.TwoWay);
		

			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					new Label (){ Text = this.translator.Trans ("Project") },
					projectp
				}
			});





			//TITULO Y DESCRIPCION DEL CONTENIDO

			var title = new Entry { Placeholder = this.translator.Trans ("ContentTitle"), BindingContext = model };
			title.SetBinding (Entry.TextProperty, "Title", BindingMode.TwoWay);
			var description = new Editor {  
				BindingContext = model, 
				MinimumHeightRequest = 100,
				VerticalOptions = LayoutOptions.FillAndExpand  };
			description.SetBinding (Editor.TextProperty, "Description", BindingMode.TwoWay);


			//ViewControl/Extensions.cs
			description.TextChanged += (sender, e) =>  description.Invalidate();

			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					new Label (){ Text = this.translator.Trans ("ContentTitleDescription") },
					title,
					description
				}
			});


			//ELEGIR CIUDAD

			cityp = new BindableCollectionPicker {Title= this.translator.Trans ("City"), BindingContext = model, Collection = model.Projects};
			cityp.SetBinding (BindableCollectionPicker.CollectionSelectedProperty, "City", BindingMode.TwoWay);


			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					new Label (){ Text = this.translator.Trans ("City") },
					cityp
				}
			});


			//CONTROL DE TEMÁTICAS

			topicInput = new Entry { Placeholder = this.translator.Trans ("SelectTopics"), IsEnabled = true};
			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					new Label (){ Text = this.translator.Trans ("Topics") },
					topicInput
				}
			});

			topicInput.Focused += (sender, e) => {
				topicInput.Unfocus ();
				Navigation.PushAsync (new TopicSelectionPage (this, model.Topics, this.container.Resolve<TaxonomyModel> (), translator));
			};




			//CONTROL DE RUTAS

			Grid route_grid = new Grid () {
				ColumnSpacing = 0,
				RowSpacing = 0,
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) }
				}
			};

			routesp = new BindableCollectionPicker {Title= this.translator.Trans ("Route"), BindingContext = model, Collection = model.Routes};
			routesp.SetBinding (BindableCollectionPicker.CollectionSelectedProperty, "Route", BindingMode.TwoWay);

			Entry routePosition = new Entry { Placeholder = this.translator.Trans ("RoutePosition"), Keyboard = Keyboard.Numeric, BindingContext = model };
			routePosition.SetBinding (Entry.TextProperty, "RoutePosition", BindingMode.TwoWay);


			route_grid.Children.Add (new StackLayout{
				Children = {
					new Label (){
						Text =this.translator.Trans ("Route"),
						TextColor = Color.FromHex ("888888")
					},
					routesp
				}
			}, 0, 0);
			route_grid.Children.Add (new StackLayout{
				Children = {
					new Label (){
						Text = this.translator.Trans ("Position"),
						TextColor = Color.FromHex ("888888")
					},
					routePosition
				}
			}, 1,0);

			layout.Children.Add (
				new StackLayout () {
					Padding = new Thickness(0, 20, 0, 0),
					Children = {
						new Label (){ Text = this.translator.Trans ("RouteSelection") },
						route_grid
					}
				}
			);








			//CONTROLES DE ENTREVISTA

			if (model.ContentType != ContentViewModel.FILE_TYPE_IMAGE) {

				Entry age = new Entry { Placeholder = this.translator.Trans ("PersonAge"), Keyboard = Keyboard.Numeric, BindingContext = model };
				age.SetBinding (Entry.TextProperty, "Age", BindingMode.TwoWay);

				genresp = new BindableCollectionPicker { Title = this.translator.Trans ("PersonGenre") , BindingContext = model, Collection = new List<Term>()};
				genresp.SetBinding (BindableCollectionPicker.CollectionSelectedProperty, "Genre", BindingMode.TwoWay);
				languagesp = new BindableCollectionPicker { Title = this.translator.Trans ("PersonLanguage"), BindingContext = model, Collection = new List<Language>() };
				languagesp.SetBinding (BindableCollectionPicker.CollectionSelectedProperty, "Language", BindingMode.TwoWay);

				layout.Children.Add (
					new StackLayout () {
						Padding = new Thickness(0, 20, 0, 0),
						Children = {
							new Label (){ Text = this.translator.Trans ("InterviewData") },
							new StackLayout{
								Padding = new Thickness(0, 10, 0, 0),
								Children = {
									new Label (){
										Text = this.translator.Trans ("PersonAge"),
										TextColor = Color.FromHex ("888888")
									},
									age
								}
							},
							new StackLayout{
								Padding = new Thickness(0, 10, 0, 0),
								Children = {
									new Label (){
										Text = this.translator.Trans ("PersonLanguage"),
										TextColor = Color.FromHex ("888888")
									},
									languagesp
								}
							}
							,
							new StackLayout{
								Padding = new Thickness(0, 10, 0, 0),
								Children = {
									new Label (){
										Text = this.translator.Trans ("PersonGenre"),
										TextColor = Color.FromHex ("888888")
									},
									genresp
								}
							},
						}
					}
				);

			}







			//BOTÓN DE SUBIDA

			uploadButton = new Button { 
				Text = this.translator.Trans ("UploadContent"), 
				TextColor = Color.White,
				BackgroundColor = Color.FromRgb (70, 160, 220),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BindingContext = model
			};

			uploadButton.Clicked += (sender, e) => this.upload(app);
			uploadButton.SetBinding (Button.IsEnabledProperty, "Enabled", BindingMode.TwoWay);


			var loadingIndicator = new ActivityIndicator(){
				IsRunning = false,
				IsEnabled = true
			};
			loadingIndicator.BindingContext = model;
			loadingIndicator.SetBinding (ActivityIndicator.IsRunningProperty, "Loading");
			loadingIndicator.SetBinding (ActivityIndicator.IsVisibleProperty, "Loading");


			layout.BindingContext = model;
			layout.SetBinding (StackLayout.IsVisibleProperty, "DisplayLayout", BindingMode.TwoWay);



			this.Content =  new ScrollView{ 
				Content = new StackLayout(){
					Children = {
						layout,
						new StackLayout (){
							Padding = new Thickness (20, 20, 20, 20),
							Children = {loadingIndicator, uploadButton }
						}
					}
				}
			};

		}


		private async Task<bool> isConnected(){

			IConnection connection = this.container.Resolve<IConnection> ();

			if (!connection.isConnected ()) {
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("InternetRequiredTryItLater"), this.translator.Trans ("Accept"));
				return false;
			}

			return true;

		}


		public async void upload(App app){



			IConnection connection = this.container.Resolve<IConnection> ();

			if (!await this.isConnected ()) {
				return;
			}


			if (String.IsNullOrEmpty (model.Title)) {
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("ContentTitleRequired"), this.translator.Trans ("Accept"));
				return;
			}

			if (String.IsNullOrEmpty (model.File)) {
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("ContentFileRequired"), this.translator.Trans ("Accept"));
				return;
			}

			if (model.Project == null) {
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("ContentProjectRequired"), this.translator.Trans ("Accept"));
				return;
			}

			model.DisplayLayout = false;

			uploadButton.Text = this.translator.Trans ("ContentUploading");


			if (model.ContentType != ContentViewModel.FILE_TYPE_VIDEO || (this.localId >0 && !this.fromQueue)) {

				 

				try{

					this.uploadFileToServer(app);

				}catch(NetworkException e){
					DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("InternetRequiredTryItLater"), this.translator.Trans ("Accept"));
					app.SetPage (new NavigationPage (new HomePage (app)));
				}

			} else if (!connection.isWifiConnected ()) {


				model.Loading = true;
				model.Enabled = false;


				await Task.Factory.StartNew (() => {

					ContentRepository repository = this.container.Resolve<ContentRepository> ();

					Content item = new Content ();

					item.Name = model.Title;
					item.Project = model.Project.Name;
					item.Letter = "V";
					item.Date = DateTime.Now.ToString ("dd-MM-yyyy HH:mm");
					item.JSONData = model.getJSONString ();

					item.Id = localId;

					repository.set (item);

				});
				model.Loading = false;
				model.Enabled = true;


				await DisplayAlert (this.translator.Trans ("FileQueued"), this.translator.Trans ("FileQueuedMessage"), this.translator.Trans ("Accept"));
				app.SetPage (new NavigationPage (new HomePage (app)));
				await Navigation.PushAsync (new QueuePage (app));




			} 
			else 
			{

				model.Loading = true;
				model.Enabled = false;


				var vimeo_error = true;


				await Task.Factory.StartNew (() => {
					
					ContentRepository repository = this.container.Resolve<ContentRepository> ();

					Content item = repository.get (localId);


					Vimeo vimeo = this.container.Resolve<Vimeo> ();
					Video video =  null;

					try{

						video = vimeo.Upload( model.File ).Result;

					}catch(NetworkException e){

					}

					if(video!=null){
						
						model.ContentUrl =  video.Link;
						model.ThumbUrl   =  video.Thumb;

						model.File = null;

						if(item != null){
							repository.remove (item);
						}

						vimeo_error = false;

						
					}


				});
				model.Loading = false;
				model.Enabled = true;

				if (vimeo_error) {


					await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("VimeoErrorMessage"), this.translator.Trans ("Accept"));

					app.SetPage (new NavigationPage (new HomePage (app)));

				} else {


					try{

						this.uploadFileToServer(app);

					}catch(NetworkException e){
						DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("InternetRequiredTryItLater"), this.translator.Trans ("Accept"));
						app.SetPage (new NavigationPage (new HomePage (app)));
					}

				}







			}



		}

		private async void uploadFileToServer(App app){

			model.Loading = true;
			model.Enabled = false;

			string result = null;
			bool network_error = false;

			await Task.Factory.StartNew (() => {

				Client client = this.container.Resolve<Client> ();	

				try{

					if( this.localId > 0  && !this.fromQueue ){
						result = client.put ("contents/" + this.localId.ToString () + "/edit", model.getValues ());
					}else{
						result = client.post ("contents", model.getValues (), model.File);
					}
					
				}catch(NetworkException e){
					network_error = true;
				}





			});
			model.Loading = false;
			model.Enabled = true;


			if (!network_error) {

				if (String.IsNullOrEmpty (result)) {
					uploadButton.Text = this.translator.Trans ("UploadContentError");
					await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("UploadContentErrorMessage"), this.translator.Trans ("Accept"));
				} else {
					uploadButton.Text = this.translator.Trans ("ContentUploaded");
					await DisplayAlert (this.translator.Trans ("Success"), this.translator.Trans ("ContentUploadedMessage"), this.translator.Trans ("Accept"));

				}

			} else {
				
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("InternetRequiredTryItLater"), this.translator.Trans ("Accept"));

			}




			app.SetPage (new NavigationPage (new HomePage (app)));

		}



		private async void selectFile(Button fileButton, Image imagePreview){

			switch (model.ContentType) {

			case ContentViewModel.FILE_TYPE_IMAGE:

				this.ImageFileAction (imagePreview);

				break;


			case ContentViewModel.FILE_TYPE_VIDEO:

			    this.VideoFileAction (fileButton);

				break;


			case ContentViewModel.FILE_TYPE_AUDIO:


				IMediaFilePicker mediaPicker = this.container.Resolve<IMediaFilePicker> ();
				mediaPicker.setTranslator (this.translator);

				String action = await DisplayActionSheet (
					this.translator.Trans ("AttachAudio"), 
					this.translator.Trans ("Cancel"), 
					null,  
					this.translator.Trans ("ChooseFromGalery"), 
					this.translator.Trans ("Record"));



				if(action == this.translator.Trans ("Record")){


					mediaPicker.AudioFilePicked += (object fpsender, EventArgs fpargs) => {

						MediaFilePickerEventArgs args = (MediaFilePickerEventArgs)fpargs;

						String uri = args.FileUri;
						model.File = uri;

						fileButton.Text = Util.removeDuplicateExtensionInFileName (Path.GetFileName (uri));

					};

					try{
						mediaPicker.takeAudio ();
					}catch(AudioRecorderNotFoundException e){
						await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("AudioRecorderNotFound"), this.translator.Trans ("Accept"));
					}





				}else if (action == this.translator.Trans ("ChooseFromGalery")){

					mediaPicker.AudioFilePicked += (object fpsender, EventArgs fpargs) => {

						MediaFilePickerEventArgs args = (MediaFilePickerEventArgs)fpargs;

						String uri = args.FileUri;
						model.File = uri;

						fileButton.Text = Util.removeDuplicateExtensionInFileName (Path.GetFileName (uri));

					};
					mediaPicker.pickAudio ();


				}

				break;
			}

		}



		private async void ImageFileAction(Image imageControl){
			
		

			String action = await DisplayActionSheet (
				this.translator.Trans ("AttachImage"), 
				this.translator.Trans ("Cancel"), 
				null,  
				this.translator.Trans ("ChooseFromGalery"), 
				this.translator.Trans ("TakeWithCam"));



			if(action == this.translator.Trans ("TakeWithCam")){

				imageControl.Source = null;



				MediaFile file = await this.TakePicture ();

				if (file != null) {

					model.File = file.Path;

					try{

						IImageFileData fileDataService = this.container.Resolve<IImageFileData> ();
						model.File = fileDataService.rotateByMetadata (model.File);
						imageControl.Source = ImageSource.FromFile (model.File);
						this.fileButton.Text = Util.removeDuplicateExtensionInFileName (Path.GetFileName (model.File));

					}catch(Exception e){
						imageControl.Source = ImageSource.FromFile (model.File); 
					}

				}



			}else if (action == this.translator.Trans ("ChooseFromGalery")){

				imageControl.Source = null;

				MediaFile file = await this.PickPicture ();

				if (file != null) {
					model.File = file.Path;

					try{

						IImageFileData fileDataService = this.container.Resolve<IImageFileData> ();
						model.File = fileDataService.rotateByMetadata (model.File, true);
						imageControl.Source = ImageSource.FromFile (model.File);
						this.fileButton.Text = Util.removeDuplicateExtensionInFileName (Path.GetFileName (model.File));

					}catch(Exception e){

						imageControl.Source = ImageSource.FromFile (model.File); 
					}

				}


			}
		}

		private async Task<MediaFile> TakePicture ()
		{
			IMediaPicker _mediaPicker = this.container.Resolve<IMediaPicker> ();

			var options = new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 };

			return await _mediaPicker.TakePhotoAsync(options).ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						var s = t.Exception.InnerException.ToString();
					}
					else if (t.IsCanceled)
					{
						return null;
					}
					else
					{
						MediaFile mediaFile = t.Result;

						return mediaFile;
					}

					return null;
				});
		}


		private async Task<MediaFile> PickPicture ()
		{

			IMediaPicker _mediaPicker = this.container.Resolve<IMediaPicker> ();
		
			try {
				
				return await _mediaPicker.SelectPhotoAsync (new CameraMediaStorageOptions());



			} catch (System.Exception ex) {
				return null;
			}
		}


		private async void VideoFileAction(Button videoButton){



			String action = await DisplayActionSheet (
				this.translator.Trans ("AttachVideo"), 
				this.translator.Trans ("Cancel"), 
				null,  
				this.translator.Trans ("ChooseFromGalery"), 
				this.translator.Trans ("RecordWithCam"));


			MediaFile file = null;

			if(action == this.translator.Trans ("RecordWithCam")){


				 file = await this.TakeVideo ();

			}else if (action == this.translator.Trans ("ChooseFromGalery")){

				 file = await this.PickVideo ();

			}

			if (file != null) {

				model.File = file.Path;

				file.Dispose ();
				file = null;

				videoButton.Text = Util.removeDuplicateExtensionInFileName (Path.GetFileName (model.File));


			}

		}


		private async Task<MediaFile> TakeVideo ()
		{
			IMediaPicker _mediaPicker = this.container.Resolve<IMediaPicker> ();

			var options = new VideoMediaStorageOptions () { DefaultCamera = CameraDevice.Front };

			return await _mediaPicker.TakeVideoAsync(options).ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						var s = t.Exception.InnerException.ToString();
					}
					else if (t.IsCanceled)
					{
						var canceled = true;
					}
					else
					{
						MediaFile mediaFile = t.Result;

						return mediaFile;
					}

					return null;
				});
		}


		private async Task<MediaFile> PickVideo ()
		{

			IMediaPicker _mediaPicker = this.container.Resolve<IMediaPicker> ();

			try {

				return await _mediaPicker.SelectVideoAsync (new VideoMediaStorageOptions ());


			} catch (System.Exception ex) {
				return null;
			}
		}



		public async void  setLocation(Button locationLabel){

			IGeolocator locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			if (!locator.IsGeolocationEnabled) {
				
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("GeolocationDeactivatedMessage"), this.translator.Trans ("Accept"));


			} else {



				Position position = null;

					if(!locator.IsListening){
					  await locator.StartListeningAsync (10000, 1000);
					}

					position = await locator.GetPositionAsync ();



					await locator.StopListeningAsync ();
					locator = null;
			

				if (position != null) {

					locationLabel.Text = position.Latitude + "  " + position.Longitude;

					model.Longitude = position.Longitude.ToString ();
					model.Latitude  = position.Latitude.ToString ();


				} else {
					locationLabel.Text = this.translator.Trans ("CantDeterminePosition");
				}

			}



		}




	}
}

