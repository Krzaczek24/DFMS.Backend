using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldGroup : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual DbFormTemplate Template { get; set; }
	}
}
