using Owin;
using Microsoft.Owin.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Unity.WebApi;
using TutorialHq.Web.Business.Interfaces;
using TutorialHq.Web.Business.Implementations;
using AutoMapper;
using TutorialHq.Web.Entities;
using TutorialHq.Web.Models;
using TutorialHq.Web.Security;
using Microsoft.AspNet.Identity;
using Swashbuckle.Application;
using System.Reflection;

namespace TutorialHq.Web
{
	public class Startup
	{
		public void Configuration(IAppBuilder builder)
		{
			log4net.Config.XmlConfigurator.Configure();

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
			container.RegisterType<ICategoryManager, CategoryManager>();
			container.RegisterType<IUserManager, UserManager>();
			container.RegisterType<ITutorialManager, TutorialManager>();
			container.RegisterType<IPasswordHasher, PasswordHasher>();
			container.RegisterType<ILogService, Log4NetService>();
			httpConfiguration.DependencyResolver = new UnityDependencyResolver(container);

			builder.Use(typeof(BasicAuthenticationMiddleware), container.Resolve<IUserManager>());

			Mapper.CreateMap<Category, CategoryModel>();
			Mapper.CreateMap<Tutorial, TutorialModel>()
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Title))
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName));
			Mapper.CreateMap<TutorialModel, Tutorial>();
			Mapper.CreateMap<Comment, CommentModel>()
				.ForMember(dest => dest.Tutorial, opt => opt.MapFrom(src => src.Tutorial.Title))
				.ForMember(dest => dest.TutorialId, opt => opt.MapFrom(src => src.Tutorial.Id))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
			Mapper.CreateMap<CommentModel, Comment>();
			Mapper.CreateMap<User, UserModel>();
			Mapper.CreateMap<UserRegisterModel, User>();
			Mapper.CreateMap<Vote, VoteModel>()
				.ForMember(dest => dest.TutorialId, opt => opt.MapFrom(src => src.Tutorial.Id));
			Mapper.CreateMap<VoteModel, Vote>();

			httpConfiguration.Filters.Add(new THQApiFilter(container.Resolve<ILogService>()));

			ILogService logService = container.Resolve<ILogService>();
			logService.Debug(this, string.Format("TutorialHQ version {0} started", Assembly.GetExecutingAssembly().GetName().Version.ToString()));

			httpConfiguration.MapHttpAttributeRoutes();

			httpConfiguration
				.EnableSwagger(c => c.SingleApiVersion("v1", "TutorialHQ API"))
				.EnableSwaggerUi();

			builder.UseWebApi(httpConfiguration);
		}
	}
}