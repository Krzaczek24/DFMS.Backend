using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormTemplateGroup : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual string Code { get; set; }
		public virtual int PrivacyLevel { get; set; }
		public virtual DbFormTemplateGroup Parent { get; set; }
	}
}
