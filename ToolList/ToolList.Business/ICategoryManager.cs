using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolList.Entities;

namespace ToolList.Business
{
	public interface ICategoryManager
	{
		Task<Category> GetCategory(int categoryId);
		Task<Category[]> GetCategories();
	}
}
