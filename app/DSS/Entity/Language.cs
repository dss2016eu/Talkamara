using System;

namespace DSS.Entity
{
	public class Language
	{
		string iso;

		public string Iso {
			get {
				return iso;
			}
			set {
				iso = value;
			}
		}

		string name;

		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		public override string ToString (){
			return this.Name;
		}

		public Language ()
		{
		}
	}
}

