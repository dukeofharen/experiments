using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialHq.Web.Entities
{
	[Table("clicks")]
	public class Click : EntityBase
	{
		[Key]
		[Column("click_id")]
		public override int Id { get; set; }

		[Required]
		public Tutorial Tutorial { get; set; }

		public User User { get; set; }
	}
}
