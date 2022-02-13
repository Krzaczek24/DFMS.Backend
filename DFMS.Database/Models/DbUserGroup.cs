using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbUserGroup : DbTableCommonModel
	{
		public virtual string Name { get; set; }
	}
}
