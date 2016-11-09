using Armyknife.Exceptions;
using Armyknife.Extendability;
using Armyknife.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armyknife
{
	class Program
	{
		static void Main(string[] args)
		{
			CommandParser parser = new CommandParser();
			while (true)
			{
				try
				{
					Console.WriteLine("Please fill in a command:");
					string command = Console.ReadLine();
					if (command == "exit")
					{
						Environment.Exit(0);
					}
					if (command == "help")
					{
						Console.WriteLine();
						foreach (ArmyknifeExtension extension in CommandParser.Extensions)
						{
							Console.WriteLine(string.Format("Instructions for extension {0}", extension.Name));
							Console.WriteLine(extension.Help());
							Console.WriteLine();
						}
						continue;
					}
					string output = parser.Parse(command);
					Console.WriteLine(output);
				}
				catch (AKArgumentException e)
				{
					Console.WriteLine(e.Message);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
				finally
				{
					Console.WriteLine();
				}
			}
		}
	}
}
