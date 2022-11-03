using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbUserPermissionAssignment : DbTableCommonModel
	{
		public virtual DbUserPermissionGroup PermissionGroup { get; set; }
		public virtual DbUserPermission Permission { get; set; }
	}
}
