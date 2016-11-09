using Book.Data.Interfaces;
using Book.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Commands
{
	public class DeleteLineCommand : IBookCommand
	{
		public IBookManager Manager { get; set; }

		public string CommandKey
		{
			get { return "delete-line"; }
		}

		public string CommandName
		{
			get { return "Delete name"; }
		}

		public async void Execute(string[] arguments)
		{
			if (arguments.Length == 0)
			{
				throw new ArgumentException("You have to submit an ID.");
			}

			int id = 0;
			if (!int.TryParse(arguments[0], out id))
			{
				throw new ArgumentException("The ID has to be a number.");
			}

			Line line = await this.Manager.GetLine(id);
			await this.Manager.DeleteLine(id);
			Console.WriteLine("The line is deleted successfully.");
		}

		public string Help()
		{
			throw new NotImplementedException();
		}
	}
}
