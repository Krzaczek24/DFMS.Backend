using DFMS.Database.Models.Base;
using System;

namespace DFMS.Database.Models
{
	internal class DbUserPrivilegeGroupAssignment : DbTableCommonModel
	{
		public virtual DateTime? ValidUntil { get; set; }
		public virtual DbUser User { get; set; }
		public virtual DbUserPrivilegeGroup PrivilegeGroup { get; set; }
	}
}
