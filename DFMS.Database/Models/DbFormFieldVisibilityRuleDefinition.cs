using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormFieldVisibilityRuleDefinition : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual DbFormFieldVisibilityRuleExpression Expression { get; set; }
	}
}
