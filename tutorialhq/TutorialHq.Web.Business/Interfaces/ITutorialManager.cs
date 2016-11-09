using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Entities;

namespace TutorialHq.Web.Business.Interfaces
{
	public interface ITutorialManager
	{
		Task<Tutorial> GetTutorial(int tutorialId);
		Task<Tutorial[]> GetTutorials(int howMany, int page, int categoryId = 0);
		Task<Tutorial> AddTutorial(Tutorial tutorial, int categoryId, string ip, string username);
		Task UpdateTutorial(Tutorial tutorial, int tutorialId, int categoryId = 0);
		Task DeleteTutorial(int tutorialId);

		Task<Comment[]> GetComments(int tutorialId);
		Task<Comment> AddComment(Comment comment, int tutorialId, string ip, string username);
		Task DeleteComment(int commentId);

		Task<Vote> CastVote(int tutorialId, string username, int rating, string ip);

		Task<Click> AddClick(int tutorialId, string username, string ip);
	}
}
