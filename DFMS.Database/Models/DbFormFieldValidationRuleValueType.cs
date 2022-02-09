using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldValidationRuleValueType : DbTableCommonModel
	{
		public virtual DbFormValueType ValueType { get; set; }
		public virtual DbFormFieldValidationRuleDefinition ValidationDefinition { get; set; }
	}
}
