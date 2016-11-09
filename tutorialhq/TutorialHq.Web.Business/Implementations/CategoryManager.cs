using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Data;
using TutorialHq.Web.Entities;
using TutorialHq.Web.Exceptions;
using TutorialHq.Web.Resources;

namespace TutorialHq.Web.Business.Implementations
{
	public class CategoryManager : ICategoryManager
	{
		public Task<Category> GetCategory(int categoryId)
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					Category category = ctx.Categories
										.Where(c => c.Id == categoryId)
										.FirstOrDefault<Category>();
					if (category == null)
					{
						throw new THQNotFoundException(Strings.category);
					}
					return category;
				}
			});
		}

		public Task<Category[]> GetCategories()
		{
			return Task.Run(() =>
			{
				using (var ctx = new THQEntities())
				{
					return ctx.Categories.ToArray<Category>();
				}
			});
		}
	}
}
