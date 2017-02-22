
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using System.Timers;

namespace DSS.Droid
{
	[Activity( Theme = "@style/Theme.Splash",//Indicates the theme to use for this activity
		MainLauncher = true, //Set it as boot activity
		NoHistory = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.Portrait)] //Doesn't place it in back stack	
	public class SplashScreenActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			
			base.OnCreate (bundle);


			RequestWindowFeature (WindowFeatures.NoTitle);
			SetContentView(Resource.Layout.Splash);



			Timer timer = new Timer();
			timer.Interval = 500; 
			timer.AutoReset = false; // Do not reset the timer after it's elapsed
			timer.Elapsed += (object sender, ElapsedEventArgs e) =>
			{
				StartActivity(typeof(MainActivity));
			};
			timer.Start();
			// Create your application here
		}


	}
}

