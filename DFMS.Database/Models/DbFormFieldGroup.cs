using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormFieldGroup : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual DbFormTemplate Template { get; set; }
	}
}
