using Book.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Commands
{
	public class QuitCommand : IBookCommand
	{
		public string CommandKey
		{
			get
			{
				return "quit";
			}
		}

		public string CommandName
		{
			get
			{
				return "Exit application";
			}
		}

		public void Execute(string[] arguments)
		{
			Environment.Exit(0);
		}

		public string Help()
		{
			return string.Empty;
		}

		public IBookManager Manager { get; set; }
	}
}
