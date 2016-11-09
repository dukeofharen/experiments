using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialHq.Web.Exceptions
{
	public class THQNotAuthorizedException : Exception
	{
		public THQNotAuthorizedException(string message)
			: base(message)
		{

		}
	}
}
