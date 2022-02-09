using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldVisibilityRulePhrase : DbTableCommonModel
	{
		public virtual int FieldId { get; set; }
		public virtual DbFormFieldVisibilityRuleExpression Expression { get; set; }
		public virtual DbFormFieldVisibilityRuleOperator Operator { get; set; }
		public virtual DbFormFieldOption Option { get; set; }
	}
}
