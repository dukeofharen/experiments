using Microsoft.Owin;
using Owin;
using Microsoft.Owin.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.FileSystems;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Unity.WebApi;
using AutoMapper;
using DidYouKnow.Web.Entities;
using DidYouKnow.Web.Models;
using DidYouKnow.Web.Business.Interfaces;
using DidYouKnow.Web.Business.Implementations;

namespace DidYouKnow.Web
{
	public class Startup
	{
		public void Configuration(IAppBuilder builder)
		{
			builder.UseDefaultFiles(new DefaultFilesOptions()
			{
				RequestPath = new PathString(),
				DefaultFileNames = new List<string>() { "index.html" },
				FileSystem = new PhysicalFileSystem(@".\app")
			});

			builder.UseStaticFiles(new StaticFileOptions()
			{
				RequestPath = new PathString(),
				FileSystem = new PhysicalFileSystem(@".\app")
			});

			HttpConfiguration httpConfiguration = new HttpConfiguration();

			UnityContainer container = new UnityContainer();
			container.RegisterType<IFactManager, FactManagerADO>();
			container.RegisterType<ICategoryManager, CategoryManager>();
			httpConfiguration.DependencyResolver = new UnityDependencyResolver(container);

			Mapper.CreateMap<Fact, FactModel>()
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
			Mapper.CreateMap<Category, CategoryModel>();

			httpConfiguration.MapHttpAttributeRoutes();

			builder.UseWebApi(httpConfiguration);
		}
	}
}