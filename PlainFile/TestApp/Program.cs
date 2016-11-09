using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Please fill in the root location:");
			//PlainFile.RootLocation = Console.ReadLine();
			PlainFile.RootLocation = @"C:\tmp";

			Stopwatch stopwatch = Stopwatch.StartNew();

			//for (int i = 0; i < 1000000; i++)
			//{
			//	Person person = new Person()
			//	{
			//		Id = i + 1,
			//		FirstName = "Duco",
			//		LastName = "Winterwerp",
			//		Country = "Netherlands"
			//	};
			//	PlainFile.Write(person, string.Format("person{0}", (i + 1)), "persons");
			//}

			//Person person = PlainFile.Get<Person>("person44444", "persons");

			//PlainFile.Write(12345, "int");
			//Console.WriteLine(PlainFile.GetInt("int"));

			//PlainFile.Write(1234511111111111, "long");
			//Console.WriteLine(PlainFile.GetLong("long"));

			//PlainFile.Write(12.34, "double");
			//Console.WriteLine(PlainFile.GetDouble("double"));

			//PlainFile.Write(12f, "float");
			//Console.WriteLine(PlainFile.GetFloat("float"));

			stopwatch.Stop();
			Console.WriteLine(stopwatch.ElapsedMilliseconds);

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}
	}
}
