using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data.Models
{
	[DataContract]
	public class MenuItem
	{
		[DataMember(Name = "text")]
		public string Text { get; set; }

		[DataMember(Name = "link")]
		public string Link { get; set; }
	}
}
