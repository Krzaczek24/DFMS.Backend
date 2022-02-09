using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldValidationRule : DbTableCommonModel
	{
		public virtual string Value { get; set; }
		public virtual string Message { get; set; }
		public virtual DbFormTemplateField Field { get; set; }
		public virtual DbFormFieldValidationRuleDefinition ValidationRule { get; set; }
	}
}
