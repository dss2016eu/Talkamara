using System;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DSS.Service
{
	public class Util
	{


		public static int ConvertToUnixTimestamp(DateTime date)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			TimeSpan diff = date - origin;
			return (int) Math.Floor(diff.TotalSeconds);
		}

	
			public static bool IsJTokenNullOrEmpty( JToken token)
			{
				return (token == null) ||
					(token.Type == JTokenType.Array && !token.HasValues) ||
					(token.Type == JTokenType.Object && !token.HasValues) ||
					(token.Type == JTokenType.String && token.ToString() == String.Empty) ||
					(token.Type == JTokenType.Null);
			}


		public static string removeDuplicateExtensionInFileName(string name){


			var tmp_ext  = Path.GetExtension(name);
			var tmp_ext2 = Path.GetExtension(tmp_ext);

			if (tmp_ext2 == tmp_ext) {
				var _name = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(name));

				return _name + tmp_ext;
			}

			return name;

		}

	}
}

