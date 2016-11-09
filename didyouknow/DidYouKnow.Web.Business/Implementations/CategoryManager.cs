using DidYouKnow.Web.Business.Interfaces;
using DidYouKnow.Web.Data;
using DidYouKnow.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Business.Implementations
{
	public class CategoryManager : ICategoryManager
	{
		public Task<Category> GetCategory(int categoryId)
		{
			return Task.Run(() =>
			{
				using (var ctx = new DYDContext())
				{
					Category category = ctx.Categories
										.Where(c => c.Id == categoryId)
										.FirstOrDefault();
					return category;
				}
			});
		}

		public Task<Category[]> GetCategories()
		{
			return Task.Run(() =>
			{
				using (var ctx = new DYDContext())
				{
					return ctx.Categories.ToArray();
				}
			});
		}
	}
}
