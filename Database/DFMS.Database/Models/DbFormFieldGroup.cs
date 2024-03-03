using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormFieldGroup : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual DbFormTemplate Template { get; set; }
	}
}
