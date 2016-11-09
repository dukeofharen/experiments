using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolList.Entities
{
	public abstract class EntityBase
	{
		public abstract int Id { get; set; }

		[Column("created")]
		public DateTime Created { get; set; }

		[Column("updated")]
		public DateTime Updated { get; set; }
	}
}
