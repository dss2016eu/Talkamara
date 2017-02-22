using System;
using Xamarin.Forms;
using System.Collections.Generic;
using DSS.Entity;
using DSS.ViewModel;
using System.Threading.Tasks;

namespace DSS
{
	public class TopicSelectionPage: ContentPage
	{
		UploadPage refererPage;

		List<Term> topic_list;
		List<Term> selected;
		TaxonomyModel model;
		ListView listView;
		i18n translator;

		public TopicSelectionPage (UploadPage referer, List<Term> selectedInModel, TaxonomyModel model, i18n translator)
		{

			this.translator = translator;

			Title = this.translator.Trans ("ChooseTopics");
			this.refererPage = referer;
			this.model 		 = model;

			this.topic_list  = new List<Term>();
			this.selected    = new List<Term>();


			listView = new ListView {
				ItemTemplate = new DataTemplate(typeof(TopicCell))
			};
			this.loadList (selectedInModel);



			listView.ItemSelected +=  (sender, e) => {


			
				Term term = (Term)e.SelectedItem;

				if( this.checkItem(term, selected) ){
					
					selected.Remove (term) ;
					term.Color = Color.Default;
				}else{
					
					selected.Add (term) ;
					term.Color = Term.SelectedColor;

				}
				listView.ItemTemplate = new DataTemplate(typeof(TopicCell));


			};



			Button confirmButton = new Button (){ 
				Text = this.translator.Trans ("Accept"),
				TextColor = Color.White,
				BackgroundColor = Color.FromRgb (70, 160, 220),
			};
			confirmButton.Clicked += (sender, e) => {

				refererPage.updateSelectedTopics(selected);
				Navigation.PopAsync ();
				selected = new List<Term>();
			};

			StackLayout layout = new StackLayout (){
				Padding = new Thickness (20, 20, 20, 20),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
			layout.Children.Add (listView);
			layout.Children.Add (confirmButton);

			Content = layout;

		}

		private async void loadList(List<Term> selectedInModel){


			await Task.Factory.StartNew ( () => {
				
				topic_list = model.getTopics ();

			});

			listView.ItemsSource = topic_list;

			foreach (Term item in topic_list) {
				if (this.checkItem (item, selectedInModel)) {
					item.Color = Term.SelectedColor;
					selected.Add (item);
				} else {
					item.Color = Color.Default;
				}
			}

		}

		private bool checkItem(Term term, List<Term>topics){

			foreach (Term item in topics) {

				if (term.Id == item.Id) {
					return true;
				}

			}

			return false;

		}



	}





	public class TopicCell:ViewCell{


		public TopicCell(){

			// Create views with bindings for displaying each property.
			Label nameLabel = new Label();
			nameLabel.SetBinding(Label.TextProperty, "Name");

			StackLayout item_layout =  new StackLayout {
				Padding = new Thickness (4, 4),
				Orientation = StackOrientation.Horizontal,
				Children = {
					new StackLayout
					{
						VerticalOptions = LayoutOptions.Center,
						Spacing = 0,
						Children =  { nameLabel }
					}
				}
			};


			item_layout.SetBinding (StackLayout.BackgroundColorProperty, "Color");

			View = item_layout;

		}

	}
}

