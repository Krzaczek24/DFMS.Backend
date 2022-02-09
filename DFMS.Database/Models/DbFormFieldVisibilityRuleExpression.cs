using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldVisibilityRuleExpression : DbTableCommonModel
	{
		public virtual DbFormFieldVisibilityRuleExpression Parent { get; set; }
		public virtual DbFormFieldVisibilityRuleExpressionType Type { get; set; }
	}
}
