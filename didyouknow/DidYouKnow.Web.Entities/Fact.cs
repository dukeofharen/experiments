using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Entities
{
	[Table("facts")]
	public class Fact : EntityBase
	{
		[Required]
		[Column("fact")]
		[StringLength(2000)]
		public string FactName { get; set; }

		[Column("source")]
		[StringLength(500)]
		public string Source { get; set; }

		public Category Category { get; set; }
	}
}
