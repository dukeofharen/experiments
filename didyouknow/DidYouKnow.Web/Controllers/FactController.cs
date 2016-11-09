using AutoMapper;
using DidYouKnow.Web.Business.Interfaces;
using DidYouKnow.Web.Entities;
using DidYouKnow.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DidYouKnow.Web.Controllers
{
	[RoutePrefix("api/facts")]
	public class FactController : ApiController
	{
		private IFactManager _factManager;

		public FactController(IFactManager factManager)
		{
			this._factManager = factManager;
		}

		[Route("random")]
		public async Task<IHttpActionResult> GetRandomFact()
		{
			Fact fact = await this._factManager.GetRandomFact();
			FactModel model = Mapper.Map<FactModel>(fact);
			return this.Ok(model);
		}
	}
}