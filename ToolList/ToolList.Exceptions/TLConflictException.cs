using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolList.Exceptions
{
	public class TLConflictException : Exception
	{
		public TLConflictException(string field)
			: base(string.Format("Conflict detected: {0}", field))
		{

		}
	}
}
