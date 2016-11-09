using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Entities
{
	[Table("categories")]
	public class Category : EntityBase
	{
		[Required]
		[Column("name")]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[Column("description")]
		[StringLength(500)]
		public string Description { get; set; }

		[Required]
		[Column("image")]
		[StringLength(200)]
		public string Image { get; set; }

		public Fact[] Facts { get; set; }
	}
}
