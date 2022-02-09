using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldVisibilityRuleDefinition : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual DbFormFieldVisibilityRuleExpression Expression { get; set; }
	}
}
