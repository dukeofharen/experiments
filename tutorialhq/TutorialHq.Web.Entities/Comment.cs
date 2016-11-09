using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialHq.Web.Entities
{
	[Table("comments")]
	public class Comment : EntityBase
	{
		[Key]
		[Column("comment_id")]
		public override int Id { get; set; }

		[Required]
		[Column("content")]
		[StringLength(2000)]
		public string Content { get; set; }

		[Required]
		public Tutorial Tutorial { get; set; }

		[Required]
		public User User { get; set; }
	}
}
