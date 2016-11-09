using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolList.Entities;

namespace ToolList.Data
{
	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class TLEntities : DbContext
	{
		public TLEntities()
			: base("name=ToolList")
		{

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tool>()
				.HasMany(t => t.OperatingSystems)
				.WithMany(o => o.Tools)
				.Map(m =>
				{
					m.MapLeftKey("toolId");
					m.MapRightKey("osId");
					m.ToTable("tool_os");
				});
			modelBuilder.Entity<Tool>()
				.HasRequired(t => t.Category)
				.WithMany(c => c.Tools)
				.Map(m =>
				{
					m.MapKey("categoryId");
				});
		}

		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Tool> Tools { get; set; }
		public virtual DbSet<ToolList.Entities.OperatingSystem> OperatingSystems { get; set; }
	}
}
