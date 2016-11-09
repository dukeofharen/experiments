using SiteOnWheels.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data
{
	public static class ContentParser
	{
		public const string PreviewImageFormat = "<img src=\"{0}\" alt=\"{1}\" title=\"{1}\" />";

		public static void ParseItemTemplate(this Item item)
		{
			string frame = string.Empty;
			if (item.IsPost)
			{
				frame = DataObject.PostFrame;
			}
			else
			{
				frame = DataObject.PageFrame;
			}
			item.RenderedContents = frame.Replace("[content]", item.RenderedContents);
		}

		public static string ParseTemplate(this string contents)
		{
			return DataObject.Frame.Replace("[content]", contents);
		}

		public static void ParseVariables(this Item item)
		{
			string previewImage = string.Empty;
			if (!string.IsNullOrEmpty(item.PreviewImage))
			{
				previewImage = string.Format(PreviewImageFormat, item.PreviewImage, item.Title);
			}
			item.RenderedContents = item.RenderedContents.Replace("[post:preview-image]", previewImage);
			item.RenderedContents = item.RenderedContents.Replace("[post:content]", item.OriginalRenderedContents);
			item.RenderedContents = item.RenderedContents.Replace("[post:full-url]", string.Format("{0}/{1}", DataObject.SiteObject.RootUrl, item.NewFileName));

			List<string> tags = new List<string>();
			foreach (string tag in item.Tags)
			{
				string filename = tag.GetTagFilename();
				tags.Add(string.Format("<a href=\"{0}\">{1}</a>", string.Format("{0}/{1}", DataObject.SiteObject.RootUrl, filename), tag));
			}

			item.RenderedContents = item.RenderedContents.Replace("[post:tags]", string.Join(", ", tags));
			string changedOn = string.Empty;
			if (item.ChangedOn.HasValue)
			{
				changedOn = item.ChangedOn.Value.ToString(DataObject.SiteObject.DateFormat);
			}
			item.RenderedContents = item.RenderedContents.Replace("[post:changed-on]", changedOn);
			item.RenderedContents = item.RenderedContents.Replace("[post:added-on]", item.AddedOn.ToString(DataObject.SiteObject.DateFormat));
			item.RenderedContents = item.RenderedContents.Replace("[post:author]", item.Author);
			item.RenderedContents = item.RenderedContents.Replace("[post:title]", item.Title);
			item.RenderedContents = item.RenderedContents.Replace("[post:filename]", item.NewFileName);
			item.RenderedContents = item.RenderedContents.Replace("[post:preview]", item.Preview);
		}

		public static string ParseGlobalVariables(this string contents)
		{
			contents = contents.Replace("[header]", DataObject.HeaderFrame);
			contents = contents.Replace("[footer]", DataObject.FooterFrame);

			foreach (KeyValuePair<string, string> pair in DataObject.SiteObject.Variables)
			{
				contents = contents.Replace(string.Format("[var:{0}]", pair.Key), pair.Value);
			}

			contents = contents.Replace("[site-name]", DataObject.SiteObject.SiteName);
			contents = contents.Replace("[full-site-title]", DataObject.SiteObject.FullSiteTitle);
			contents = contents.Replace("[site-title]", DataObject.SiteTitle);
			contents = contents.Replace("[language-code]", DataObject.SiteObject.LanguageCode);
			contents = contents.Replace("[meta-description]", DataObject.SiteObject.MetaDescription);
			contents = contents.Replace("[meta-keywords]", DataObject.SiteObject.MetaKeywords);
			contents = contents.Replace("[root-url]", DataObject.SiteObject.RootUrl);
			contents = contents.Replace("[year]", DateTime.Now.Year.ToString());
			
			return contents;
		}

		public static string ParseMenu(this string contents)
		{
			string frame = DataObject.MenuItemFrame;
			StringBuilder builder = new StringBuilder();
			foreach (MenuItem item in DataObject.SiteObject.MenuItems)
			{
				builder.Append(frame.Replace("[menu:text]", item.Text).Replace("[menu:link]", item.Link));
			}
			contents = contents.Replace("[menu]", builder.ToString());

			return contents;
		}

		public static string StripNewline(this string input)
		{
			return input.Replace(Environment.NewLine, string.Empty);
		}

		public static string ParsePaging(this string input, int numOfPages, int selectedPage)
		{
			string result = string.Empty;
			if (numOfPages > 1)
			{
				StringBuilder builder = new StringBuilder();
				SiteObject obj = DataObject.SiteObject;
				for (int i = 0; i < numOfPages; i++)
				{
					string filename = i == 0 ? "index.html" : string.Format("page-{0}.html", i + 1);
					builder.Append(DataObject.PagingItem
						.Replace("[page:url]", string.Format("{0}/{1}", obj.RootUrl, filename))
						.Replace("[page:number]", (i + 1).ToString())
						.Replace("[page:selected]", (selectedPage == i ? "selected" : "")));
				}
				result = DataObject.PagingFrame.Replace("[content]", builder.ToString());
			}
			return input.Replace("[paging]", result);
		}

		public static string GetTagFilename(this string tag)
		{
			Regex rgx = new Regex("[^a-zA-Z0-9 -]");
			return string.Format("tag-{0}.html", WebUtility.UrlEncode(rgx.Replace(tag, string.Empty)));
		}
	}
}
