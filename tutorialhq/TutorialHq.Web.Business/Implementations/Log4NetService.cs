using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Business.Interfaces;

namespace TutorialHq.Web.Business.Implementations
{
	public class Log4NetService : ILogService
	{
		public void Debug(object sender, object msg)
		{
			ILog log = this.GetLogger(sender.GetType());
			log.Debug(msg);
		}

		public void Info(object sender, object msg)
		{
			ILog log = this.GetLogger(sender.GetType());
			log.Info(msg);
		}

		public void Warn(object sender, object msg)
		{
			ILog log = this.GetLogger(sender.GetType());
			log.Warn(msg);
		}

		public void Error(object sender, object msg)
		{
			ILog log = this.GetLogger(sender.GetType());
			log.Error(msg);
		}

		public void Fatal(object sender, object msg)
		{
			ILog log = this.GetLogger(sender.GetType());
			log.Fatal(msg);
		}

		private ILog GetLogger(Type type)
		{
			return LogManager.GetLogger(type);
		}
	}
}
