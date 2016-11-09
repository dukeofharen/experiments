using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ToolList.Business;
using ToolList.Entities;
using ToolList.Models;

namespace ToolList.Controllers
{
	[RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
		private ICategoryManager _categoryManager;

		public CategoryController(ICategoryManager categoryManager)
		{
			this._categoryManager = categoryManager;
		}

		[Route("{categoryId:int}")]
		[HttpGet]
		public async Task<CategoryModel> GetCategory([FromUri]int categoryId)
		{
			Category category = await this._categoryManager.GetCategory(categoryId);
			CategoryModel model = Mapper.Map<CategoryModel>(category);
			return model;
		}

		[Route("")]
		[HttpGet]
		public async Task<CategoryModel[]> GetCategories()
		{
			Category[] categories = await this._categoryManager.GetCategories();
			CategoryModel[] models = Mapper.Map<CategoryModel[]>(categories);
			return models;
		}
    }
}
