using DFMS.Database.Models.Base;
using System;

namespace DFMS.Database.Models
{
	internal class DbUserPermissionGroupAssignment : DbTableCommonModel
	{
		public virtual DateTime? ValidUntil { get; set; }
		public virtual DbUser User { get; set; }
		public virtual DbUserPermissionGroup PermissionGroup { get; set; }
	}
}
