using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbUserPrivilege : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
	}
}
