using SiteOnWheels.App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siteonwheels
{
	public class ConsoleWriter : SiteOnWheelsWriter
	{
		private StringBuilder _output;

		public ConsoleWriter()
		{
			this._output = new StringBuilder();
		}

		public void Write(string output)
		{
			Console.WriteLine(output);
			this._output.AppendLine(output);
		}

		public void Write(string format, object output)
		{
			Console.WriteLine(format, output);
			this._output.AppendFormat(format, output);
			this._output.AppendLine();
		}

		public override string ToString()
		{
			return this._output.ToString();
		}
	}
}
