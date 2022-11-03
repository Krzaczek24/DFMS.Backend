using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbUserRole : DbTableCommonModel
	{
		public virtual string Name { get; set; }
	}
}
