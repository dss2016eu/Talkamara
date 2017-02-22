
using Java.IO;
using DSS.Droid;
using Android.OS;
using DSS.Interfaces;

namespace DSS.Android
{
	public class FolderManager:IFolderManager

	{


		const string FOLDER_NAME = "dss";

		MainActivity context;


		public FolderManager (MainActivity context)
		{

			this.context = context;
		}


		/* Checks if external storage is available for read and write */
		public bool isExternalStorageWritable() {
			
			System.String state = Environment.ExternalStorageState;
			if (Environment.MediaMounted.Equals(state)) {
				return true;
			}
			return false;
		}

		/* Checks if external storage is available to at least read */
		public bool isExternalStorageReadable() {
			System.String state = Environment.ExternalStorageState;
			if (Environment.MediaMounted.Equals(state) ||
				Environment.MediaMountedReadOnly.Equals(state)) {
				return true;
			}
			return false;
		}


		public string getDirectory(){


			File folder = null;

			if (isExternalStorageWritable ()) {

				folder = Environment.GetExternalStoragePublicDirectory (Environment.DirectoryPictures);

			} else {

			    folder = this.context.FilesDir;
			}

			folder = new File (folder, FOLDER_NAME);

			folder.Mkdirs ();


			return folder.AbsolutePath;


		}

		public string createFile(string filename){


			File file = new File(getDirectory (), filename);



			return file.AbsolutePath;


		}


	}
}

