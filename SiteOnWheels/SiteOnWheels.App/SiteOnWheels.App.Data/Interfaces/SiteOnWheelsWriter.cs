using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data.Interfaces
{
	public interface SiteOnWheelsWriter
	{
		void Write(string output);
		void Write(string format, object output);
	}
}
