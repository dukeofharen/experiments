using Book.Data.Interfaces;
using Book.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Commands
{
	public class ReportCommand : IBookCommand
	{
		public IBookManager Manager { get; set; }

		public string CommandKey
		{
			get { return "report"; }
		}

		public string CommandName
		{
			get { return "Report"; }
		}

		public async void Execute(string[] arguments)
		{
			DateTime? fromNull = null;
			DateTime? toNull = null;

			if (arguments.Length > 0)
			{
				if (arguments[0] == "today")
				{
					fromNull = DateTime.Now.Date;
					toNull = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
				}
				else if (arguments[0] == "yesterday")
				{
					fromNull = DateTime.Now.AddDays(-1).Date;
					toNull = DateTime.Now.Date.AddSeconds(-1);
				}
				else
				{
					DateTime from = DateTime.MinValue;
					if (DateTime.TryParseExact(arguments[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out from))
					{
						fromNull = from;
					}

					if (arguments.Length == 2)
					{
						DateTime to = DateTime.MinValue;
						if (DateTime.TryParseExact(arguments[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out to))
						{
							toNull = to.AddDays(1).AddSeconds(-1);
						}
					}
				}
			}

			StringBuilder builder = new StringBuilder();
			Line[] lines = await this.Manager.GetLines(fromNull, toNull);

			double total = 0;
			Dictionary<string, double> categoryTotals = new Dictionary<string, double>();
			foreach (Line line in lines)
			{
				builder.AppendFormat("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}\r\n\r\n", line.Id, line.DateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), line.AccountName, line.Amount, line.Category, line.Description);
				total += line.Amount;
				if (!categoryTotals.ContainsKey(line.Category))
				{
					categoryTotals[line.Category] = line.Amount;
				}
				else
				{
					categoryTotals[line.Category] = categoryTotals[line.Category] + line.Amount;
				}
			}

			builder.AppendLine("Category totals:\r\n");

			foreach (KeyValuePair<string, double> pair in categoryTotals)
			{
				builder.AppendFormat("{0}\t{1}\r\n", pair.Key, pair.Value);
			}

			builder.AppendFormat("\r\nTotal amount: {0}\r\n", total);
			Console.WriteLine(builder.ToString());
			if (arguments.Length > 0 && arguments[arguments.Length - 1] == "to-file")
			{
				string path = Path.Combine(Executor.BookPath, "reports");
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				string filepath = Path.Combine(path, string.Format("report_{0}.txt", DateTime.Now.ToString("yyyyMMddHHmmss")));
				File.WriteAllText(filepath, builder.ToString());
				Console.WriteLine("The report has been written to file '{0}'", filepath);
			}
		}

		public string Help()
		{
			return string.Empty;
		}

		private string HtmlReport(Line[] lines)
		{
			StringBuilder builder = new StringBuilder();

			return builder.ToString();
		}
	}
}
