using Book.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Commands
{
	public class AddLineCommand : IBookCommand
	{
		public IBookManager Manager { get; set; }

		public string CommandKey
		{
			get { return "add-line"; }
		}

		public string CommandName
		{
			get { return "Add new line to the bookkeeping"; }
		}

		public async void Execute(string[] arguments)
		{
			Console.WriteLine("Where did the transaction take place?");
			string accountName = Console.ReadLine();

			Console.WriteLine("What was the amount?");
			string amountString = Console.ReadLine();
			double amount = 0;
			double.TryParse(amountString, out amount);

			Console.WriteLine("What is the category?");
			string category = Console.ReadLine().Replace(" ", "");

			Console.WriteLine("What is the date and time of the transaction? (format: yyyy-mm-dd hh:mm)");
			string dateTimeString = Console.ReadLine();
			DateTime dateTime = DateTime.MinValue;
			DateTime.TryParseExact(dateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTime);

			Console.WriteLine("You can fill in a description here.");
			string description = Console.ReadLine();

			await this.Manager.AddLine(amount, dateTime, accountName, description, category);
		}

		public string Help()
		{
			return string.Empty;
		}
	}
}
