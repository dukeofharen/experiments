using SiteOnWheels.App.Data;
using SiteOnWheels.App.Data.Enums;
using SiteOnWheels.App.Data.Exceptions;
using SiteOnWheels.App.Data.Interfaces;
using SiteOnWheels.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.Extension.Disqus
{
	public class Disqus : SiteOnWheelsExtension
	{
		private SiteOnWheelsWriter _writer;

		public void AfterComplete(Item[] items)
		{

		}

		public void BeforePostsWrite(Item[] items)
		{

		}

		public string GetExtensionName()
		{
			return "Disqus for Site On Wheels";
		}

		public void OnInit()
		{

		}

		public string BeforeFileWrite(string content, FileType type)
		{
			if (!DataObject.SiteObject.Variables.ContainsKey("disqus_code"))
			{
				throw new SOWArgumentException("'disqus_code' is not defined in the site.json file");
			}
			string disqusCode = DataObject.SiteObject.Variables["disqus_code"];
			return content.Replace("[disqus]", DisqusResources.JSFrame.Replace("[disqus-code]", disqusCode));
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
