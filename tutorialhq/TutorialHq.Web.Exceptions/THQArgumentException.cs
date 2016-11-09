﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Resources;

namespace TutorialHq.Web.Exceptions
{
	public class THQArgumentException : Exception
	{
		public THQArgumentException(string message)
			: base(string.Format(Errors.argumentError, message))
		{
			
		}
	}
}
