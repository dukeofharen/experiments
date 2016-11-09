using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armyknife.Exceptions
{
	public class AKArgumentException : ArgumentException
	{
		public AKArgumentException(string message)
			: base(message)
		{

		}
	}
}
