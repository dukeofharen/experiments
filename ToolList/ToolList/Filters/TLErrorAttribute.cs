using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using ToolList.Exceptions;

namespace ToolList.Filters
{
	public class TLErrorAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext ctx)
		{
			string ctrlName = ctx.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
			string actionname = ctx.ActionContext.ActionDescriptor.ActionName;
			string message = string.Format("({0}Controller - {1}) {2}", ctrlName, actionname, ctx.Exception.Message);

			if (ctx.Exception is TLNotFoundException)
			{
				this.HandleRequest(ctx, HttpStatusCode.NotFound, message);
			}
			else if (ctx.Exception is ArgumentException)
			{
				this.HandleRequest(ctx, HttpStatusCode.BadRequest, message);
			}
			else if (ctx.Exception is TLConflictException)
			{
				this.HandleRequest(ctx, HttpStatusCode.Conflict, message);
			}
			else if (ctx.Exception is TLNotAuthorizedException)
			{
				this.HandleRequest(ctx, HttpStatusCode.Unauthorized, message);
			}
			else
			{
				this.HandleRequest(ctx, HttpStatusCode.InternalServerError, message);
			}
		}

		private void HandleRequest(HttpActionExecutedContext ctx, HttpStatusCode code, string message)
		{
			ctx.Response = ctx.Request.CreateResponse(code, message);
		}
	}
}
