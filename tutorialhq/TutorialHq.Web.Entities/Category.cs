using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialHq.Web.Entities
{
	[Table("categories")]
	public class Category : EntityBase
	{
		[Key]
		[Column("category_id")]
		public override int Id { get; set; }

		[Required]
		[Column("name")]
		[StringLength(255)]
		public string Title { get; set; }

		[Required]
		[Column("description")]
		[StringLength(2000)]
		public string Description { get; set; }

		public Category Parent { get; set; }

		public ICollection<Category> Children { get; set; }

		public ICollection<Tutorial> Tutorials { get; set; }
	}
}
