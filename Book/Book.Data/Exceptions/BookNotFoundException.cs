using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Exceptions
{
	public class BookNotFoundException : ArgumentException
	{
		public BookNotFoundException(string message)
			: base(string.Format("Resource not found: {0}", message))
		{

		}
	}
}
