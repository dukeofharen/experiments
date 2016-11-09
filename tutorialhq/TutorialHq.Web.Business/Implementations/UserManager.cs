using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Data;
using TutorialHq.Web.Entities;
using TutorialHq.Web.Entities.Enums;
using TutorialHq.Web.Exceptions;
using TutorialHq.Web.Business.Extensions;
using TutorialHq.Web.Resources;

namespace TutorialHq.Web.Business.Implementations
{
	public class UserManager : IUserManager
	{
		private IPasswordHasher _passwordHasher;

		public UserManager(IPasswordHasher passwordHasher)
		{
			this._passwordHasher = passwordHasher;
		}

		public Task<bool> ValidateUser(string username, string password)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					User user = ctx.Users
								.Where(u => u.UserName == username)
								.FirstOrDefault<User>();
					if (user != null)
					{
						bool valid = this._passwordHasher.VerifyHashedPassword(user.PasswordHash, password) != PasswordVerificationResult.Failed;
						if (valid)
						{
							user.LastLogin = DateTime.Now;
							ctx.SaveChanges();
						}
						return valid;
					}
					return false;
				}
			});
		}

		public Task ActivateUser(string username, string activationCode)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					if (string.IsNullOrEmpty(activationCode) && activationCode != "0")
					{
						throw new THQArgumentException(Strings.activationCode);
					}
					User user = ctx.Users
								.Where(u => u.UserName == username && u.ActivationCode == activationCode)
								.FirstOrDefault<User>();
					if (user == null)
					{
						throw new THQNotFoundException(Strings.user);
					}
					user.Activated = true;
					user.ActivationCode = "0";
					ctx.SaveChanges();
				}
			});
		}

		public Task<User> GetUser(string username)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					User user = ctx.Users
								.Where(u => u.UserName == username)
								.FirstOrDefault<User>();
					if (user == null)
					{
						throw new THQNotFoundException(Strings.user);
					}
					return user;
				}
			});
		}

		public Task<User[]> GetUsers()
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					User[] users = ctx.Users
									.ToArray<User>();
					return users;
				}
			});
		}


		public Task<User> RegisterUser(User user, string password, string passwordRepeat, string ip)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					if (string.IsNullOrEmpty(password) || password != passwordRepeat)
					{
						throw new THQArgumentException(Strings.password);
					}
					if (string.IsNullOrEmpty(user.Email) || !user.Email.IsValidEmail())
					{
						throw new THQArgumentException(Strings.email);
					}
					if (string.IsNullOrEmpty(user.UserName))
					{
						throw new THQArgumentException(Strings.password);
					}
					if (ctx.Users.Where(u => u.UserName == user.UserName).Any())
					{
						throw new THQConflictException(Strings.username);
					}
					if (this.EmailExists(user.Email))
					{
						throw new THQConflictException(Strings.email);
					}

					User newUser = new User();
					newUser.Created = DateTime.Now;
					newUser.LastModified = DateTime.MinValue;
					newUser.LastLogin = DateTime.MinValue;
					newUser.Location = user.Location;
					newUser.Name = user.Name;
					newUser.UserName = user.UserName;
					newUser.UserRole = UserRole.RegularUser;
					newUser.Website = user.Website;
					newUser.Email = user.Email;
					newUser.Ip = ip;
					newUser.PasswordHash = this._passwordHasher.HashPassword(password);
					newUser.Activated = false;
					newUser.ActivationCode = Guid.NewGuid().ToString();
					ctx.Users.Add(newUser);
					ctx.SaveChanges();
					return newUser;
				}
			});
		}

		public Task UpdateUser(User user, string username, string password = "", string passwordRepeat = "")
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					User currUser = ctx.Users
									.Where(u => u.UserName == username)
									.FirstOrDefault<User>();
					if (currUser == null)
					{
						throw new THQNotFoundException(Strings.user);
					}

					currUser.LastModified = DateTime.Now;

					if (!string.IsNullOrEmpty(password))
					{
						if (password != passwordRepeat)
						{
							throw new THQArgumentException(Strings.password);
						}
						currUser.PasswordHash = this._passwordHasher.HashPassword(password);
					}
					else if (!string.IsNullOrEmpty(user.Email) && user.Email != currUser.Email)
					{
						if (string.IsNullOrEmpty(user.Email) || !user.Email.IsValidEmail())
						{
							throw new THQArgumentException(Strings.email);
						}
						if (this.EmailExists(user.Email))
						{
							throw new THQConflictException(Strings.email);
						}
						currUser.Email = user.Email;
					}
					else
					{
						currUser.Name = user.Name;
						currUser.Location = user.Location;
						currUser.Website = user.Website;
					}
					ctx.SaveChanges();
				}
			});
		}

		private bool EmailExists(string email)
		{
			using (var ctx = new THQEntities())
			{
				var query = from u in ctx.Users
							where u.Email == email
							select u;
				return query.Any();
			}
		}
	}
}
