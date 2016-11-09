using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ToolList.Business;
using ToolList.Entities;
using ToolList.Models;

namespace ToolList.Controllers
{
	public class ToolController : ApiController
	{
		private IToolManager _toolManager;

		public ToolController(IToolManager toolManager)
		{
			this._toolManager = toolManager;
		}

		[Route("api/tools/{toolId:int}")]
		[HttpGet]
		public async Task<ToolModel> GetTool([FromUri]int toolId)
		{
			Tool tool = await this._toolManager.GetTool(toolId);
			ToolModel model = tool.ConvertTool();
			return model;
		}

		[Route("api/tools/{activationCode}/activate")]
		[HttpGet]
		public async Task<string> ActivateTool([FromUri]string activationCode)
		{
			bool valid = await this._toolManager.ActivateTool(activationCode);
			if (valid)
			{
				return "Tool activated successfully.";
			}
			else
			{
				return "Tool not found";
			}
		}

		[Route("api/tools")]
		[HttpGet]
		public async Task<ToolModel[]> GetTools([FromUri]int categoryId = 0, [FromUri]int type = 0, [FromUri]int os = 0, [FromUri]int license = 0)
		{
			ToolType? toolEnum = (ToolType)type;
			if (toolEnum == 0)
			{
				toolEnum = null;
			}
			License? licenseEnum = (License)license;
			if (licenseEnum == 0)
			{
				licenseEnum = null;
			}
			Tool[] tools = await this._toolManager.GetTools(categoryId, toolEnum, os, licenseEnum);
			ToolModel[] models = tools.ConvertTools();
			return models;
		}

		[Route("api/tools")]
		[HttpPost]
		public async Task AddTool(ToolSubmitModel tool)
		{
			Tool toolEntity = await this._toolManager.AddTool(tool);
		}

		[Route("api/types")]
		[HttpGet]
		public TypeModel[] GetTypes()
		{
			var types = Enum.GetValues(typeof(ToolType));
			TypeModel[] models = new TypeModel[types.Length];
			int i = 0;
			foreach (int value in types)
			{
				models[i] = new TypeModel()
				{
					Id = value,
					Name = Enum.GetName(typeof(ToolType), value)
				};
				i++;
			}
			return models;
		}

		[Route("api/licenses")]
		[HttpGet]
		public LicenseModel[] GetLicenses()
		{
			var licenses = Enum.GetValues(typeof(License));
			LicenseModel[] models = new LicenseModel[licenses.Length];
			int i = 0;
			foreach (int value in licenses)
			{
				models[i] = new LicenseModel()
				{
					Id = value,
					Name = Enum.GetName(typeof(License), value)
				};
				i++;
			}
			return models;
		}
	}
}
