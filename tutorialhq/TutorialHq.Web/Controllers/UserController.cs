using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Entities;
using TutorialHq.Web.Exceptions;
using TutorialHq.Web.Models;
using TutorialHq.Web.Resources;

namespace TutorialHq.Web.Controllers
{
	[RoutePrefix("api/users")]
    public class UserController : ApiController
    {
		private IUserManager _userManager;

		public UserController(IUserManager userManager)
		{
			this._userManager = userManager;
		}

		[Route("{username}")]
		[Authorize]
		public async Task<IHttpActionResult> GetUser([FromUri]string username)
		{
			User user = await this._userManager.GetUser(username);
			UserModel model = Mapper.Map<UserModel>(user);
			return this.Ok(model);
		}

		[Route("")]
		[Authorize(Roles = "Administrator")]
		public async Task<IHttpActionResult> GetUsers()
		{
			User[] users = await this._userManager.GetUsers();
			UserModel[] models = Mapper.Map<UserModel[]>(users);
			return this.Ok(models);
		}

		[Route("")]
		[HttpPost]
		public async Task<IHttpActionResult> AddUser([FromBody]UserRegisterModel model)
		{
			if (model == null)
			{
				throw new THQArgumentException(Errors.corruptJson);
			}
			User user = Mapper.Map<User>(model);
			User newUser = await this._userManager.RegisterUser(user, model.Password, model.PasswordRepeat, HttpContext.Current.Request.UserHostAddress);
			UserModel userModel = Mapper.Map<UserModel>(newUser);
			this.SendWelcomeMail(newUser, HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
			return this.Ok(userModel);
		}

		[Route("{username}")]
		[HttpPut]
		[Authorize]
		public async Task<IHttpActionResult> UpdateUser([FromBody]UserRegisterModel model, [FromUri]string username)
		{
			if (model == null)
			{
				throw new THQArgumentException(Errors.corruptJson);
			}
			IPrincipal currentUser = HttpContext.Current.User;
			if (!currentUser.IsInRole("Administrator") && username != currentUser.Identity.Name)
			{
				throw new THQNotAuthorizedException(Errors.cantUpdateProfile);
			}

			User user = Mapper.Map<User>(model);
			await this._userManager.UpdateUser(user, username, model.Password, model.PasswordRepeat);
			return this.StatusCode(HttpStatusCode.NoContent);
		}

		[Route("{username}/activate/{activationCode}")]
		[HttpGet]
		public async Task<IHttpActionResult> ActivateUser([FromUri]string username, [FromUri]string activationCode)
		{
			await this._userManager.ActivateUser(username, activationCode);
			return this.StatusCode(HttpStatusCode.NoContent);
		}

		private void SendWelcomeMail(User user, string rootUrl)
		{
			Task.Run(() =>
			{
				string ngUrl = string.Format(Strings.ngActivateUrl, user.ActivationCode);
				string url = string.Format("{0}/{1}", rootUrl, ngUrl);
				string body = Strings.welcomeMail.Replace("{NAME}", user.UserName).Replace("{LINK}", url);
				MailMessage msg = new MailMessage(WebConfigurationManager.AppSettings["noReplyMail"], user.Email, Strings.welcomeMailSubject, body);
				msg.IsBodyHtml = true;
				SmtpClient smtp = new SmtpClient();
				smtp.Send(msg);
			});
		}
    }
}
