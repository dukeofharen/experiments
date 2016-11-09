using Book.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Commands
{
	public class GetCategoriesCommand : IBookCommand
	{
		public IBookManager Manager { get; set; }

		public string CommandKey
		{
			get { return "get-categories"; }
		}

		public string CommandName
		{
			get { return "Get categories"; }
		}

		public async void Execute(string[] arguments)
		{
			string[] categories = await this.Manager.GetCategories();
			StringBuilder builder = new StringBuilder();
			foreach (string category in categories)
			{
				builder.AppendLine(category);
			}
			Console.WriteLine(builder.ToString());
		}

		public string Help()
		{
			return string.Empty;
		}
	}
}
