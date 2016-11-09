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
using TutorialHq.Web.Exceptions;
using TutorialHq.Web.Models;
using TutorialHq.Web.Resources;

namespace TutorialHq.Web.Controllers
{
	[RoutePrefix("api")]
    public class CommentController : ApiController
    {
		private ITutorialManager _tutorialManager;

		public CommentController(ITutorialManager tutorialManager)
		{
			this._tutorialManager = tutorialManager;
		}

		[Route("tutorials/{tutorialId:int}/comments")]
		public async Task<IHttpActionResult> GetComments([FromUri]int tutorialId)
		{
			Comment[] comments = await this._tutorialManager.GetComments(tutorialId);
			CommentModel[] models = Mapper.Map<CommentModel[]>(comments);
			return this.Ok(models);
		}

		[Route("tutorials/{tutorialId:int}/comments")]
		[HttpPost]
		[Authorize]
		public async Task<IHttpActionResult> AddComment([FromUri]int tutorialId, [FromBody]CommentModel model)
		{
			if (model == null)
			{
				throw new THQArgumentException(Errors.corruptJson);
			}
			if (string.IsNullOrEmpty(model.Content))
			{
				throw new THQArgumentException(Strings.tutorial);
			}
			Comment comment = Mapper.Map<Comment>(model);
			comment = await this._tutorialManager.AddComment(comment, tutorialId, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.User.Identity.Name);
			CommentModel returnModel = Mapper.Map<CommentModel>(comment);
			return this.Ok(returnModel);
		}

		[Route("tutorials/{tutorialId:int}/comments/{commentId:int}")]
		[HttpDelete]
		[Authorize(Roles = "Administrator")]
		public async Task<IHttpActionResult> DeleteComment([FromUri]int commentId, [FromUri]int tutorialId)
		{
			if (await this._tutorialManager.GetTutorial(tutorialId) == null)
			{
				throw new THQNotFoundException(Strings.tutorial);
			}
			await this._tutorialManager.DeleteComment(commentId);
			return this.StatusCode(HttpStatusCode.NoContent);
		}
    }
}
