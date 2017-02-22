using System;

using Xamarin.Forms;
using Autofac;
using DSS.Interfaces;
using DSS.Service;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;

namespace DSS
{
	public class App : Application
	{

		IContainer container;


		public App (ContainerBuilder container)
		{

			container.RegisterType<Storage>().As<IStorage>().SingleInstance();
			container.RegisterType<ContentRepository>().SingleInstance();

			container.Register(c => new Client(c.Resolve<IStorage>())).SingleInstance();
			container.Register(c => new UserModel(c.Resolve<Client>())).SingleInstance();
			container.Register(c => new ProjectOptions(c.Resolve<Client>())).SingleInstance();


			var strings = new Dictionary<string, AbstractStringsCatalog> ();
			strings.Add ("es", new EsStringCatalog());
			strings.Add ("eu", new EuStringCatalog());
			strings.Add ("fr", new FrStringCatalog());
			strings.Add ("en", new EnStringCatalog());

			container.Register(c => new i18n("es", strings)).SingleInstance();


			container.Register(c => new TaxonomyModel(c.Resolve<Client>(), c.Resolve<IStorage>(), c.Resolve<i18n>())).SingleInstance();

			container.Register(c => new Vimeo("")).SingleInstance();










			this.container = container.Build();



			try{

				if (isAuth ()) {

					MainPage = new NavigationPage(new HomePage(this));

				} else {

					MainPage = new LoginPage(this);
				}

			}catch(NetworkException e){
				
				MainPage = new LoginPage(this);

			}


		}

	
		public bool isAuth(){


			
			Client client          = this.container.Resolve<Client> ();			
			IConnection connection = this.container.Resolve<IConnection> ();


			UserModel model     = this.container.Resolve<UserModel> ();	

			if ( String.IsNullOrEmpty (client.Username)|| String.IsNullOrEmpty (client.Password) || String.IsNullOrEmpty (client.URL) ) {
				return false;
			}

			if (connection.isConnected ()) {

				try{

					model.doAuth ();

				}catch(WebException e){

					return false;

				}


				if (model.IsAuth) {

					ProjectOptions options = this.container.Resolve<ProjectOptions> ();

					if (!options.load ()) {

						return false;
						
					}

					TaxonomyModel taxonomyModel = this.container.Resolve<TaxonomyModel> ();
					taxonomyModel.resetCache ();


					i18n translator = this.container.Resolve<i18n> ();
					try{
						translator.Language = model.Language;
					}catch(i18nException e){
						translator.Language = translator.DefaultLanguage;
					}

				}

				return model.IsAuth;

			 } else {


				//todo: redirigir a pagina sin conexión
				return false;

			 }

		}

		public void SetPage(Page page){
			MainPage = page;
		}

		public IContainer getContainer(){
			return this.container;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

