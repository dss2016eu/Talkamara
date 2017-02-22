using System;
using DSS.Service;

namespace DSS
{
	public class ProjectOptions
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

		string logo;

		public string Logo {
			get {
				return logo;
			}
			set {
				logo = value;
			}
		}


		Client client;


		public ProjectOptions (Client client)
		{

			this.client = client;
		}


		public bool load(){

			var data = client.get ("configuration");

			if (!String.IsNullOrEmpty(data)) {


				var json = Newtonsoft.Json.Linq.JObject.Parse (data);

				if (!Util.IsJTokenNullOrEmpty (json ["configuration"]["site_name"])) {

					this.Name = json ["configuration"]["site_name"].ToString ();



				}
				if (!Util.IsJTokenNullOrEmpty (json ["styles"]["logo"])) {

					this.Logo = json ["styles"]["logo"].ToString ();



				}

				if (!String.IsNullOrEmpty(this.Name)) {
					return true;
				}

			}

			return false;

		}

	}
}

