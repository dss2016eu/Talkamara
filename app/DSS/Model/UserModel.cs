using System;
using DSS.Service;

namespace DSS
{
	public class UserModel
	{

		private Client client;

		private bool isAuth = false;

		public bool IsAuth {
			get {
				return isAuth;
			}
			set {
				isAuth = value;
			}
		}

		private string language;

		public string Language {
			get {
				return language;
			}
			set {
				language = value;
			}
		}

		public UserModel (Client client)
		{
			this.client = client;
			
		}

		public void doAuth(){
			
			var data = client.get ("auth");

			if (!String.IsNullOrEmpty(data)) {

				this.IsAuth = true;

				var json = Newtonsoft.Json.Linq.JObject.Parse (data);

				if (!Util.IsJTokenNullOrEmpty (json ["language"])) {

					this.Language = json ["language"].ToString ();


				}

			}

		}





	}
}

