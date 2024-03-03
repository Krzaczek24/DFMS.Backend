using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbWorkspace : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual bool? Public { get; set; }
	}
}
