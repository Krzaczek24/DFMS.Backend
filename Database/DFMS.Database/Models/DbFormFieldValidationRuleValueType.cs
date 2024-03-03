using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormFieldValidationRuleValueType : DbTableCommonModel
	{
		public virtual DbFormFieldValueType ValueType { get; set; }
		public virtual DbFormFieldValidationRuleDefinition ValidationDefinition { get; set; }
	}
}
