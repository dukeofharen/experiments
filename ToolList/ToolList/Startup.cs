using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Practices.Unity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Unity.WebApi;
using Swashbuckle.Application;
using ToolList.Filters;
using ToolList.Business;
using ToolList.Business.Implementations;
using AutoMapper;
using ToolList.Entities;
using ToolList.Models;

namespace ToolList
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
			container.RegisterType<IToolManager, ToolManager>();
			container.RegisterType<ICategoryManager, CategoryManager>();
			container.RegisterType<IOSManager, OSManager>();
			httpConfiguration.DependencyResolver = new UnityDependencyResolver(container);

			Mapper.CreateMap<Tool, ToolModel>();
			Mapper.CreateMap<Category, CategoryModel>();
			Mapper.CreateMap<Entities.OperatingSystem, OSModel>();

			httpConfiguration.Filters.Add(new TLErrorAttribute());

			httpConfiguration
				.EnableSwagger(c => c.SingleApiVersion("v1", "ToolList API"))
				.EnableSwaggerUi();

			httpConfiguration.MapHttpAttributeRoutes();
			builder.UseWebApi(httpConfiguration);
		}
	}
}
