using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbUserGroupMember : DbTableCommonModel
	{
		public virtual DbUserGroup Group { get; set; }
		public virtual DbUser User { get; set; }
	}
}
