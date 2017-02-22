using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Autofac;
using DSS.Android;
using DSS.Interfaces;
using Xamarin;
using XLabs.Forms;
using XLabs.Platform.Services.Media;

namespace DSS.Droid
{
	[Activity (Label = "DSS.Droid", Icon = "@android:color/transparent", Theme = "@android:style/Theme.Holo.Light", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : XFormsApplicationDroid
	//global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{



		/*void MyApp_UnhandledExceptionHandler (object sender, RaiseThrowableEventArgs e)
		{
			// Do your error handling here.
			System.Diagnostics.Debug.WriteLine("App UnhandledExceptionHandler error: " + e.Exception.Message);
		}*/



		protected override void OnCreate (Bundle bundle)
		{



			/*Java.Lang.Thread.DefaultUncaughtExceptionHandler = new DefaultUncaughtExceptionHandler ();

			AndroidEnvironment.UnhandledExceptionRaiser += MyApp_UnhandledExceptionHandler;

            */

			base.OnCreate (bundle);

			Insights.Initialize("30b0fb7e7546250fc2aacb9d6750012f9339c8c6", this);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			ContainerBuilder builder = new ContainerBuilder();

			builder.Register(c => new Connection(this)).As<IConnection>();

			builder.Register(c => new ImageFileData(this)).As<IImageFileData>().SingleInstance ();
			builder.Register(c => new MediaFilePicker(this)).As<IMediaFilePicker>();
			builder.Register(c => new FolderManager(this)).As<IFolderManager>();

			builder.Register(c => new MediaPicker()).As<IMediaPicker>().SingleInstance ();



			LoadApplication (new App (builder));





		}

		private Action<int, Result, Intent> _activityResultCallback;

		public void ConfigureActivityResultCallback(Action<int, Result, Intent> callback)
		{
			if (callback == null) throw new ArgumentNullException("callback");
			_activityResultCallback = callback;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			

			if (_activityResultCallback != null) {
				
				_activityResultCallback.Invoke (requestCode, resultCode, data);
				_activityResultCallback = null;

			} else {
				
				base.OnActivityResult(requestCode, resultCode, data);

			}
		}
	}

	/*public class DefaultUncaughtExceptionHandler: Java.Lang.Object, Java.Lang.Thread.IUncaughtExceptionHandler
	{
		#region IUncaughtExceptionHandler implementation

		public void UncaughtException (Java.Lang.Thread thread, Java.Lang.Throwable ex)
		{
			System.Diagnostics.Debug.WriteLine("UncaughtException error: " + ex.Message);
		}

		#endregion

	}*/

}



