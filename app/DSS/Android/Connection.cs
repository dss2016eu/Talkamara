using System;
using DSS.Interfaces;
using Android.Net;
using Android.App;

namespace DSS.Android
{
	public class Connection:IConnection
	{

		ConnectivityManager manager;

		public Connection (Activity context)
		{


			this.manager = (ConnectivityManager)context.GetSystemService(
				Activity.ConnectivityService
			);


		}

		bool IConnection.isConnected ()
		{
			var activeConnection = manager.ActiveNetworkInfo;
			if ((activeConnection != null)  && activeConnection.IsConnected)
			{
				return true;
			}
			return false;
		}

		bool IConnection.isWifiConnected ()
		{
			var mobileState = manager.GetNetworkInfo(ConnectivityType.Wifi).GetState();
			if (mobileState == NetworkInfo.State.Connected)
			{
				return true;
			}

			return false;
		}




	}
}

