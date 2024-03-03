using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbEventType : DbTableCommonModel
	{
		public virtual string Name { get; set; }
	}
}
