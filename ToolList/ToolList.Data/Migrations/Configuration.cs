namespace ToolList.Data.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using ToolList.Entities;

	internal sealed class Configuration : DbMigrationsConfiguration<ToolList.Data.TLEntities>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(TLEntities context)
		{
			ToolList.Entities.OperatingSystem[] oss = this.GenerateOSs();
			Category[] categories = this.GenerateCategories();
			Tool[] tools = this.GenerateTool(oss, categories);
			context.OperatingSystems.AddOrUpdate(o => o.Id, oss);
			context.Categories.AddOrUpdate(c => c.Id, categories);
			context.Tools.AddOrUpdate(t => t.Id, tools);
		}

		#region Generators
		private ToolList.Entities.OperatingSystem[] GenerateOSs()
		{
			DateTime now = DateTime.Now;
			var os1 = new ToolList.Entities.OperatingSystem()
			{
				Id = 1,
				Name = "Windows",
				Created = now
			};
			var os2 = new ToolList.Entities.OperatingSystem()
			{
				Id = 2,
				Name = "Mac",
				Created = now
			};
			var os3 = new ToolList.Entities.OperatingSystem()
			{
				Id = 3,
				Name = "Web",
				Created = now
			};
			var os4 = new ToolList.Entities.OperatingSystem()
			{
				Id = 4,
				Name = "Linux",
				Created = now
			};
			var os5 = new ToolList.Entities.OperatingSystem()
			{
				Id = 5,
				Name = "Android",
				Created = now
			};
			var os6 = new ToolList.Entities.OperatingSystem()
			{
				Id = 6,
				Name = "iOS",
				Created = now
			};
			var os7 = new ToolList.Entities.OperatingSystem()
			{
				Id = 7,
				Name = "Windows Phone",
				Created = now
			};
			return new ToolList.Entities.OperatingSystem[] { os1, os2, os3, os4, os5, os6, os7 };
		}

		private Category[] GenerateCategories()
		{
			DateTime now = DateTime.Now;
			var c1 = new Category()
			{
				Id = 1,
				Name = "Development",
				Created = now
			};
			var c2 = new Category()
			{
				Id = 2,
				Name = "Design",
				Created = now
			};
			var c3 = new Category()
			{
				Id = 3,
				Name = "Productivity",
				Created = now
			};
			var c4 = new Category()
			{
				Id = 4,
				Name = "Educational",
				Created = now
			};
			return new Category[] { c1, c2, c3, c4 };
		}

		private Tool[] GenerateTool(ToolList.Entities.OperatingSystem[] oss, Category[] categories)
		{
			var t1 = new Tool()
			{
				Id = 1,
				Name = "IForgot",
				Description = "I've created a little command line tool which captures a screenshot every once in a while (you can set the interval in minutes) and saves it to a specific location on your PC. I save them to my Dropbox. I also copied a link to the tool in my startup folder, so everytime my work laptop starts, the program opens. Now, when I need to go back in time; no problem! I've got a folder filled with screenshots.",
				DownloadUrl = "https://ducode.org/assets/downloads/iforgot.zip",
				Category = categories[2],
				Created = DateTime.Now,
				Creator = "Duco Winterwerp",
				CreatorSite = "https://ducode.org",
				ImageUrl = "https://ducode.org/assets/iforgot.png",
				License = License.Proprietary,
				OperatingSystems = new ToolList.Entities.OperatingSystem[] { oss[0] },
				SiteUrl = "https://ducode.org/iforgot-review-your-working-days.html",
				Type = ToolType.Desktop,
				Version = "0.1",
				Activated = true,
				ActivationCode = Guid.NewGuid().ToString()
			};
			var t2 = new Tool()
			{
				Id = 2,
				Name = "Site On Wheels",
				Description = "Site On Wheels is a simple, Markdown (CommonMark) enabled, .NET based, extendible static site generator. It is perfect for blogs. Wheels is a simple console application which expects an input directory (the source code for your website) and an output directory (the directory which later on contains the site which you can deploy to your webserver).",
				DownloadUrl = "https://ducode.org/assets/siteonwheels/siteonwheels.zip",
				Category = categories[0],
				Created = DateTime.Now,
				Creator = "Duco Winterwerp",
				CreatorSite = "https://ducode.org",
				ImageUrl = "https://ducode.org/assets/siteonwheels/wheels_output.jpg",
				License = License.Proprietary,
				OperatingSystems = new ToolList.Entities.OperatingSystem[] { oss[0] },
				SiteUrl = "https://ducode.org/siteonwheels.html",
				Type = ToolType.Desktop,
				Version = "0.3.2",
				Activated = true,
				ActivationCode = Guid.NewGuid().ToString()
			};
			return new Tool[] { t1, t2 };
		}
		#endregion
	}
}
