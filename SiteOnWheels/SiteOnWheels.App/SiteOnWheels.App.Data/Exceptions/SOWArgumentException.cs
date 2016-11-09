using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data.Exceptions
{
	public class SOWArgumentException : Exception
	{
		public SOWArgumentException(string message)
			: base(message)
		{

		}
	}
}
