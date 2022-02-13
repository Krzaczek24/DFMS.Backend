using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormFieldDefinition : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string Code { get; set; }
		public virtual bool Visible { get; set; }
	}
}
