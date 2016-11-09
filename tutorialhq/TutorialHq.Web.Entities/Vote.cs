using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialHq.Web.Entities
{
	[Table("votes")]
	public class Vote : EntityBase
	{
		[Key]
		[Column("vote_id")]
		public override int Id { get; set; }

		[Required]
		public User User { get; set; }

		[Required]
		public Tutorial Tutorial { get; set; }

		[Required]
		[Column("rating")]
		[Range(1, 10)]
		public int Rating { get; set; }
	}
}
