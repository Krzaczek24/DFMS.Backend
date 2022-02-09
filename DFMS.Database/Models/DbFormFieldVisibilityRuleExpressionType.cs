using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldVisibilityRuleExpressionType : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual string Value { get; set; }
	}
}
