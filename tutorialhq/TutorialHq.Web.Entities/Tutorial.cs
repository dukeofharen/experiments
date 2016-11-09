using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Entities.Enums;

namespace TutorialHq.Web.Entities
{
	[Table("tutorials")]
	public class Tutorial : EntityBase
	{
		[Key]
		[Column("tutorial_id")]
		public override int Id { get; set; }

		[Required]
		[Column("title")]
		[StringLength(255)]
		public string Title { get; set; }

		[Required]
		[Column("description")]
		[StringLength(2000)]
		public string Description { get; set; }

		[Required]
		[Column("url")]
		[StringLength(2000)]
		public string Url { get; set; }

		[Column("num_votes")]
		public int NumVotes { get; set; }

		[Column("num_clicks")]
		public int NumClicks { get; set; }

		[Column("num_comments")]
		public int NumComments { get; set; }

		[Column("avg_rating")]
		public double AvgRating { get; set; }

		[Required]
		[Column("status")]
		public TutorialStatus Status { get; set; }

		[Required]
		public Category Category { get; set; }

		[Required]
		public User User { get; set; }

		public ICollection<Comment> Comments { get; set; }

		public ICollection<Vote> Votes { get; set; }

		public ICollection<Click> Clicks { get; set; }
	}
}
