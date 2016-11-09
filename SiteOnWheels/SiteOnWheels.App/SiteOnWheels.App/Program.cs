using SiteOnWheels.App.Data;
using SiteOnWheels.App.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace siteonwheels
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch watch = new Stopwatch();
			ConsoleWriter writer = new ConsoleWriter();
			DateTime now = DateTime.Now;
			writer.Write("Batch started on '{0}'", now.ToString("yyyy-MM-dd HH:mm:ss"));
			try
			{
				watch.Start();
				if (args.Length < 2)
				{
					throw new SOWArgumentException("You should provide a path to a website.");
				}
				string location = args[0];
				string outputLocation = args[1];
				string siteObjectLocation = string.Empty;
				if (args.Length == 3)
				{
					siteObjectLocation = args[2];
				}
				Executor executor = new Executor(writer);
				executor.Execute(location, outputLocation, siteObjectLocation);
			}
			catch (SOWArgumentException e)
			{
				writer.Write(e.Message);
			}
			catch (SOWNotFoundException e)
			{
				writer.Write(e.Message);
			}
			catch (Exception e)
			{
				writer.Write(e.Message);
			}
			finally
			{
				watch.Stop();
				writer.Write("This run took {0} milliseconds", watch.Elapsed.Milliseconds);
				string filename = string.Format("log_{0}.txt", now.ToString("yyyyMMddHHmmss"));
				File.WriteAllText(filename, writer.ToString());
			}
		}
	}
}
