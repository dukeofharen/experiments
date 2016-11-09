using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Interfaces
{
	public interface IBookCommand
	{
		IBookManager Manager { get; set; }
		string CommandKey { get; }
		string CommandName { get; }
		void Execute(string[] arguments);
		string Help();
	}
}
