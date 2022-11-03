using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbUserPrivilegeAssignment : DbTableCommonModel
	{
		public virtual DbUserPrivilegeGroup PrivilegeGroup { get; set; }
		public virtual DbUserPrivilege Privilege { get; set; }
	}
}
