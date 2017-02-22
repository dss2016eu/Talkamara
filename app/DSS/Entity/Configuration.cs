using System;
using SQLite;

namespace DSS.Entity
{
	public class Configuration
	{

		[PrimaryKey]
		public string key { get; set; }
		public string value { get; set; }


		public Configuration ()
		{
		}


	}
}

