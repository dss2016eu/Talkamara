using System;
using SQLite;
using System.IO;

namespace DSS.Service
{
	abstract public class AbstractConnection
	{
		protected SQLiteConnection dbconn;

		public AbstractConnection ()
		{

			dbconn = new SQLiteConnection (getDatabasePath ());
			setUp ();

		}

		abstract public void setUp();


		public object getConnection(){
			return this.dbconn;
		}

		private string getDatabasePath(){
			var sqliteFilename = "DSSDB.sqlite";
			#if __IOS__
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			#else
			#if __ANDROID__
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			#else
			// WinPhone
			var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
			#endif
			#endif

			return path;
		}
	}
}

