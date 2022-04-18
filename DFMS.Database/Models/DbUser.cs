using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbUser : DbTableCommonModel
	{
		public virtual string Login { get; set; }
	}
}
