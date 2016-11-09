using SiteOnWheels.App.Data;
using SiteOnWheels.App.Data.Enums;
using SiteOnWheels.App.Data.Interfaces;
using SiteOnWheels.App.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.Extension.RSS
{
    public class RSSFeedGenerator : SiteOnWheelsExtension
    {
        public void AfterComplete(Item[] items)
        {
            this.Writer.Write("Begin writing RSS feed");

            StringBuilder builder = new StringBuilder();
            string frame = RSSResources.RSSItemFrame;
            foreach (Item item in items)
            {
                string rssItem = frame;
                rssItem = rssItem.Replace("[rss:title]", item.Title);
                rssItem = rssItem.Replace("[rss:filename]", item.NewFileName);
                rssItem = rssItem.Replace("[rss:added-on]", item.AddedOn.ToString("r"));
                rssItem = rssItem.Replace("[rss:content]", item.OriginalRenderedContents);
                builder.Append(rssItem);
            }
            string result = RSSResources.RSSFrame.Replace("[rss-items]", builder.ToString());
            result = result.ParseGlobalVariables();
            result = result.Replace("[current-rss-datetime]", DateTime.Now.ToString("r"));
            File.WriteAllText(string.Format(@"{0}{1}feed.xml", DataObject.OutputLocation, Path.DirectorySeparatorChar), result);

            this.Writer.Write("Finished writing RSS feed");
        }

        public string BeforeFileWrite(string content, FileType type)
        {
            return content.Replace("[rss-feed-url]", string.Format("{0}/feed.xml", DataObject.SiteObject.RootUrl)); ;
        }

        public void BeforePostsWrite(Item[] items)
        {
            
        }

        public string GetExtensionName()
        {
            return "RSS Feed Generator";
        }

        public void OnInit()
        {
            
        }

        public SiteOnWheelsWriter Writer { get; set; }
    }
}
