using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Models
{
	[DataContract]
	public class Line
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public double Amount { get; set; }

		[DataMember]
		public DateTime DateTime { get; set; }

		[DataMember]
		public string AccountName { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public string Category { get; set; }
	}
}
