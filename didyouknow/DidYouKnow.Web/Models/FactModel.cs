using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidYouKnow.Web.Models
{
	public class FactModel
	{
		[JsonProperty("fact")]
		public string FactName { get; set; }

		[JsonProperty("source")]
		public string Source { get; set; }

		[JsonProperty("category")]
		public string Category { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }
	}
}