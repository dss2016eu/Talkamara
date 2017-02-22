using System;
using Xamarin.Forms;
using Autofac;
using DSS.ViewModel;
using Newtonsoft.Json.Linq;
using DSS.Service;
using DSS.Interfaces;
using XLabs.Forms.Controls;
using XLabs.Enums;

namespace DSS
{
	public class HomePage: ContentPage
	{
		IContainer container;
		i18n translator;
		
		public HomePage (App app)
		{
			
			this.container = app.getContainer ();
			this.translator = this.container.Resolve<i18n> ();

			ProjectOptions options = this.container.Resolve<ProjectOptions> ();

	
			this.Title = options.Name;
			this.Icon = null;

			this.setUpToolbarItems (app);

			StackLayout layout = new StackLayout (){
				Padding = new Thickness (20, 20, 20, 20)
			};





			Button btn1 = new Button () {
				Text = this.translator.Trans ("UploadImage"),
				BackgroundColor = Color.FromRgb (70, 160, 220),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			layout.Children.Add (btn1);

			btn1.Clicked +=  (sender, e) => {

				Navigation.PushModalAsync (new NavigationPage(new UploadPage(app, ContentViewModel.FILE_TYPE_IMAGE)));

			};



			Button btn2 = new Button () {
				Text = this.translator.Trans ("UploadVideo"),
				BackgroundColor = Color.FromRgb (70, 160, 220),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				IsEnabled = false
			};
			layout.Children.Add (btn2);



			btn2.Clicked +=  (sender, e) => {

				Navigation.PushModalAsync (new NavigationPage(new UploadPage(app, ContentViewModel.FILE_TYPE_VIDEO)));

			};

			Button btn3 = new Button () {
				Text = this.translator.Trans ("UploadAudio"),
				BackgroundColor = Color.FromRgb (70, 160, 220),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			layout.Children.Add (btn3);

			btn3.Clicked +=  (sender, e) => {

				Navigation.PushModalAsync (new NavigationPage(new UploadPage(app, ContentViewModel.FILE_TYPE_AUDIO)));

			};

			if (!String.IsNullOrEmpty (options.Logo)) {

				StackLayout customLogoContainer = new StackLayout () {
					Padding = new Thickness (3, 20, 3, 20),
					Children = {
						new Image () {
							Source = ImageSource.FromUri (new Uri(options.Logo))
						}
					}
				};

				layout.Children.Add (customLogoContainer);

			} 

			this.setUpContentButtons (layout, app);




				StackLayout logoContainer = new StackLayout (){
					Padding = new Thickness (3, 40, 3, 0),
					Children = {
						new Label (){
							Text = "v3.0",
						},
						new Image (){
							Source = ImageSource.FromFile("logoh.png")
						}
					}
				};
				layout.Children.Add (logoContainer);












			this.Content = new ScrollView{
				Content = new ScrollView{
					Content = layout
				}
			};
			this.setUpVimeo (btn2);

		}


		private void setUpToolbarItems(App app){
			
			this.ToolbarItems.Add ( 
				new ToolbarItem {
					Text = this.translator.Trans ("Options"),
					Order = ToolbarItemOrder.Secondary,

					Command = new Command (() => {
						Navigation.PushModalAsync (new NavigationPage(new OptionsPage (app)));
					})
				} 
			);
			this.ToolbarItems.Add ( 
				new ToolbarItem {
					Text = this.translator.Trans ("Logout"),
					Order = ToolbarItemOrder.Secondary,
					Command = new Command (() => {

						IStorage storage = this.container.Resolve<IStorage> ();
						storage.set ("password", "");

						app.SetPage (new LoginPage (app));
					})
				} 
			);

		}



		private void setUpContentButtons(StackLayout layout, App app){
			

		
			Grid grid = new Grid () {
				ColumnSpacing = 2,
				RowSpacing = 0,
				HeightRequest = 120,
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) }
				}
			};

			ImageButton queue = new ImageButton () {
				Text = this.translator.Trans ("GoToUploadQueue"),
				TextColor = Color.FromRgb (35, 35, 35),
				BackgroundColor = Color.FromHex ("#dedede"),
				FontSize = 11,
				Orientation = ImageOrientation.ImageOnTop,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions  = LayoutOptions.FillAndExpand,
				Source = new FileImageSource{ File = "queue.png" },
				ImageHeightRequest = 110,
				ImageWidthRequest = 110,

			};



			queue.Clicked += (sender, e) => Navigation.PushAsync(new QueuePage (app));

			ImageButton content = new ImageButton () {
				Text = this.translator.Trans ("GoToContent"),
				BackgroundColor = Color.FromHex ("#dedede"),
				TextColor = Color.FromRgb (35, 35, 35),
				FontSize = 11,
				Orientation = ImageOrientation.ImageOnTop,
				Source = new FileImageSource{ File = "content.png" },
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions  = LayoutOptions.FillAndExpand,
				ImageHeightRequest = 110,
				ImageWidthRequest = 110,
			};

			content.Clicked += (sender, e) => Navigation.PushAsync(new UserContentPage (app));

			grid.Children.Add (new StackLayout{
				Children = {
					content
				}
			}, 0, 0);
			grid.Children.Add (new StackLayout{
				Children = {
					queue
				}
			}, 1,0);

			layout.Children.Add(
				
				new StackLayout{
					Padding = new Thickness(0, 20),
					Children = {
						grid
					}
				}
			);

		}

		private async void setUpVimeo(Button vimeo_upload){

			IConnection connection = this.container.Resolve<IConnection> ();
			if (!connection.isConnected ()) {
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("UploadInternetRequired"), this.translator.Trans ("Accept"));
				return;
			}


			Client client = this.container.Resolve<Client> ();
			Vimeo vimeo   = this.container.Resolve<Vimeo> ();

			if (!String.IsNullOrEmpty (vimeo.getAccessToken ())) {
				vimeo_upload.IsEnabled = true;
				return;
			}

			var raw = client.get ("configuration");
			if( !String.IsNullOrEmpty (raw) ){
				JObject json = JObject.Parse (raw);
				var config = json.GetValue ("configuration");


				if (config != null) {


					if (!String.IsNullOrEmpty (config ["vimeo_api_access_token"].ToString ())) {
						
						vimeo.setAccessToken (config ["vimeo_api_access_token"].ToString ());

						vimeo_upload.IsEnabled = true;
						return;
					}


				}
			}


			vimeo_upload.IsVisible = false;
			await DisplayAlert (this.translator.Trans ("Notice"),this.translator.Trans ("UploadVimeoRequired") , this.translator.Trans ("Accept"));
			return;
		}
	}
}

