using System;
using Xamarin.Forms;

namespace DSS.Entity
{
	public class Term
	{
		string name;

		public static Color SelectedColor = Color.FromHex ("dddddd");

		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		int id;

		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}

		Color color;

		public Color Color {
			get {
				return color;
			}
			set {
				color = value;
			}
		}

		public override string ToString (){
			return this.Name;
		}

		public Term ()
		{


		}
	}
}

