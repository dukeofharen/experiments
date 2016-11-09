using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data.Models
{
	public class Item
	{
		private string[] _tags;

		public string FilePath { get; set; }
		public string NewFileName { get; set; }
		public string Title { get; set; }
		public string Contents { get; set; }
		public string OriginalRenderedContents { get; set; }
		public string RenderedContents { get; set; }
		public string Preview
		{
			get
			{
				string contents = Regex.Replace(this.OriginalRenderedContents, "<[^>]*(>|$)", string.Empty);
				contents = contents.Replace(Environment.NewLine, string.Empty);
				if (contents.Length >= DataObject.SiteObject.PostPreviewLength)
				{
					contents = string.Format("{0}...", contents.Substring(0, DataObject.SiteObject.PostPreviewLength));
				}
				return contents;
			}
		}
		public string Author { get; set; }
		public DateTime AddedOn { get; set; }
		public DateTime? ChangedOn { get; set; }
		public string[] Tags
		{
			get
			{
				if (this._tags == null)
				{
					return new string[0];
				}
				return this._tags;
			}
			set
			{
				this._tags = value;
			}
		}
		public bool IsPost { get; set; }
		public string PreviewImage { get; set; }
	}
}
