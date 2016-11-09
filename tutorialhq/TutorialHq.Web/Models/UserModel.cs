using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutorialHq.Web.Models
{
	public class UserModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("register_date")]
		public DateTime Created { get; set; }

		[JsonProperty("username")]
		public string UserName { get; set; }

		[JsonProperty("last_login")]
		public DateTime LastLogin { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("location")]
		public string Location { get; set; }

		[JsonProperty("website")]
		public string Website { get; set; }

		[JsonProperty("role")]
		public int UserRole { get; set; }
	}
}