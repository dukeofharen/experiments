using DidYouKnow.Web.Entities;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Data
{
	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class DYDContext : DbContext
	{
		public DYDContext()
			: base("name=DidYouKnow")
		{

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Fact>()
				.HasRequired(f => f.Category)
				.WithMany(c => c.Facts)
				.Map(m => m.MapKey("category_id"));
		}

		public virtual DbSet<Fact> Facts { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
	}
}
