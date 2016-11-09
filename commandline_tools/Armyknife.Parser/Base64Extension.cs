using Armyknife.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armyknife.Extendability
{
	public class Base64Extension : ArmyknifeExtension
	{
		public string Key
		{
			get
			{
				return "base64";
			}
		}

		public string Name
		{
			get
			{
				return "Base64 encoder / decoder";
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
					return ToBase64(input);
				}
				else if (parts[0] == "decode")
				{
					return FromBase64(input);
				}
			}
			throw new AKArgumentException("Please provide if you would like to 'encode' or 'decode' a string.");
		}

		public string Help()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("Example: base64 encode stringtoencode");
			builder.AppendLine("Example: base64 decode stringtodecode");
			return builder.ToString();
		}

		private string ToBase64(string input)
		{
			byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
			string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
			return returnValue;
		}

		private string FromBase64(string input)
		{
			byte[] encodedDataAsBytes = System.Convert.FromBase64String(input);
			string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
			return returnValue;
		}
	}
}
