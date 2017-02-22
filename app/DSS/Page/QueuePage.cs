using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using DSS.Entity;
using Autofac;
using DSS.Service;

namespace DSS
{
	public class QueuePage: ContentPage
	{

		ContentRepository repository;
		i18n translator;

		
		public QueuePage (App app)
		{



			repository = app.getContainer ().Resolve<ContentRepository> ();
			this.translator = app .getContainer ().Resolve<i18n> ();;			

	
			Title = this.translator.Trans ("QueueTitle");

			ListView list = new ListView (){
				RowHeight = 60
			};

			list.ItemsSource = this.repository.getAll();



			list.ItemTemplate = new DataTemplate(typeof(FileCell));

			list.ItemSelected += async (sender, e) => {

				Content item = (Content)e.SelectedItem;

				string optionUploadToVimeo = this.translator.Trans ("UploadToVimeo");
				string optionRemoveContent = this.translator.Trans ("RemoveContent");

				String action = await DisplayActionSheet (
					this.translator.Trans ("ChooseAnAction"), 
					this.translator.Trans ("Cancel"), 
					null,  
					optionUploadToVimeo, 
					optionRemoveContent
				);

				if(action == optionUploadToVimeo){
					await Navigation.PushModalAsync (new NavigationPage(new UploadPage(app,  "video", item.Id, true)));
				}


				if(action == optionRemoveContent){
					repository.remove(item);

					await DisplayAlert (this.translator.Trans ("Success"), this.translator.Trans ("ContentRemovedMessage"), this.translator.Trans ("Accept"));
					list.ItemsSource = this.repository.getAll();
				}





			};




			this.Content = list;
		}



	}






}

