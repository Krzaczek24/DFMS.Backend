using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbUserGroup : DbTableCommonModel
	{
		public virtual string Name { get; set; }
	}
}
