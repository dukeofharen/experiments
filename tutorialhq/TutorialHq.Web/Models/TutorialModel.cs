using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutorialHq.Web.Entities.Enums;

namespace TutorialHq.Web.Models
{
	public class TutorialModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }

		[JsonProperty("last_modified")]
		public DateTime LastModified { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("num_votes")]
		public int NumVotes { get; set; }

		[JsonProperty("num_clicks")]
		public int NumClicks { get; set; }

		[JsonProperty("num_comments")]
		public int NumComments { get; set; }

		[JsonProperty("avg_rating")]
		public double AvgRating { get; set; }

		[JsonProperty("status")]
		public TutorialStatus Status { get; set; }

		[JsonProperty("category")]
		public string Category { get; set; }

		[JsonProperty("category_id")]
		public int CategoryId { get; set; }

		[JsonProperty("user")]
		public string User { get; set; }
	}
}