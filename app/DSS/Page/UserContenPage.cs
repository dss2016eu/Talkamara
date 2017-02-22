using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using DSS.Entity;
using Autofac;
using DSS.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DSS.ViewModel;
using DSS.Interfaces;

namespace DSS
{
	public class UserContentPage: ContentPage
	{

		ContentRepository repository;
		i18n translator;
		UserContentViewModel model;

		int page =1;

		
		public UserContentPage (App app)
		{



			IConnection connection = app.getContainer ().Resolve<IConnection> ();

			if (!connection.isConnected ()) {
				DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("InternetRequiredTryItLater"), this.translator.Trans ("Accept"));
				app.SetPage (new NavigationPage (new HomePage (app)));
			}


			this.model = new UserContentViewModel ();

			this.repository = app.getContainer ().Resolve<ContentRepository> ();
			this.translator = app .getContainer ().Resolve<i18n> ();;
			Title = this.translator.Trans ("UserContentTitle");

			ListView list = new ListView (){
				RowHeight = 60
			};


			ObservableCollection<Content> collection = new ObservableCollection<Content>();
			list.ItemsSource = collection;

			this.loadCollection(app.getContainer ().Resolve<Client> (), collection, app);




			list.ItemTemplate = new DataTemplate(typeof(FileCell));

			list.ItemSelected += async (sender, e) => {

				Content item = (Content)e.SelectedItem;
				await Navigation.PushModalAsync (new NavigationPage(new UploadPage(app,  item.getContentType (), item.Id)));


			};

			var loadingIndicator = new ActivityIndicator(){
				IsRunning = false,
				IsEnabled = true
			};
			loadingIndicator.BindingContext = model;
			loadingIndicator.SetBinding (ActivityIndicator.IsRunningProperty, "Loading");
			loadingIndicator.SetBinding (ActivityIndicator.IsVisibleProperty, "Loading");

			Button more_button = new Button{
				Text = this.translator.Trans ("LoadMore"),
				BindingContext = model
			};

			more_button.SetBinding (Button.IsEnabledProperty, "Enabled");


			more_button.Clicked += async (sender, e) => {

				if(this.page == -1) return;

				this.page++;

				this.model.Enabled = false;
				this.model.Loading = true;

				await this.loadCollection(app.getContainer ().Resolve<Client> (), collection, app);

				this.model.Enabled = true;
				this.model.Loading = false;
			};


			this.Content = new StackLayout{
				Children = {

					list,
					loadingIndicator,
					more_button

				}
			};
		}

		private async Task loadCollection(Client client, ObservableCollection<Content> collection, App app){

			List<Content> items = null;

			try{
				items = this.repository.getAllRemote (client, this.page);
			}catch(NetworkException e){
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("InternetRequiredTryItLater"), this.translator.Trans ("Accept"));
				app.SetPage (new NavigationPage (new HomePage (app)));
			}

			if (items.Count < 1) {
				this.page = -1;
				return;
			}
				

			foreach (Content item in items) {

				collection.Add (item);

			}

		}



	}






}

