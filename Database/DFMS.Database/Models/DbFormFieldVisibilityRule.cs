using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormFieldVisibilityRule : DbTableCommonModel
	{
		public virtual DbFormTemplateField Field { get; set; }
		public virtual DbFormTemplateSection Section { get; set; }
		public virtual DbFormFieldVisibilityRuleDefinition Rule { get; set; }
	}
}
