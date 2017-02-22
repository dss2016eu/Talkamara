using System;
using Xamarin.Forms;
using Autofac;
using DSS.Interfaces;
using DSS.ViewModel;
using System.Threading.Tasks;
using System.Net;

namespace DSS
{
	public class LoginPage: ContentPage
	{

		IContainer container;
		App app;
		AccessViewModel model;
		i18n translator;

		public LoginPage (App app)
		{
			
			this.app       = app;
			this.container = app.getContainer ();
			this.model     = new AccessViewModel ();
			this.model.loadFromStorage (this.container.Resolve<IStorage> ());

			this.translator = this.container.Resolve<i18n> ();;

			this.setView ();




		}


		private async void doAuth(){


			Client client          = this.container.Resolve<Client> ();			
			IConnection connection = this.container.Resolve<IConnection> ();

			UserModel userModel     = this.container.Resolve<UserModel> ();	

			Uri uriResult=null;
			Uri.TryCreate (model.Url, UriKind.Absolute, out uriResult);

			if(! (uriResult !=null && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)) ){

				await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("NotAValidUrl"), this.translator.Trans ("Accept"));


				return;

			}


			if (connection.isConnected ()) {

				model.Loading = true;
				model.Enabled = false;
				await Task.Factory.StartNew (() => {

					client.Username = model.Username;
					client.Password = model.Password;



					client.URL      = model.Url;


					try{

						userModel.doAuth ();

					}catch(WebException e){

						userModel.IsAuth = false;

					}catch(NetworkException e){
						
						userModel.IsAuth = false;

					}catch(Exception e){
						userModel.IsAuth = false;
					}

				});
				model.Loading = false;
				model.Enabled = true;

				if (!userModel.IsAuth) {
					
					await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("LoginIncorrectData"), this.translator.Trans ("Accept"));
				
				} else {

					i18n translator = this.container.Resolve<i18n> ();

					try{
						translator.Language = userModel.Language;
					}catch(i18nException e){
						translator.Language = translator.DefaultLanguage;
					}

					model.persistData (this.container.Resolve<IStorage> ());

					ProjectOptions options = this.container.Resolve<ProjectOptions> ();

					if (!options.load ()) {
						await DisplayAlert (this.translator.Trans ("Error"), this.translator.Trans ("ProjectConfigLoadError"), this.translator.Trans ("Accept"));

					} else {
						app.SetPage (new NavigationPage (new HomePage (app)));
					}





				}



			} else {
				await DisplayAlert (this.translator.Trans ("Notice"), this.translator.Trans ("LoginInternetRequired"), this.translator.Trans ("Accept"));
			}
		}

		private void setView(){



	

			StackLayout logoContainer = new StackLayout (){
				Padding = new Thickness (3, 0, 3, 40),
				Children = {
					new Image (){
						Source = ImageSource.FromFile("logoh.png")
					}
				}
			};

			Entry url = new Entry(){
				Placeholder = this.translator.Trans ("LoginUrl") ,
				BindingContext = model,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			url.SetBinding (Entry.TextProperty, "Url", BindingMode.TwoWay);


			Entry username = new Entry(){
				Placeholder = this.translator.Trans ("LoginUsername") ,
				BindingContext = model,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			username.SetBinding (Entry.TextProperty, "Username", BindingMode.TwoWay);


		

			Entry password = new Entry () {

				Placeholder = this.translator.Trans ("LoginPassword"),
				IsPassword  = true,
				BindingContext = model,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			password.SetBinding (Entry.TextProperty, "Password", BindingMode.TwoWay);


			Button btn = new Button () {
				Text = this.translator.Trans ("LoginButton"),
				BackgroundColor = Color.FromRgb (70, 160, 220),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				IsEnabled = true,
				BindingContext = model
			};
			btn.SetBinding (Button.IsEnabledProperty, "Enabled", BindingMode.TwoWay);
			btn.Clicked += (sender, e) => doAuth ();


			var loadingIndicator = new ActivityIndicator(){
				IsRunning = false,
				IsEnabled = true
			};
			loadingIndicator.BindingContext = model;
			loadingIndicator.SetBinding (ActivityIndicator.IsRunningProperty, "Loading");
			loadingIndicator.SetBinding (ActivityIndicator.IsVisibleProperty, "Loading");

		
			ScrollView scrollview = new ScrollView {

				Content = new StackLayout (){
					Padding = new Thickness (20, 20, 20, 20),
					Children = {
						logoContainer,
						url,
						username,
						password,
						loadingIndicator,
						btn
					}
				}

			};


			RelativeLayout bglayout = new RelativeLayout ();

			Image bg = new Image (){
				Opacity = 0.1
			};
			bg.Source = ImageSource.FromFile("bg.jpg");
			bg.Aspect = Aspect.AspectFill;
	

			bglayout.Children.Add (bg, 
				Constraint.Constant (0), 
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => { return parent.Width; }),
				Constraint.RelativeToParent ((parent) => { return parent.Height; }));


			bglayout.Children.Add (scrollview,
				Constraint.Constant (0), 
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => { return parent.Width; }),
				Constraint.RelativeToParent ((parent) => { return parent.Height; }));




			this.Content = bglayout;
		}
	}
}

