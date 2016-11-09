using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolList.Models
{
	public class ToolSubmitModel
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("siteUrl")]
		public string SiteUrl { get; set; }

		[JsonProperty("downloadUrl")]
		public string DownloadUrl { get; set; }

		[JsonProperty("imageUrl")]
		public string ImageUrl { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("type")]
		public int Type { get; set; }

		[JsonProperty("license")]
		public int License { get; set; }

		[JsonProperty("oss")]
		public int[] OSs { get; set; }

		[JsonProperty("category")]
		public int Category { get; set; }

		[JsonProperty("creator")]
		public string Creator { get; set; }

		[JsonProperty("creatorSite")]
		public string CreatorSite { get; set; }
	}
}
