using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data.Exceptions
{
	public class SOWNotFoundException : Exception
	{
		public SOWNotFoundException(string message)
			: base(string.Format("Resource not found: {0}", message))
		{

		}
	}
}
