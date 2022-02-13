using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormFieldValueType : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string Code { get; set; }
	}
}
