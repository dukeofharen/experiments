using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolList.Business
{
	public interface IOSManager
	{
		Task<ToolList.Entities.OperatingSystem> GetOS(int osId);
		Task<ToolList.Entities.OperatingSystem[]> GetOSs();
	}
}
