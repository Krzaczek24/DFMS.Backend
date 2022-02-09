using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldVisibilityRule : DbTableCommonModel
	{
		public virtual DbFormTemplateField Field { get; set; }
		public virtual DbFormTemplateSection Section { get; set; }
		public virtual DbFormFieldVisibilityRuleDefinition Rule { get; set; }
	}
}
