using System;
using Xamarin.Forms;
using Autofac;
using DSS.Interfaces;
using DSS.ViewModel;
using System.Threading.Tasks;
using DSS.ViewControl;
using DSS.Entity;
using System.Collections.Generic;

namespace DSS
{
	public class OptionsPage: ContentPage
	{

		IContainer container;
		App app;
		OptionsViewModel model;
		i18n translator;

		BindableCollectionPicker languagesp;

		public OptionsPage (App app)
		{
			
			this.app       = app;
			this.container = app.getContainer ();
			this.model     = new OptionsViewModel ();



			this.translator = this.container.Resolve<i18n> ();;

			this.setView ();
			this.loadData ();


			this.Title = this.translator.Trans ("Options");

		}

		private async void  loadData(){

			IConnection connection = this.container.Resolve<IConnection> ();
			if (!connection.isConnected ()) {
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("InternetRequiredTryItLater"), this.translator.Trans ("Accept"));
				app.SetPage (new NavigationPage (new HomePage (app)));
			}

			TaxonomyModel taxonomyModel = this.container.Resolve<TaxonomyModel> ();
			UserModel     userModel     = this.container.Resolve<UserModel> ();	

			model.Loading = true;
			model.Enabled = false;

			await Task.Factory.StartNew (() => {

				taxonomyModel.updateData();

			});
			model.Loading = false;
			model.Enabled = true;


			var items = taxonomyModel.getLanguages();

			foreach (var item in items) {
				if (!String.IsNullOrEmpty (item.Iso)) {
					this.model.Languages.Add (item);
				}
			}

			this.languagesp.Collection   = this.model.Languages;

			foreach (Language item in this.languagesp.Collection) {

				if (item.Iso == userModel.Language) {
					this.model.Language = item;
				}


			}


		}

		private async void Update(){


			String result = "";

			model.Loading = true;
			model.Enabled = false;

			await Task.Factory.StartNew (() => {

				Client client = this.container.Resolve<Client> ();	


				result = client.put ("account", model.getValues ());
			



			});


			model.Loading = false;
			model.Enabled = true;

			if (String.IsNullOrEmpty (result)) {
				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("UpdateErrorMessage"), this.translator.Trans ("Accept"));
			} else {

				UserModel userModel  = this.container.Resolve<UserModel> ();	
				userModel.Language   = this.model.Language.Iso;


				try{
					this.translator.Language = userModel.Language;
				}catch(i18nException e){
					this.translator.Language = this.translator.DefaultLanguage;
				}

				TaxonomyModel taxonomyModel = this.container.Resolve<TaxonomyModel> ();
				taxonomyModel.resetCache ();

				await DisplayAlert (this.translator.Trans ("Success"), this.translator.Trans ("UpdateSuccessMessage"), this.translator.Trans ("Accept"));

			}



			app.SetPage (new NavigationPage (new HomePage (app)));
		}


		private void setView(){


			StackLayout layout = new StackLayout (){
				Padding = new Thickness (20, 20, 20, 20)
			};



			this.languagesp = new BindableCollectionPicker {
				Collection     = this.model.Languages,
				BindingContext = this.model
			};
			this.languagesp.SetBinding (
				BindableCollectionPicker.CollectionSelectedProperty, 
				"Language", 
				BindingMode.TwoWay);

			layout.Children.Add (new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					new Label (){
						Text =  this.translator.Trans ("LanguageLabel")
					},
					this.languagesp
				}
			});

			Button button = new Button { 
				Text = this.translator.Trans ("Update"), 
				TextColor = Color.White,
				BackgroundColor = Color.FromRgb (70, 160, 220),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BindingContext = model
			};

			button.Clicked += (sender, e) => this.Update ();
			button.SetBinding (Button.IsEnabledProperty, "Enabled", BindingMode.TwoWay);

			var loadingIndicator = new ActivityIndicator(){
				IsRunning = false,
				IsEnabled = true
			};
			loadingIndicator.BindingContext = model;
			loadingIndicator.SetBinding (ActivityIndicator.IsRunningProperty, "Loading");
			loadingIndicator.SetBinding (ActivityIndicator.IsVisibleProperty, "Loading");

			layout.Children.Add (new StackLayout(){
				Padding = new Thickness(0, 20, 0, 0),
				Children = {
					loadingIndicator, button
				}
			});

			this.Content = layout;
	


		}
	}
}

