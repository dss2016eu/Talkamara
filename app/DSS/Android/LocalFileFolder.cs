using System;
using System.IO;

namespace DSS.Android
{
	public class LocalFileFolder: ILocalFileFolder
	{
		#region ILocalFileFolder implementation

		string ILocalFileFolder.getDirPath (string dirname)
		{
			return Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), filename);
		}

		#endregion

		public LocalFileFolder ()
		{
		}
	}
}

