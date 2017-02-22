using System;
using System.Collections.Generic;

namespace DSS
{
	public class i18n
	{
		string defaultLanguage;

		Dictionary<string, AbstractStringsCatalog> catalog;

		public Dictionary<string, AbstractStringsCatalog> Catalog {
			get {
				return catalog;
			}
			set {
				catalog = value;
			}
		}

		public string DefaultLanguage {
			get {
				return defaultLanguage;
			}
			set {
				defaultLanguage = value;

				if (!this.Catalog.ContainsKey (value)) {

					throw new i18nException ("The catalog for the " + value + " language doesn't exists");

				}
				
				this.Catalog [value].SetUp ();

			}
		}


		string language;

		public string Language {
			get {
				return language;
			}
			set {
				language = value;
				if (!this.Catalog.ContainsKey (value)) {

					throw new i18nException ("The catalog for the " + value + " language doesn't exists");

				}
			}
		}	

		public i18n (string defaultLanguage, Dictionary<string, AbstractStringsCatalog> strings)
		{
			
			this.Catalog         = strings;
			this.DefaultLanguage = defaultLanguage;

			foreach (KeyValuePair<string, AbstractStringsCatalog> item in this.Catalog) {
				
				item.Value.loadCatalog ();

			}

		}




	


		public string Trans(string key){



			if (String.IsNullOrEmpty (this.Language)) {

				if (!this.catalog [this.defaultLanguage].Has (key)) {

					return key;

				} else {
					
					return this.catalog [this.defaultLanguage].Get (key);

				}

			} else if (this.catalog [this.Language].Has (key)) {

	

				return this.catalog [this.Language].Get (key);

				

			} else {

				return key;

			}



		}






	}
}

