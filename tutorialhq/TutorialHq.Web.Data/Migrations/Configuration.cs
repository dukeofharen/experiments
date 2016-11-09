namespace TutorialHq.Web.Data.Migrations
{
	using Microsoft.AspNet.Identity;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using TutorialHq.Web.Entities;
	using TutorialHq.Web.Entities.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<TutorialHq.Web.Data.THQEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
			// If database doesn't exist, create it and seed it with dummy data
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<THQEntities, Configuration>());
			SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(THQEntities context)
        {
			var passwordHasher = new PasswordHasher();
			string ip = "91.72.231.108";

			Category cat1 = new Category
			{
				Id = 1,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Title = "ASP.NET",
				Description = "ASP.NET is Microsofts programming framework. You can create (web) applications using C#, MVC, Web API and more.",
				Ip = ip
			};
			Category cat2 = new Category
			{
				Id = 2,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Title = "PHP",
				Description = "PHP is one of the most popular web based scripting languages. Popular frameworks include Zend, Symphony, Laravel and Yii.",
				Ip = ip
			};

			User user1 = new User
			{
				Id = 1,
				Created = DateTime.Now,
				Email = "dwinterwerp@quintor.nl",
				LastLogin = DateTime.Now,
				LastModified = DateTime.MinValue,
				Location = "Haren gn",
				Name = "Duco Winterwerp",
				PasswordHash = passwordHasher.HashPassword("password1"),
				UserName = "duco",
				UserRole = UserRole.Administrator,
				Ip = ip,
				Website = "http://duco.cc",
				Activated = true,
				ActivationCode = Guid.NewGuid().ToString()
			};
			User user2 = new User
			{
				Id = 2,
				Created = DateTime.Now,
				Email = "gwinterwerp38@gmail.com",
				LastLogin = DateTime.Now,
				LastModified = DateTime.MinValue,
				Location = "Annen",
				Name = "Gerrie Winterwerp",
				PasswordHash = passwordHasher.HashPassword("password2"),
				UserName = "gerrie",
				UserRole = UserRole.RegularUser,
				Ip = ip,
				Website = "http://gerr.ie",
				Activated = true,
				ActivationCode = Guid.NewGuid().ToString()
			};

			Tutorial tut1 = new Tutorial
			{
				Id = 1,
				Created = DateTime.Now,
				Category = cat1,
				Description = "The complete ASP.NET tutorial",
				Ip = ip,
				LastModified = DateTime.MinValue,
				Title = "ASP.NET tutorial",
				Url = "http://asp.net-tutorials.com/",
				User = user1,
				Status = TutorialStatus.Approved,
				NumClicks = 1,
				NumComments = 2,
				NumVotes = 1,
				AvgRating = 2
			};
			Tutorial tut2 = new Tutorial
			{
				Id = 2,
				Created = DateTime.Now,
				Category = cat2,
				Description = "Create a simple PHP view engine",
				Ip = ip,
				LastModified = DateTime.MinValue,
				Title = "Simple PHP view engine",
				Url = "http://duco.cc/create-a-simple-template-view-engine-with-php/",
				User = user2,
				Status = TutorialStatus.Pending,
				NumClicks = 1,
				NumComments = 0,
				NumVotes = 1,
				AvgRating = 9
			};

			Comment com1 = new Comment
			{
				Id = 1,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Content = "Wow, great tutorial!",
				Tutorial = tut1,
				User = user1,
				Ip = ip
			};
			Comment com2 = new Comment
			{
				Id = 2,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Content = "Wow, great tutorial to!",
				Tutorial = tut1,
				User = user1,
				Ip = ip
			};

			Click cl1 = new Click
			{
				Id = 1,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Ip = ip,
				Tutorial = tut1
			};
			Click cl2 = new Click
			{
				Id = 2,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Ip = ip,
				User = user1,
				Tutorial = tut2
			};

			Vote vo1 = new Vote
			{
				Id = 1,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Ip = ip,
				User = user2,
				Tutorial = tut1,
				Rating = 2
			};
			Vote vo2 = new Vote
			{
				Id = 2,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Ip = ip,
				User = user1,
				Tutorial = tut2,
				Rating = 9
			};

			context.Categories.AddOrUpdate(c => c.Title, cat1, cat2);
			context.Users.AddOrUpdate(u => u.UserName, user1, user2);
			context.Tutorials.AddOrUpdate(t => t.Title, tut1, tut2);
			context.Comments.AddOrUpdate(c => c.Content, com1, com2);
			context.Clicks.AddOrUpdate(c => c.Id, cl1, cl2);
			context.Votes.AddOrUpdate(v => v.Rating, vo1, vo2);

			context.SaveChanges();
        }
    }
}
