using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Entities;
using TutorialHq.Web.Exceptions;
using TutorialHq.Web.Models;

namespace TutorialHq.Web.Controllers
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
		public async Task<IHttpActionResult> GetCategory([FromUri]int categoryId)
		{
			Category category = await this._categoryManager.GetCategory(categoryId);
			CategoryModel model = Mapper.Map<CategoryModel>(category);
			return this.Ok(model);
		}

		[Route("")]
		public async Task<IHttpActionResult> GetCategories()
		{
			Category[] categories = await this._categoryManager.GetCategories();
			CategoryModel[] models = Mapper.Map<CategoryModel[]>(categories);
			return this.Ok(models);
		}
    }
}
