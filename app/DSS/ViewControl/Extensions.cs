using System;
using Xamarin.Forms;
using System.Reflection;

namespace DSS
{
	public static class Extensions
	{
		
		public static void Invalidate(this View view)
		{
			if (view == null)
			{
				return;
			}

			var method = typeof(View).GetMethod("InvalidateMeasure", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]{ }, null);

			method.Invoke(view, null);
		}


	}
}

