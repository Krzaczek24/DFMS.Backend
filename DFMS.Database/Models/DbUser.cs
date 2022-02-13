using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbUser : DbTableCommonModel
	{
		public virtual string Login { get; set; }
	}
}
