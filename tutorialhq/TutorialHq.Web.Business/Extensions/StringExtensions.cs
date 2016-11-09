using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TutorialHq.Web.Business.Extensions
{
	public static class StringExtensions
	{
		public static bool IsValidEmail(this string email)
		{
			try
			{
				var addr = new MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}
	}
}