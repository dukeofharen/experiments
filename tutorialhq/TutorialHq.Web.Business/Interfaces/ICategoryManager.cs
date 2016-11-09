using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Entities;

namespace TutorialHq.Web.Business.Interfaces
{
	public interface ICategoryManager
	{
		Task<Category> GetCategory(int categoryId);
		Task<Category[]> GetCategories();
	}
}
