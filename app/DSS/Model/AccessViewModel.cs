using System;
using System.ComponentModel;

namespace DSS.ViewModel
{
	public class AccessViewModel: AbstractLoadingViewModel
	{

		public new event PropertyChangedEventHandler PropertyChanged;

		string url;
		public string Url {
			get {
				return url;

			}
			set {
				if (url != value)
				{
					url = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Url"));
					}
				}
			}
		}

		string username;
		public string Username {
			get {
				return username;

			}
			set {
				if (username != value)
				{
					username = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Username"));
					}
				}
			}
		}

		string password;

		public string Password {
			get {
				return password;
			}
			set {
				if (password != value)
				{
					password = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Username"));
					}
				}
			}
		}


		public void persistData(IStorage storage){

			storage.set ("username", this.Username);
			storage.set ("password", this.Password);
			storage.set ("url", this.Url);

		}

		public void loadFromStorage(IStorage storage){
			this.Username = storage.get ("username");
			this.Url = storage.get ("url");
		}


	}
}

