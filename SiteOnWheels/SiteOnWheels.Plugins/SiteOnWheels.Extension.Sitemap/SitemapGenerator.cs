using SiteOnWheels.App.Data;
using SiteOnWheels.App.Data.Interfaces;
using SiteOnWheels.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.Extension.Sitemap
{
	public class SitemapGenerator : SiteOnWheelsExtension
	{
		private SiteOnWheelsWriter _writer;

		public string GetExtensionName()
		{
			return "Sitemap Generator";
		}

		public void AfterComplete(Item[] items)
		{
			this.Writer.Write("SitemapGenerator is busy creating a sitemap.");
			string frame = SitemapResources.frame;
			string itemFrame = SitemapResources.item;
			string xsl = SitemapResources.xsl;

			xsl = xsl.ParseGlobalVariables();
			frame = frame.ParseGlobalVariables();
			frame = frame.Replace("[current-sitemap-datetime]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			File.WriteAllText(string.Format("{0}{1}sitemap.xsl", DataObject.OutputLocation, Path.DirectorySeparatorChar), xsl);

			StringBuilder builder = new StringBuilder();

			items = items.OrderBy(i => i.IsPost).ToArray();

			foreach (Item siteItem in items)
			{
				string item = itemFrame;

				item = item.Replace("[sitemap:url]", string.Format("{0}/{1}", DataObject.SiteObject.RootUrl, siteItem.NewFileName));

				DateTime lastmod;
				if (siteItem.ChangedOn.HasValue)
				{
					lastmod = siteItem.ChangedOn.Value;
				}
				else
				{
					lastmod = siteItem.AddedOn;
				}
				item = item.Replace("[sitemap:lastmod]", lastmod.ToString("yyyy-MM-dd"));

				string frequency = "";
				double priority = 0;
				if (siteItem.IsPost)
				{
					int diffDays = (int)(DateTime.Now - lastmod).TotalDays;
					if (diffDays <= 20)
					{
						frequency = "daily";
						priority = 0.9;
					}
					else if (diffDays > 20 && diffDays <= 60)
					{
						frequency = "weekly";
						priority = 0.6;
					}
					else
					{
						frequency = "monthly";
						priority = 0.2;
					}
				}
				else
				{
					frequency = "weekly";
					priority = 0.7;
				}
				item = item.Replace("[sitemap:freq]", frequency);
				item = item.Replace("[sitemap:priority]", priority.ToString(CultureInfo.GetCultureInfo("en-GB")));

				builder.Append(item);
			}

			File.WriteAllText(string.Format("{0}{1}sitemap.xml", DataObject.OutputLocation, Path.DirectorySeparatorChar), frame.Replace("[urls]", builder.ToString()));

			this.Writer.Write("Done creating sitemap.");
		}


		public void OnInit()
		{
			
		}


		public void BeforePostsWrite(Item[] items)
		{
			
		}


		public string BeforeFileWrite(string content, App.Data.Enums.FileType type)
		{
			return content;
		}


		public SiteOnWheelsWriter Writer
		{
			get
			{
				return this._writer;
			}
			set
			{
				this._writer = value;
			}
		}
	}
}
