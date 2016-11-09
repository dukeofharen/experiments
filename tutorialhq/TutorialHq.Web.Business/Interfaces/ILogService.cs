using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialHq.Web.Business.Interfaces
{
	public interface ILogService
	{
		void Debug(object sender, object msg);
		void Info(object sender, object msg);
		void Warn(object sender, object msg);
		void Error(object sender, object msg);
		void Fatal(object sender, object msg);
	}
}
