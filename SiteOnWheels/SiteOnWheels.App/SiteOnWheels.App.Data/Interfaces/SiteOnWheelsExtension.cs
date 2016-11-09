using SiteOnWheels.App.Data.Enums;
using SiteOnWheels.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data.Interfaces
{
	public interface SiteOnWheelsExtension
	{
		SiteOnWheelsWriter Writer { get; set; }
		string GetExtensionName();
		void OnInit();
		void BeforePostsWrite(Item[] items);
		string BeforeFileWrite(string content, FileType type);
		void AfterComplete(Item[] items);
	}
}
