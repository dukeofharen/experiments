using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutorialHq.Web.Models
{
	public class CommentModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }

		[JsonProperty("last_modified")]
		public DateTime LastModified { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }

		[JsonProperty("tutorial")]
		public string Tutorial { get; set; }

		[JsonProperty("tutorial_id")]
		public int TutorialId { get; set; }

		[JsonProperty("username")]
		public string UserName { get; set; }
	}
}