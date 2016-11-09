using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ToolList.Business;
using ToolList.Models;

namespace ToolList.Controllers
{
	[RoutePrefix("api/os")]
    public class OSController : ApiController
    {
		private IOSManager _osManager;

		public OSController(IOSManager osManager)
		{
			this._osManager = osManager;
		}

		[Route("{osId:int}")]
		[HttpGet]
		public async Task<OSModel> GetOS([FromUri]int osId)
		{
			Entities.OperatingSystem os = await this._osManager.GetOS(osId);
			OSModel model = Mapper.Map<OSModel>(os);
			return model;
		}

		[Route("")]
		[HttpGet]
		public async Task<OSModel[]> GetOSs()
		{
			Entities.OperatingSystem[] oss = await this._osManager.GetOSs();
			OSModel[] models = Mapper.Map<OSModel[]>(oss);
			return models;
		}
    }
}
