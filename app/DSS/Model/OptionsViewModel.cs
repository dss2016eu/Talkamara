using System;
using System.ComponentModel;
using System.Collections.Generic;
using DSS.Entity;

namespace DSS.ViewModel
{
	public class OptionsViewModel: AbstractLoadingViewModel
	{

		public new event PropertyChangedEventHandler PropertyChanged;



		Language       language;

		public Language Language {
			get {
				return language;
			}
			set {
				if (language != value)
				{
					language = value;

					OnPropertyChanged("Language");
				}
			}
		}




		List<Language> languages;

		public List<Language> Languages {
			get {
				return languages;
			}
			set {
				if (languages != value)
				{
					languages = value;

					OnPropertyChanged("Languages");
				}

			}
		}



		/*
		 * Returns the key value pair to send over HTTP  
		 */
		public List<KeyValuePair<string, string>> getValues(){


			string lang_iso = Language != null ? Language.Iso : "";

			List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>> ();
			values.Add (new KeyValuePair<string, string>("language", lang_iso));

			return values;


		}



		public OptionsViewModel(){
			this.Languages = new List<Language>();
		}

	}
}

