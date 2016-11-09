using Armyknife.Exceptions;
using Armyknife.Extendability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armyknife.Parser
{
	public class CommandParser
	{
		public static ArmyknifeExtension[] Extensions { get; set; }

		public CommandParser()
		{
			Type interfaceType = typeof(ArmyknifeExtension);
			Type[] types = AppDomain.CurrentDomain.GetAssemblies()
							.SelectMany(s => s.GetTypes())
							.Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
							.ToArray();

			ArmyknifeExtension[] result = new ArmyknifeExtension[types.Length];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = (ArmyknifeExtension)Activator.CreateInstance(types[i]);
			}

			Extensions = result;
		}

		public string Parse(string input)
		{
			ArmyknifeExtension extension = null;

			string[] parts = input.Split(new char[] { ' ' });
			if (parts.Length > 0)
			{
				extension = GetExtension(parts[0]);
			}
			if (extension == null)
			{
				parts = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
				if (parts.Length > 0)
				{
					extension = GetExtension(parts[0]);
				}
			}
			if (extension != null)
			{
				List<string> partsList = parts.ToList();
				partsList.RemoveAt(0);
				string command = string.Join(" ", partsList.ToArray());
				return extension.Execute(command);
			}

			throw new AKArgumentException(string.Format("The extension couldn't be found."));
		}

		private static ArmyknifeExtension GetExtension(string key)
		{
			return Extensions.Where(e => e.Key == key).FirstOrDefault();
		}
	}
}
