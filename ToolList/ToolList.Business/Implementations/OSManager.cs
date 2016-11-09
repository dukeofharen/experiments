using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolList.Data;
using ToolList.Exceptions;

namespace ToolList.Business.Implementations
{
	public class OSManager : IOSManager
	{
		public Task<Entities.OperatingSystem> GetOS(int osId)
		{
			return Task.Run(() =>
			{
				using (var ctx = new TLEntities())
				{
					Entities.OperatingSystem os = ctx.OperatingSystems
												  .Where(o => o.Id == osId)
												  .FirstOrDefault();
					if (os == null)
					{
						throw new TLNotFoundException("os");
					}
					return os;
				}
			});
		}

		public Task<Entities.OperatingSystem[]> GetOSs()
		{
			return Task.Run(() =>
			{
				using (var ctx = new TLEntities())
				{
					return ctx.OperatingSystems.ToArray();
				}
			});
		}
	}
}
