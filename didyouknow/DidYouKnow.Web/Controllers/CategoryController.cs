using AutoMapper;
using DidYouKnow.Web.Business.Interfaces;
using DidYouKnow.Web.Entities;
using DidYouKnow.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DidYouKnow.Web.Controllers
{
	[RoutePrefix("api/categories")]
	public class CategoryController : ApiController
	{
		private ICategoryManager _categoryManager;

		public CategoryController(ICategoryManager categoryManager)
		{
			this._categoryManager = categoryManager;
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