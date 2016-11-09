using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialHq.Web.Entities
{
	public abstract class EntityBase
	{
		public abstract int Id { get; set; }

		[Required]
		[Column("created")]
		public DateTime Created { get; set; }

		[Required]
		[Column("last_modified")]
		public DateTime LastModified { get; set; }

		[Required]
		[Column("ip")]
		[StringLength(30)]
		public string Ip { get; set; }
	}
}
