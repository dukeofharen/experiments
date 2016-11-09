using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Data;
using TutorialHq.Web.Entities;
using TutorialHq.Web.Entities.Enums;
using TutorialHq.Web.Exceptions;
using TutorialHq.Web.Resources;

namespace TutorialHq.Web.Business.Implementations
{
	public class TutorialManager : ITutorialManager
	{

		public Task<Tutorial> GetTutorial(int tutorialId)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					Tutorial tutorial = ctx.Tutorials
										.Where(t => t.Id == tutorialId)
										.Include(t => t.Category)
										.Include(t => t.User)
										.FirstOrDefault<Tutorial>();
					if (tutorial == null)
					{
						throw new THQNotFoundException(Strings.tutorial);
					}
					return tutorial;
				}
			});
		}

		public Task<Tutorial[]> GetTutorials(int howMany, int page, int categoryId = 0)
		{
			return Task.Run(() =>
			{
				if (howMany > 50)
				{
					throw new THQNotFoundException(Errors.tooManyRequests);
				}
				using (var ctx = new THQEntities())
				{
					Tutorial[] tutorials = ctx.Tutorials
											.Include(t => t.Category)
											.Include(t => t.User)
											.ToArray<Tutorial>();
					var query = tutorials.OrderByDescending(t => t.Created).AsQueryable();
					if (categoryId != 0)
					{
						query = query.Where(t => t.Category.Id == categoryId);
					}
					query = query.Skip(page * howMany).Take(howMany);
					return query.ToArray<Tutorial>();
				}
			});
		}


		public Task<Tutorial> AddTutorial(Tutorial tutorial, int categoryId, string ip, string username)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					if (string.IsNullOrEmpty(tutorial.Title))
					{
						throw new THQArgumentException(Strings.title);
					}
					if (string.IsNullOrEmpty(tutorial.Description))
					{
						throw new THQArgumentException(Strings.description);
					}
					if (string.IsNullOrEmpty(tutorial.Url))
					{
						throw new THQArgumentException(Strings.url);
					}
					Category category = ctx.Categories
										.Where(c => c.Id == categoryId)
										.FirstOrDefault<Category>();
					if (category == null)
					{
						throw new THQNotFoundException(Strings.category);
					}
					User user = ctx.Users
								.Where(u => u.UserName == username)
								.FirstOrDefault<User>();
					if (user == null)
					{
						throw new THQNotFoundException(Strings.user);
					}

					Tutorial newTutorial = new Tutorial();
					newTutorial.Created = DateTime.Now;
					newTutorial.LastModified = DateTime.MinValue;
					newTutorial.Title = tutorial.Title;
					newTutorial.Description = tutorial.Description;
					newTutorial.Url = tutorial.Url;
					newTutorial.Status = TutorialStatus.NewQueue;
					newTutorial.Category = ctx.Categories.Attach(category);
					newTutorial.Ip = ip;
					newTutorial.User = ctx.Users.Attach(user);
					ctx.Tutorials.Add(newTutorial);
					ctx.SaveChanges();
					return newTutorial;
				}
			});
		}


		public Task UpdateTutorial(Tutorial tutorial, int tutorialId, int categoryId = 0)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					Tutorial currTutorial = ctx.Tutorials
											.Where(t => t.Id == tutorialId)
											.Include(t => t.User)
											.FirstOrDefault<Tutorial>();
					if (currTutorial == null)
					{
						throw new THQNotFoundException(Strings.tutorial);
					}

					Category category = null;
					if (categoryId != 0)
					{
						category = ctx.Categories
											.Where(c => c.Id == categoryId)
											.FirstOrDefault<Category>();
						if (category == null && categoryId != null)
						{
							throw new THQNotFoundException(Strings.category);
						}
					}

					currTutorial.LastModified = DateTime.Now;
					currTutorial.Title = tutorial.Title;
					currTutorial.Description = tutorial.Description;
					currTutorial.Url = tutorial.Url;
					currTutorial.Status = tutorial.Status;
					if (category != null)
					{
						currTutorial.Category = category;
					}
					ctx.SaveChanges();
				}
			});
		}


		public Task DeleteTutorial(int tutorialId)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					Tutorial tutorial = ctx.Tutorials
										.Where(t => t.Id == tutorialId)
										.Include(t => t.Category)
										.Include(t => t.User)
										.FirstOrDefault<Tutorial>();
					if (tutorial == null)
					{
						throw new THQNotFoundException(Strings.tutorial);
					}

					if (tutorial.Status != TutorialStatus.Deleted)
					{
						tutorial.Status = TutorialStatus.Deleted;
						ctx.SaveChanges();
					}
				}
			});
		}


		public Task<Comment[]> GetComments(int tutorialId)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					Tutorial tutorial = ctx.Tutorials
										.Where(t => t.Id == tutorialId)
										.Include(t => t.Comments)
										.FirstOrDefault<Tutorial>();
					if (tutorial == null)
					{
						throw new THQNotFoundException(Strings.tutorial);
					}
					return tutorial.Comments.ToArray<Comment>();
				}
			});
		}


		public Task<Comment> AddComment(Comment comment, int tutorialId, string ip, string username)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					if (string.IsNullOrEmpty(comment.Content))
					{
						throw new THQArgumentException(Strings.content);
					}

					Tutorial tutorial = ctx.Tutorials
										.Where(t => t.Id == tutorialId)
										.Include(t => t.Category)
										.Include(t => t.User)
										.FirstOrDefault<Tutorial>();
					if (tutorial == null)
					{
						throw new THQNotFoundException(Strings.tutorial);
					}

					User user = ctx.Users
								.Where(u => u.UserName == username)
								.FirstOrDefault<User>();
					if (user == null)
					{
						throw new THQNotFoundException(Strings.user);
					}

					Comment newComment = new Comment();
					newComment.Created = DateTime.Now;
					newComment.LastModified = DateTime.MinValue;
					newComment.Content = comment.Content;
					newComment.Ip = ip;
					newComment.User = user;
					newComment.Tutorial = tutorial;
					tutorial.NumComments += 1;

					ctx.Comments.Add(newComment);
					ctx.SaveChanges();
					return newComment;
				}
			});
		}

		public Task DeleteComment(int commentId)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					Comment comment = ctx.Comments
										.Where(c => c.Id == commentId)
										.FirstOrDefault<Comment>();
					if (comment == null)
					{
						throw new THQNotFoundException(Strings.comment);
					}

					ctx.Comments.Remove(comment);
					ctx.SaveChanges();
				}
			});
		}

		public Task<Vote> CastVote(int tutorialId, string username, int rating, string ip)
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

					Tutorial tutorial = ctx.Tutorials
										.Where(t => t.Id == tutorialId)
										.Include(t => t.User)
										.Include(t => t.Category)
										.FirstOrDefault<Tutorial>();
					if (tutorial == null)
					{
						throw new THQNotFoundException(Strings.tutorial);
					}

					if (tutorial.User.Id == user.Id)
					{
						throw new THQArgumentException(Errors.cantVoteOwnTutorials);
					}

					if (!this.UserCanVote(username, tutorialId))
					{
						throw new THQArgumentException(Errors.voteLimit);
					}

					if (rating <= 0 || rating > 10)
					{
						throw new THQArgumentException(Errors.ratingRange);
					}

					Vote vote = new Vote()
					{
						Created = DateTime.Now,
						LastModified = DateTime.MinValue,
						Ip = ip,
						Rating = rating,
						Tutorial = tutorial,
						User = user
					};
					ctx.Votes.Add(vote);

					tutorial.NumVotes += 1;
					if (tutorial.AvgRating == 0)
					{
						tutorial.AvgRating = rating;
					}
					double avgRating = ((tutorial.AvgRating * (double)(tutorial.NumVotes - 1)) + rating) / (double)tutorial.NumVotes;
					tutorial.AvgRating = avgRating;

					ctx.SaveChanges();
					return vote;
				}
			});
		}


		public Task<Click> AddClick(int tutorialId, string username, string ip)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					User user = null;
					if (!string.IsNullOrEmpty(username))
					{
						user = ctx.Users
								.Where(u => u.UserName == username)
								.FirstOrDefault<User>();
						if (user == null)
						{
							throw new THQNotFoundException(Strings.user);
						}
					}

					Tutorial tutorial = ctx.Tutorials
										.Where(t => t.Id == tutorialId)
										.Include(t => t.User)
										.Include(t => t.Category)
										.FirstOrDefault<Tutorial>();
					if (tutorial == null)
					{
						throw new THQNotFoundException(Strings.tutorial);
					}

					Click click = new Click()
					{
						Created = DateTime.Now,
						LastModified = DateTime.MinValue,
						Ip = ip
					};
					click.User = user;

					tutorial.NumClicks += 1;
					click.Tutorial = tutorial;

					ctx.Clicks.Add(click);
					ctx.SaveChanges();
					return click;
				}
			});
		}

		private bool UserCanVote(string username, int tutorialId)
		{
			using (var ctx = new THQEntities())
			{
				DateTime yesterday = DateTime.Now.AddDays(-1);
				User user = (from u in ctx.Users
							 where u.UserName == username
							 select u).FirstOrDefault<User>();
				var query = (from v in ctx.Votes
							 where v.User.UserName == username
							 && v.Tutorial.Id == tutorialId
							 select v).Include(v => v.User).Include(v => v.Tutorial);
				Vote[] votes = query
									.Where(v => v.Created > yesterday)
									.ToArray<Vote>();
				return votes.Length == 0;
			}
		}
	}
}
