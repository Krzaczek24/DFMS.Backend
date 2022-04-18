using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbUserGroupMember : DbTableCommonModel
	{
		public virtual string UserLogin { get; set; }
		public virtual DbUserGroup Group { get; set; }
	}
}
