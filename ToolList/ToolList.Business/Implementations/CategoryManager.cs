using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolList.Data;
using ToolList.Entities;
using ToolList.Exceptions;

namespace ToolList.Business.Implementations
{
	public class CategoryManager : ICategoryManager
	{
		public Task<Category> GetCategory(int categoryId)
		{
			return Task.Run(() =>
			{
				using (var ctx = new TLEntities())
				{
					Category category = ctx.Categories
										.Where(c => c.Id == categoryId)
										.FirstOrDefault();
					if (category == null)
					{
						throw new TLNotFoundException("category");
					}
					return category;
				}
			});
		}

		public Task<Category[]> GetCategories()
		{
			return Task.Run(() =>
			{
				using (var ctx = new TLEntities())
				{
					return ctx.Categories.ToArray();
				}
			});
		}
	}
}
