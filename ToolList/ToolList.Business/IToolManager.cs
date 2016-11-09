using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolList.Entities;
using ToolList.Models;

namespace ToolList.Business
{
	public interface IToolManager
	{
		Task<Tool> GetTool(int id);
		Task<Tool[]> GetTools(int categoryId = 0, ToolType? type = null, int operatingSystem = 0, License? license = null);
		Task<bool> ActivateTool(string activationCode);
		Task<Tool> AddTool(ToolSubmitModel tool);
	}
}
