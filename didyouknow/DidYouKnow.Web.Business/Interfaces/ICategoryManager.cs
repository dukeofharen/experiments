using DidYouKnow.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Business.Interfaces
{
	public interface ICategoryManager
	{
		Task<Category> GetCategory(int categoryId);
		Task<Category[]> GetCategories();
	}
}
