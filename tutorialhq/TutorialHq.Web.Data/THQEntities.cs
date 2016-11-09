using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Entities;

namespace TutorialHq.Web.Data
{
	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class THQEntities : DbContext
	{
		public THQEntities()
			: base("name=TutorialHQ")
		{

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tutorial>()
				.HasMany(t => t.Comments)
				.WithRequired(c => c.Tutorial)
				.Map(m => m.MapKey("tutorial_id"));
			modelBuilder.Entity<Tutorial>()
				.HasMany(t => t.Votes)
				.WithRequired(v => v.Tutorial)
				.Map(m => m.MapKey("tutorial_id"));
			modelBuilder.Entity<Tutorial>()
				.HasMany(t => t.Clicks)
				.WithRequired(c => c.Tutorial)
				.Map(m => m.MapKey("tutorial_id"));
			modelBuilder.Entity<Category>()
				.HasMany(c => c.Children)
				.WithOptional(c => c.Parent)
				.Map(m => m.MapKey("parent_id"));
			modelBuilder.Entity<Category>()
				.HasMany(c => c.Tutorials)
				.WithRequired(t => t.Category)
				.Map(m => m.MapKey("category_id"));
			modelBuilder.Entity<User>()
				.HasMany(u => u.Tutorials)
				.WithRequired(t => t.User)
				.Map(m => m.MapKey("user_id"));
			modelBuilder.Entity<User>()
				.HasMany(u => u.Comments)
				.WithRequired(c => c.User)
				.Map(m => m.MapKey("user_id"));
			modelBuilder.Entity<User>()
				.HasMany(u => u.Votes)
				.WithRequired(v => v.User)
				.Map(m => m.MapKey("user_id"));
			modelBuilder.Entity<User>()
				.HasMany(u => u.Clicks)
				.WithOptional(c => c.User)
				.Map(m => m.MapKey("user_id"));
		}

		public virtual DbSet<Tutorial> Tutorials { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Click> Clicks { get; set; }
		public virtual DbSet<Vote> Votes { get; set; }
	}
}
