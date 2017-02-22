using Android.Net;
using Android.Content;
using DSS.Interfaces;
using Android.App;
using DSS.Droid;
using DSS.Event;


/***
 * 
 * 
 * 
 * 		private Action<int, Result, Intent> _activityResultCallback;

		public void ConfigureActivityResultCallback(Action<int, Result, Intent> callback)
		{
			if (callback == null) throw new ArgumentNullException("callback");
			_activityResultCallback = callback;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if (_activityResultCallback != null)
			{
				_activityResultCallback.Invoke(requestCode, resultCode, data);
				_activityResultCallback = null;
			}
		}
 * 
 * 
 * 
 * 
 * */
using Android.Provider;
using Android.Database;
using Java.IO;
using Android.OS;
using DSS.ViewModel;
using Xamarin.Forms;
using DSS.Media;

namespace DSS.Android
{
	public class MediaFilePicker:IMediaFilePicker
	{
		const int FILE_PICKED = 1;

		const int FILE_TAKEN  = 1;

	

	
		public File _takenFile;

		MainActivity context;

		i18n translator;


		string contentType;

		public event System.EventHandler VideoFilePicked;

		public event System.EventHandler ImageFilePicked;

		public event System.EventHandler AudioFilePicked;

		public  void OnVideoFilePicked(MediaFilePickerEventArgs e)
		{
			System.EventHandler handler = VideoFilePicked;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		public  void OnImageFilePicked(MediaFilePickerEventArgs e)
		{
			System.EventHandler handler = ImageFilePicked;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		public  void OnAudioFilePicked(MediaFilePickerEventArgs e)
		{
			System.EventHandler handler = AudioFilePicked;
			if (handler != null)
			{
				handler(this, e);
			}
		}


		public void setTranslator(i18n translator){
			this.translator = translator;
		}


		public MediaFilePicker (MainActivity context)
		{

			this.context = context;

		}


		public void pickAudio ()
		{
			
			this.contentType = ContentViewModel.FILE_TYPE_AUDIO;

			Intent intent = new Intent (Intent.ActionSend);
			intent.SetType ("audio/*");
			intent.SetAction(Intent.ActionGetContent);


			this.context.ConfigureActivityResultCallback(SelectFileResult);
			this.context.StartActivityForResult(Intent.CreateChooser (intent, this.translator.Trans("ChooseAudioFile")), FILE_PICKED);


		}

		public void takeAudio ()
		{

			Intent intent = new Intent(MediaStore.Audio.Media.RecordSoundAction);
			this.contentType = ContentViewModel.FILE_TYPE_AUDIO;
			this.context.ConfigureActivityResultCallback(TakenFileResult);

			try{
				this.context.StartActivityForResult(intent, FILE_TAKEN);
			}catch (ActivityNotFoundException e) {
				throw new AudioRecorderNotFoundException ();
			}





			/*_takenFile = new File(folderManager.getDirectory (), System.String.Format("dss_{0}.mp3", System.Guid.NewGuid()));

			if (_takenFile != null) {

				intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_takenFile));


			}*/


		}


		/*
		 * public void pickImage ()
		{
			this.contentType = ContentViewModel.FILE_TYPE_IMAGE;

			Intent intent = new Intent (Intent.ActionSend);
			intent.SetType ("image/*");
			intent.SetAction(Intent.ActionGetContent);


			this.context.ConfigureActivityResultCallback(SelectFileResult);
			this.context.StartActivityForResult(Intent.CreateChooser (intent, "Elegir un archivo de imagen"), FILE_PICKED);


		}


		public void pickVideo ()
		{
			this.contentType = ContentViewModel.FILE_TYPE_VIDEO;

			Intent intent = new Intent (Intent.ActionSend);
			intent.SetType ("video/*");
			intent.SetAction(Intent.ActionGetContent);


			this.context.ConfigureActivityResultCallback(SelectFileResult);
			this.context.StartActivityForResult(Intent.CreateChooser (intent, "Elegir un archivo de video"), FILE_PICKED);

		}*/
			


		/*public void takeImage (IFolderManager folderManager)
		{
			this.contentType = ContentViewModel.FILE_TYPE_IMAGE;

			Intent intent = new Intent(MediaStore.ActionImageCapture);



			_takenFile = new File( folderManager.createFile (System.String.Format("dss_{0}.jpg", System.Guid.NewGuid())) );

			if (_takenFile != null) {
				
				intent.PutExtra (MediaStore.ExtraOutput, Uri.FromFile (_takenFile));
				this.context.ConfigureActivityResultCallback (this.TakenFileResult);
				this.context.StartActivityForResult (intent, FILE_TAKEN);



			} else {

				System.Diagnostics.Debug.WriteLine ("no existe el archivo");

			}


		}




		public void takeVideo (IFolderManager folderManager)
		{
			Intent intent = new Intent(MediaStore.ActionVideoCapture);

			this.contentType = ContentViewModel.FILE_TYPE_VIDEO;

			_takenFile = new File(folderManager.getDirectory (), System.String.Format("dss_{0}.mp4", System.Guid.NewGuid()));

			if (_takenFile != null) {

				intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_takenFile));
				this.context.ConfigureActivityResultCallback(TakenFileResult);
				this.context.StartActivityForResult(intent, FILE_TAKEN);



			}
		}*/
	



		private void SelectFileResult(int requestCode, Result resultCode, Intent data){

			if (requestCode == FILE_PICKED && resultCode == Result.Ok)
			{

				if (data != null) {
					Uri uri = data.Data;

					MediaFilePickerEventArgs args = new MediaFilePickerEventArgs (){
						FileUri = this.getRealPathFromURI(uri)
					};



					switch (contentType) {

					case ContentViewModel.FILE_TYPE_AUDIO:

						OnAudioFilePicked (args);

						break;

					case ContentViewModel.FILE_TYPE_VIDEO:

						OnVideoFilePicked (args);

						break;

					case ContentViewModel.FILE_TYPE_IMAGE:

						OnImageFilePicked (args);

						break;

					}




				}



			}

		}

		private void TakenFileResult(int requestCode, Result resultCode, Intent data){

			if (requestCode == FILE_TAKEN && resultCode == Result.Ok) {
				

				if (data != null) {
					Uri uri = data.Data;

					MediaFilePickerEventArgs args = new MediaFilePickerEventArgs (){
						FileUri = this.getRealPathFromURI(uri)
					};



					switch (contentType) {

					case ContentViewModel.FILE_TYPE_AUDIO:

						OnAudioFilePicked (args);

						break;

					case ContentViewModel.FILE_TYPE_VIDEO:

						OnVideoFilePicked (args);

						break;

					case ContentViewModel.FILE_TYPE_IMAGE:

						OnImageFilePicked (args);

						break;

					}




				}





				//mediaScanIntent.Dispose ();
				//mediaScanIntent = null;


			}
		}


		private System.String getRealPathFromURI(Uri contentUri) {
			
			ICursor cursor = null;

			string data = "_data";
			switch (contentType) {


			case ContentViewModel.FILE_TYPE_IMAGE:
				data = MediaStore.Images.Media.InterfaceConsts.Data;
				break;

			case ContentViewModel.FILE_TYPE_VIDEO:
				data = MediaStore.Video.Media.InterfaceConsts.Data;

				break;

			case ContentViewModel.FILE_TYPE_AUDIO:
				data = MediaStore.Audio.Media.InterfaceConsts.Data;

				break;

			default:
				break;
			}



			try { 
				System.String[] proj = { data  };
				cursor = context.ContentResolver.Query(contentUri,  proj, null, null, null);
				if(cursor!=null){
					int column_index = cursor.GetColumnIndexOrThrow(data);
					cursor.MoveToFirst();
					return cursor.GetString(column_index);

				}else{
					return contentUri.Path;
				}

			} finally {
				if (cursor != null) {
					cursor.Close ();
				}
			}
		}







	}



}

