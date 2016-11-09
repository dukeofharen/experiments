using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHq.Web.Entities.Enums;

namespace TutorialHq.Web.Entities
{
	[Table("users")]
	public class User : EntityBase, IUser<int>
	{
		[Key]
		[Column("user_id")]
		public override int Id { get; set; }

		[Required]
		[Column("user_name")]
		[StringLength(50)]
		[Index("UserNameUnique", IsUnique = true)]
		public string UserName { get; set; }

		[Required]
		[Column("password_hash")]
		[StringLength(200)]
		public string PasswordHash { get; set; }

		[Required]
		[Column("email")]
		[StringLength(100)]
		[Index("EmailUnique", IsUnique = true)]
		public string Email { get; set; }

		[Required]
		[Column("last_login")]
		public DateTime LastLogin { get; set; }

		[Required]
		[Column("user_role")]
		public UserRole UserRole { get; set; }

		[Column("name")]
		[StringLength(100)]
		public string Name { get; set; }

		[Column("location")]
		[StringLength(100)]
		public string Location { get; set; }

		[Column("website")]
		[StringLength(100)]
		public string Website { get; set; }

		[Required]
		[Column("activated")]
		public bool Activated { get; set; }

		[Required]
		[Column("activation_code")]
		public string ActivationCode { get; set; }

		public ICollection<Tutorial> Tutorials { get; set; }

		public ICollection<Comment> Comments { get; set; }

		public ICollection<Vote> Votes { get; set; }

		public ICollection<Click> Clicks { get; set; }
	}
}
