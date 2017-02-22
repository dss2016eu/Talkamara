using System;
using DSS.Entity;
using SQLite;
using System.IO;

namespace DSS
{
	public class Storage:IStorage
	{

		const string TTL_CACHE_SUFIX = "_cache_ttl";

		SQLiteConnection dbconn;

		public Storage ()
		{



			dbconn = new SQLiteConnection (getDatabasePath ());

			dbconn.CreateTable<Configuration> ();



		}

		void checkCache(string key, Configuration config=null){
			
			string cache_key     = key + TTL_CACHE_SUFIX;

			if (has (cache_key, true) && config != null) {

				DateTime cacheExpires = Convert.ToDateTime (get (cache_key));
				if (DateTime.Compare (DateTime.Now, cacheExpires) < 1) {

					this.dbconn.Delete (config);
					config = null;

				}
			}
		}

		public string get(string key){
			
			Configuration config = dbconn.Find<Configuration> (key);
			this.checkCache (key, config);

			return config != null ? config.value : null;

		}

		public bool has(string key, bool isCacheKey = false){

			Configuration config = dbconn.Find<Configuration> (key);

			if (!isCacheKey) {
				this.checkCache (key, config);
			}


			return (config != null);

		}

		public void remove(string key){

			if (has (key)) {

				dbconn.Delete (dbconn.Find<Configuration> (key));

			}

		}


		public Storage set(string key, string value, TimeSpan? ttl=null){




			if ( has (key) ) {
				
				dbconn.Update (new Configuration () {

					key = key,
					value = value

				});

			} else {
				
				dbconn.Insert (new Configuration () {


					key = key,
					value = value

				});

			}

			if (ttl!=null) {


				string cache_key     = key + TTL_CACHE_SUFIX;
				DateTime? cacheValue  = DateTime.Now + ttl;




				if ( has (cache_key) ) {
					dbconn.Update (new Configuration () {

						key = cache_key,
						value = cacheValue.ToString ()

					});
				} else {
					dbconn.Insert (new Configuration () {


						key = cache_key,
						value = cacheValue.ToString ()

					});
				}

			}


			

			return this;

		}






		private string getDatabasePath(){
			var sqliteFilename = "DSSDB16.sqlite";
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

	public interface IStorage{
		
		string  get (string key);

		void  remove (string key);

		Storage set (string key, string value, TimeSpan? ttl=null);

	
	}




}

