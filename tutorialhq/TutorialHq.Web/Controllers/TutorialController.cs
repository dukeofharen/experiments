using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Entities;
using TutorialHq.Web.Entities.Enums;
using TutorialHq.Web.Exceptions;
using TutorialHq.Web.Models;
using TutorialHq.Web.Resources;

namespace TutorialHq.Web.Controllers
{
	[RoutePrefix("api/tutorials")]
    public class TutorialController : ApiController
    {
		private ITutorialManager _tutorialManager;
		private IUserManager _userManager;

		public TutorialController(ITutorialManager tutorialManager, IUserManager userManager)
		{
			this._tutorialManager = tutorialManager;
			this._userManager = userManager;
		}

		[Route("{tutorialId:int}")]
		public async Task<IHttpActionResult> GetTutorial([FromUri]int tutorialId)
		{
			Tutorial tutorial = await this._tutorialManager.GetTutorial(tutorialId);
			if (!this.CanSeeAllTutorials() && tutorial.Status != TutorialStatus.Approved)
			{
				throw new THQNotAuthorizedException(Errors.unauthorized);
			}
			string user = string.Empty;
			if (HttpContext.Current.User != null)
			{
				user = HttpContext.Current.User.Identity.Name;
			}
			Click click = await this._tutorialManager.AddClick(tutorialId, user, HttpContext.Current.Request.UserHostAddress);
			TutorialModel model = Mapper.Map<TutorialModel>(tutorial);
			return this.Ok(model);
		}

		[Route("")]
		public async Task<IHttpActionResult> GetTutorials([FromUri]int howMany = 10, [FromUri]int page = 0,[FromUri]int categoryId = 0)
		{
			Tutorial[] tutorials = await this._tutorialManager.GetTutorials(howMany, page, categoryId);
			if (!this.CanSeeAllTutorials())
			{
				tutorials = tutorials.Where(t => t.Status == TutorialStatus.Approved).ToArray<Tutorial>();
			}
			TutorialModel[] models = Mapper.Map<TutorialModel[]>(tutorials);
			return this.Ok(models);
		}

		[Route("")]
		[HttpPost]
		[Authorize]
		public async Task<IHttpActionResult> AddTutorial([FromBody]TutorialModel model)
		{
			Tutorial tutorial = Mapper.Map<Tutorial>(model);
			tutorial = await this._tutorialManager.AddTutorial(tutorial, model.CategoryId, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.User.Identity.Name);
			TutorialModel returnModel = Mapper.Map<TutorialModel>(tutorial);
			return this.Ok(returnModel);
		}

		[Route("{tutorialId:int}")]
		[HttpPut]
		[Authorize(Roles = "Administrator")]
		public async Task<IHttpActionResult> UpdateTutorial([FromBody]TutorialModel model, [FromUri]int tutorialId)
		{
			Tutorial tutorial = Mapper.Map<Tutorial>(model);
			await this._tutorialManager.UpdateTutorial(tutorial, tutorialId, model.CategoryId);
			return this.StatusCode(HttpStatusCode.NoContent);
		}

		[Route("{tutorialId:int}")]
		[HttpDelete]
		[Authorize(Roles = "Administrator")]
		public async Task<IHttpActionResult> DeleteTutorial([FromUri]int tutorialId)
		{
			await this._tutorialManager.DeleteTutorial(tutorialId);
			return this.StatusCode(HttpStatusCode.NoContent);
		}

		[Route("{tutorialId:int}/vote")]
		[Authorize]
		[HttpPost]
		public async Task<IHttpActionResult> CastVote([FromUri]int tutorialId, [FromBody]VoteModel model)
		{
			if (model == null)
			{
				throw new THQArgumentException(Errors.corruptJson);
			}
			Vote vote = await this._tutorialManager.CastVote(tutorialId, HttpContext.Current.User.Identity.Name, model.Rating, HttpContext.Current.Request.UserHostAddress);
			VoteModel returnModel = Mapper.Map<VoteModel>(vote);
			return this.Ok(returnModel);
		}

		private bool CanSeeAllTutorials()
		{
			var user = HttpContext.Current.User;
			if (user == null)
			{
				return false;
			}
			if (user.IsInRole("Administrator"))
			{
				return true;
			}
			return false;
		}
    }
}
