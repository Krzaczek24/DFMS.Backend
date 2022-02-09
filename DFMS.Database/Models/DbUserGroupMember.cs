using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbUserGroupMember : DbTableCommonModel
	{
		public virtual string UserLogin { get; set; }
		public virtual DbUserGroup Group { get; set; }
	}
}
