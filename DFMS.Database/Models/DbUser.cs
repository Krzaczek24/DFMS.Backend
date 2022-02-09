using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbUser : DbTableCommonModel
	{
		public virtual string Login { get; set; }
	}
}
