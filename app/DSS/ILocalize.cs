using System;
using System.Globalization;

namespace DSS
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo ();

		void SetLocale ();
	}
}

