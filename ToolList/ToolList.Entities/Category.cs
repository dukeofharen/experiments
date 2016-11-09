using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolList.Entities
{
	[Table("categories")]
	public class Category : EntityBase
	{
		[Key]
		[Column("id")]
		public override int Id { get; set; }

		[Required]
		[Column("name")]
		[StringLength(100)]
		public string Name { get; set; }

		public ICollection<Tool> Tools { get; set; }
	}
}
