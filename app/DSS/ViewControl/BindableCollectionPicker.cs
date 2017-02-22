using System;
using Xamarin.Forms;
using System.Collections;

namespace DSS.ViewControl
{
	public class BindableCollectionPicker : Picker
	{

		IList collection;

		public IList Collection {
			get {
				return collection;
			}
			set {

				collection = value;
				if (collection!=null) {
					foreach (var item in collection) {
						this.Items.Add (item.ToString ());
					}
				}

			}
		}

		public object collectionSelected;

		public object CollectionSelected {
			get {
				return (object)GetValue(CollectionSelectedProperty);
			}
			set {
				SetValue(CollectionSelectedProperty, value);
			}
		}

		public BindableCollectionPicker()
		{
			this.SelectedIndexChanged += OnSelectedIndexChanged;



		}



		public static BindableProperty CollectionSelectedProperty =
			BindableProperty.Create<BindableCollectionPicker, object>(o => o.CollectionSelected, default(object),propertyChanged: OnSelectedItemChanged);




		public object SelectedItem{get; set;}




		private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
		{
			if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
			{
				SelectedItem = null;
				CollectionSelected = null;
			}
			else
			{
				SelectedItem = Items[SelectedIndex];
				CollectionSelected = Collection [SelectedIndex];
				
			}
		}

		private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var picker = bindable as BindableCollectionPicker;
			if (newvalue != null)
			{
				picker.SelectedIndex = picker.Items.IndexOf(newvalue.ToString());

				if (picker.SelectedIndex < 0 || picker.SelectedIndex > picker.Items.Count - 1)
				{
					picker.SelectedItem = null;
					picker.CollectionSelected = null;
				}
				else
				{
					picker.SelectedItem = picker.Items[picker.SelectedIndex];
					picker.CollectionSelected = picker.Collection [picker.SelectedIndex];

				}
			}
		}

	}

}

