using Armyknife.Exceptions;
using Armyknife.Extendability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Armyknife.Parser
{
	public class UrlEncodeExtension : ArmyknifeExtension
	{
		public string Key
		{
			get
			{
				return "url";
			}
		}

		public string Name
		{
			get
			{
				return "URL Encoder / Decoder";
			}
		}

		public string Execute(string args)
		{
			string[] parts = args.Split(new char[] { ' ' });
			if (parts.Length > 0)
			{
				List<string> partsList = parts.ToList();
				partsList.RemoveAt(0);
				string input = string.Join(" ", partsList.ToArray());
				if (parts[0] == "encode")
				{
					return WebUtility.UrlEncode(input);
				}
				else if (parts[0] == "decode")
				{
					return WebUtility.UrlDecode(input);
				}
			}
			throw new AKArgumentException("Please provide if you would like to 'encode' or 'decode' a string.");
		}

		public string Help()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("Example: url encode http://urltoencode");
			builder.AppendLine("Example: url decode http%3A%2F%2Furltodecode");
			return builder.ToString();
		}
	}
}
