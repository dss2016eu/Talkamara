using System;

namespace DSS
{
	public class i18nException:Exception
	{
		public i18nException ()
		{
		}

		public i18nException(string message)
			: base(message)
		{
		}
	}
}

