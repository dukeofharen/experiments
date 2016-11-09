using Book.Data;
using Book.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
	class Program
	{
		static void Main(string[] args)
		{
			Executor exe = new Executor(new StorageBookManager());
			while (true)
			{
				try
				{
					Console.WriteLine("Please type in a command. Type 'help' for more information.");
					string line = Console.ReadLine();
					exe.Execute(line);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
