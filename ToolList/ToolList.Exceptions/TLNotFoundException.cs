using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolList.Exceptions
{
	public class TLNotFoundException : Exception
	{
		public TLNotFoundException(string field)
			: base(string.Format("Not found: {0}", field))
		{

		}
	}
}
