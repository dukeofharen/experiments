using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Entities;

namespace TutorialHq.Web.Security
{
	public class BasicAuthenticationMiddleware : OwinMiddleware
	{
		private IUserManager _userManager;

		public BasicAuthenticationMiddleware(OwinMiddleware next, IUserManager userManager) : base(next)
		{
			this._userManager = userManager;
		}

		public override async Task Invoke(IOwinContext context)
		{
			var response = context.Response;
			var request = context.Request;

			response.OnSendingHeaders(state =>
			{
				var owinResponse = state as OwinResponse;
				if (owinResponse != null && owinResponse.StatusCode == 401)
				{
					owinResponse.Headers.Add("WWW-Authenticate", new[] { "Basic" });
					owinResponse.StatusCode = 403;
					owinResponse.ReasonPhrase = "Forbidden";
				}
			}, response);

			var header = request.Headers["Authorization"];
			if (!string.IsNullOrEmpty(header))
			{
				var authHeader = AuthenticationHeaderValue.Parse(header);
				if ("Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
				{
					var parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter));
					var parts = parameter.Split(':');

					var username = parts[0];
					var password = parts[1];

					User user = null;

					try
					{
						user = await this._userManager.GetUser(username);
					}
					catch
					{

					}

					if (user != null && await this._userManager.ValidateUser(username, password))
					{
						var claims = new[] { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, user.UserRole == Entities.Enums.UserRole.Administrator ? "Administrator" : "") };
						request.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Basic"));
					}
				}
			}

			await Next.Invoke(context);
		}
	}
}