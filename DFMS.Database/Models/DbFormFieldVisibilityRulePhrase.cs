using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormFieldVisibilityRulePhrase : DbTableCommonModel
	{
		public virtual decimal? Value { get; set; }
		public virtual DbFormFieldVisibilityRuleExpression Expression { get; set; }
		public virtual DbFormFieldVisibilityRuleOperator Operator { get; set; }
		public virtual DbFormFieldOption Option { get; set; }
		public virtual DbFormTemplateField Field { get; set; }
	}
}
