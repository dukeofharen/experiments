using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Entities;

namespace TutorialHq.Web.Business.Interfaces
{
	public interface IUserManager
	{
		Task<bool> ValidateUser(string username, string password);
		Task ActivateUser(string username, string activationCode);
		Task<User> GetUser(string username);
		Task<User[]> GetUsers();
		Task<User> RegisterUser(User user, string password, string passwordRepeat, string ip);
		Task UpdateUser(User user, string username, string password = "", string passwordRepeat = "");
	}
}
