using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutorialHq.Web.Models
{
	public class VoteModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }

		[JsonProperty("tutorial_id")]
		public int TutorialId { get; set; }

		[JsonProperty("rating")]
		public int Rating { get; set; }
	}
}