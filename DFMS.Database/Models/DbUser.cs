using DFMS.Database.Models.Base;
using System;

namespace DFMS.Database.Models
{
	internal class DbUser : DbTableCommonModel
	{
		public virtual DateTime? LastLoginDate { get; set; }
		public virtual string Login { get; set; }
		public virtual string PasswordHash { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string Email { get; set; }
		public virtual DbUserRole Role { get; set; }
	}
}
