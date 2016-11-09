using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data.Models
{
	[DataContract]
	public class SiteObject
	{
		[DataMember(Name = "site_name")]
		public string SiteName { get; set; }

		[DataMember(Name = "full_site_title")]
		public string FullSiteTitle { get; set; }

		[DataMember(Name = "root_url")]
		public string RootUrl { get; set; }

		[DataMember(Name = "blog_index_filename")]
		public string BlogIndexFilename { get; set; }

		[DataMember(Name = "template")]
		public string Template { get; set; }

		[DataMember(Name = "meta_description")]
		public string MetaDescription { get; set; }

		[DataMember(Name = "meta_keywords")]
		public string MetaKeywords { get; set; }

		[DataMember(Name = "date_format")]
		public string DateFormat { get; set; }

        [DataMember(Name = "language_code")]
        public string LanguageCode { get; set; }

		[DataMember(Name = "post_preview_length")]
		public int PostPreviewLength { get; set; }

		[DataMember(Name = "items_per_page")]
		public int ItemsPerPage { get; set; }

		[DataMember(Name = "menu_items")]
		public MenuItem[] MenuItems { get; set; }

		[DataMember(Name = "variables")]
		public Dictionary<string, string> Variables { get; set; }
	}
}
