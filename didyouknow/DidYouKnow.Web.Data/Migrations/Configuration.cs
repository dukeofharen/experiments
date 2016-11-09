namespace DidYouKnow.Web.Data.Migrations
{
	using DidYouKnow.Web.Entities;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<DidYouKnow.Web.Data.DYDContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(DidYouKnow.Web.Data.DYDContext context)
		{
			#region Init categories
			Category c1 = new Category()
			{
				Id = 1,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Name = "Animals",
				Description = "Facts about animals",
				Image = "animals.jpg"
			};
			Category c2 = new Category()
			{
				Id = 2,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Name = "Food",
				Description = "Facts about food",
				Image = "food.jpg"
			};
			Category c3 = new Category()
			{
				Id = 3,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Name = "Sport",
				Description = "Facts about sport",
				Image = "sport.jpg"
			};
			Category c4 = new Category()
			{
				Id = 4,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Name = "Human",
				Description = "Facts about humans",
				Image = "human.jpg"
			};
			Category c5 = new Category()
			{
				Id = 5,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Name = "Earth",
				Description = "Facts about the earth",
				Image = "earth.jpg"
			};
			Category c6 = new Category()
			{
				Id = 6,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Name = "Other",
				Description = "Other facts",
				Image = "other.jpg"
			};
			#endregion

			#region Init facts
			Fact f1 = new Fact()
			{
				Id = 1,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				FactName = "Cuba is the only island in the Caribbean to have a railroad.",
				Source = "http://uselessfacts.net/",
				Category = c5
			};
			Fact f2 = new Fact()
			{
				Id = 2,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				FactName = "People say \"bless you\" when you sneeze because your heart stops for a millisecond.",
				Source = "http://uselessfacts.net/",
				Category = c4
			};
			#endregion

			#region Init seed
			context.Categories.AddOrUpdate(c => c.Id, new Category[] { c1, c2, c3, c4, c5, c6 });
			context.Facts.AddOrUpdate(f => f.Id, new Fact[] { f1, f2 });
			#endregion
		}
	}
}
