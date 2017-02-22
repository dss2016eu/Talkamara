using System;
using Xamarin.Forms;

namespace DSS
{
	public class FileCell:ViewCell{


		public FileCell(){

			// Create views with bindings for displaying each property.

			Label typeLabel = new Label(){
				BackgroundColor = Color.Transparent,
				TextColor =  Color.White,
				FontSize = 20,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			typeLabel.SetBinding(Label.TextProperty, "Letter");

			Label nameLabel = new Label(){
				FontSize = 16
			};
			nameLabel.SetBinding(Label.TextProperty, "Name");

			Label dateLabel = new Label(){
				TextColor = Color.FromRgb (70, 160, 220),
				FontSize = 10
			};
			dateLabel.SetBinding(Label.TextProperty, "Date");

			Label projectLabel = new Label(){
				TextColor = Color.FromRgb (70, 160, 220),
				FontSize = 10
			};
			projectLabel.SetBinding(Label.TextProperty, "Project");

			StackLayout item_layout =  new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(20,10,20,10),
				Children = {
					new StackLayout (){
						Padding = new Thickness(18,6,18,6),
						BackgroundColor = Color.FromRgb (70, 160, 220),
						WidthRequest = 20,
						HeightRequest = 30,
						Children = {typeLabel}
					}
					, 
					new StackLayout (){
						Padding = new Thickness(8,0,5,0),
						Children = {
							nameLabel, 
							new StackLayout (){
								Padding = new Thickness(0,0,0,0),
								Orientation = StackOrientation.Horizontal,
								Children = {dateLabel, new Label (){Text = " "} , projectLabel}
							}
						}
					} 
				}
			};

			View = item_layout;

		}

	}
}

