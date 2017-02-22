using System;
using System.ComponentModel;

namespace DSS.ViewModel
{
	abstract public class AbstractLoadingViewModel: INotifyPropertyChanged
	{




		public event PropertyChangedEventHandler PropertyChanged;

		bool loading;

		public bool Loading {
			get {
				return loading;
			}
			set {
				if (loading != value)
				{
					loading = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Loading"));
					}
				}
			}
		}

		bool enabled = true;

		public bool Enabled {
			get {
				return enabled;
			}
			set {
				if (enabled != value)
				{
					enabled = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Enabled"));
					}
				}
			}
		}


		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}


	
	}
}

