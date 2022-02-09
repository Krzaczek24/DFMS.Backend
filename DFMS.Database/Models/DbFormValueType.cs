using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormValueType : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string Code { get; set; }
	}
}
