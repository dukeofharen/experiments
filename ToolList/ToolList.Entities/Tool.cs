using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolList.Entities
{
	[Table("tools")]
	public class Tool : EntityBase
	{
		[Key]
		[Column("id")]
		public override int Id { get; set; }

		[Required]
		[Column("name")]
		[StringLength(200)]
		public string Name { get; set; }

		[Required]
		[Column("siteUrl")]
		[StringLength(500)]
		public string SiteUrl { get; set; }

		[Column("downloadUrl")]
		[StringLength(500)]
		public string DownloadUrl { get; set; }

		[Column("imageUrl")]
		[StringLength(500)]
		public string ImageUrl { get; set; }

		[Required]
		[Column("activated")]
		public bool Activated { get; set; }

		[Required]
		[Column("activationCode")]
		public string ActivationCode { get; set; }

		[Required]
		[Column("description")]
		[StringLength(2000)]
		public string Description { get; set; }

		[Column("version")]
		[StringLength(20)]
		public string Version { get; set; }

		[Required]
		[Column("type")]
		public ToolType Type { get; set; }

		[Required]
		[Column("license")]
		public License License { get; set; }

		[Required]
		[Column("creator")]
		[StringLength(100)]
		public string Creator { get; set; }

		[Required]
		[Column("creatorSite")]
		[StringLength(200)]
		public string CreatorSite { get; set; }

		public Category Category { get; set; }

		public ICollection<OperatingSystem> OperatingSystems { get; set; }
	}
}
