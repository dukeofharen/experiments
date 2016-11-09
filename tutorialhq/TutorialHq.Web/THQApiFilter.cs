using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Exceptions;

namespace TutorialHq.Web
{
	public class THQApiFilter : ExceptionFilterAttribute
	{
		private ILogService _logService;

		public THQApiFilter(ILogService logService)
		{
			this._logService = logService;
		}

		public override void OnException(HttpActionExecutedContext ctx)
		{
			string ctrlName = ctx.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
			string actionname = ctx.ActionContext.ActionDescriptor.ActionName;
			string message = string.Format("({0}Controller - {1}) {2}", ctrlName, actionname, ctx.Exception.Message);

			bool unexpected = false;

			if (ctx.Exception is THQNotFoundException)
			{
				this.HandleRequest(ctx, HttpStatusCode.NotFound, ctx.Exception.Message);
			}
			else if (ctx.Exception is THQArgumentException)
			{
				this.HandleRequest(ctx, HttpStatusCode.BadRequest, ctx.Exception.Message);
			}
			else if (ctx.Exception is THQConflictException)
			{
				this.HandleRequest(ctx, HttpStatusCode.Conflict, ctx.Exception.Message);
			}
			else if (ctx.Exception is THQNotAuthorizedException)
			{
				this.HandleRequest(ctx, HttpStatusCode.Unauthorized, ctx.Exception.Message);
			}
			else
			{
				unexpected = true;
				this.HandleRequest(ctx, HttpStatusCode.InternalServerError, string.Empty);
			}
			if (unexpected)
			{
				this._logService.Error(this, message);
			}
			else
			{
				this._logService.Warn(this, message);
			}
		}

		private void HandleRequest(HttpActionExecutedContext ctx, HttpStatusCode code, string message)
		{
			ctx.Response = ctx.Request.CreateResponse(code, message);
		}
	}
}