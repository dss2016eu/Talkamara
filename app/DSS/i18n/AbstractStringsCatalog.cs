using System;
using System.Collections.Generic;

namespace DSS
{
	public abstract class AbstractStringsCatalog
	{
		

		protected Dictionary<string, string> catalog;

		public abstract void loadCatalog();

		public void SetUp(){

			this.catalog = new Dictionary<string, string> ();
			
			if (this.catalog.Count==0) {
				this.loadCatalog ();
			}

		}

		public string Get (string key)
		{
			if (this.Has (key)) {
				return this.catalog [key];
			}
			return key;
		}

		public bool Has (string key)
		{
			return this.catalog.ContainsKey (key);
		}
	}
}

