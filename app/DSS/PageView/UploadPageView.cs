using System;
using Xamarin.Forms;
using System.Collections.Generic;
using DSS.Entity;
using DSS.ViewModel;
using Autofac;
using DSS.ViewControl;
using Geolocator.Plugin;
using System.Threading.Tasks;
using Geolocator.Plugin.Abstractions;
using DSS.Interfaces;
using System.IO;
using DSS.Service;
using Xamarin;
using DSS.Event;

namespace DSS.PageView
{
	public class UploadPageView
	{

		private Entry topicInput;

		private IContainer container;

		private Button locationLabel;

		private BindableCollectionPicker projectp;

		ContentViewModel model;

		ContentPage page;



		
		public UploadPageView (
			ContentPage page, 
			ContentViewModel model, 
			string type,
			IContainer container )
		{
			container = app.getContainer ();

			this.page 		  = page;
			this.model        = model;
			this.container    = container;


			model.ContentType = type;



			this.page.Content = this.createView ();






		}





		private View createView(){

			StackLayout layout = new StackLayout (){
				Padding = new Thickness (20, 20, 20, 20),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,

			};

			locationLabel = new Button () { 
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.Transparent,
				FontSize = 14
			};

			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 0, 0, 10),
				Children = {
					new Label (){ Text = "Posición (toca para actualizar)" },
					new StackLayout (){
						HorizontalOptions = LayoutOptions.FillAndExpand,
						BackgroundColor = Term.SelectedColor,
						Padding = new Thickness(2, 2, 2, 2),
						Children = {locationLabel}
					}

				}
			});


			string file_button_text = "Elegir archivo";

			switch (model.ContentType) {

			case "image":
				file_button_text = "Elegir archivo o sacar foto";
				break;

			case "video":
				file_button_text = "Elegir archivo o grabar vídeo";
				break;

			}

			Button fileButton = new Button { 
				Text = file_button_text, 
				TextColor = Color.White,
				BackgroundColor = Color.Gray,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			layout.Children.Add (fileButton);

			StackLayout fileContainer = new StackLayout ();
			layout.Children.Add (fileContainer);

			fileButton.Clicked += async (sender, e) => {

				switch (model.ContentType) {

					case "image":

						this.page.ImageFileAction (fileContainer);

					break;


				    case "video":
					
						this.page.VideoFileAction (fileButton);

					break;


					case "audio":

					IMediaFilePicker mediaPicker = this.container.Resolve<IMediaFilePicker> ();
					mediaPicker.AudioFilePicked += (object fpsender, EventArgs fpargs) => {

						MediaFilePickerEventArgs args = (MediaFilePickerEventArgs)fpargs;

						String uri = args.FileUri;
						model.File = uri;

						fileButton.Text = Path.GetFileName (uri);

					};
					mediaPicker.pickAudio ();
					
					break;
				}

			};

			projectp = new BindableCollectionPicker {Title= "Proyecto", BindingContext = model, Collection = model.Projects};
			projectp.SetBinding (BindableCollectionPicker.CollectionSelectedProperty, "Project", BindingMode.TwoWay);


			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					new Label (){ Text = "Proyecto" },
					projectp
				}
			});

			var title = new Entry { Placeholder = "Título del contenido", BindingContext = model };
			title.SetBinding (Entry.TextProperty, "Title");
			var description = new Editor {  BindingContext = model  };
			description.SetBinding (Editor.TextProperty, "Description");
			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					new Label (){ Text = "Título y descripción" },
					title,
					description
				}
			});


			topicInput = new Entry { Placeholder = "Seleccione temáticas", IsEnabled = true};
			layout.Children.Add(new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					new Label (){ Text = "Temáticas" },
					topicInput
				}
			});





			Grid route_grid = new Grid () {
				ColumnSpacing = 0,
				RowSpacing = 0,
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) }
				}
			};






			Picker route = new Picker {Title= "Ruta", BindingContext = model};

			route.Items.Add ("Tagueando la ciudad");
			Entry routePosition = new Entry { Placeholder = "Posición en ruta", Keyboard = Keyboard.Numeric, BindingContext = model };
			routePosition.SetBinding (Entry.TextProperty, "RoutePosition");


			route_grid.Children.Add (route, 0, 0);
			route_grid.Children.Add (routePosition, 1,0);



			layout.Children.Add (
				new StackLayout () {
					Padding = new Thickness(0, 20, 0, 0),
					Children = {
						new Label (){ Text = "Selección de ruta (opcional)" },
						route_grid
					}
				}
			);


			if (model.ContentType == "video") {

				Entry age = new Entry { Placeholder = "Edad de la persona", Keyboard = Keyboard.Numeric, BindingContext = model };

				layout.Children.Add (
					new StackLayout () {
						Padding = new Thickness(0, 20, 0, 0),
						Children = {
							new Label (){ Text = "Datos de entrevista (opcional)" },
							age,
							new Picker {Title= "Idioma de la persona", Items = {"Euskera", "Español", "Inglés"}},
							new Picker {Title= "Género de la persona", Items = {"Femenino", "Masculino", "Otro"}},
						}
					}
				);

			}




			Button uploadButton = new Button { 
				Text = "Subir contenido", 
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

			return new ScrollView{ 
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


	}
}

