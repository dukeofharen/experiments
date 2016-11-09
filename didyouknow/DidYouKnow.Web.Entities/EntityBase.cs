using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Entities
{
	public class EntityBase
	{
		[Key]
		[Required]
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[Column("created")]
		public DateTime Created { get; set; }

		[Required]
		[Column("last_modified")]
		public DateTime LastModified { get; set; }
	}
}
