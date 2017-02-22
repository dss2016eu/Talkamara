using System;
using Xamarin.Forms;

namespace DSS.Entity
{
	public class Genre
	{
		string name;


		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		string id;

		public string Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}



		public override string ToString (){
			return this.Name;
		}

	
	}
}

