using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Resources;

namespace TutorialHq.Web.Exceptions
{
	public class THQConflictException : Exception
	{
		public THQConflictException(string message)
			: base(string.Format(Errors.conflictDetected, message))
		{

		}
	}
}
