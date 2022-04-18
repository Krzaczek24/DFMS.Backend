using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormFieldValidationRuleDefinition : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string DefaultValue { get; set; }
		public virtual string DefaultMessage { get; set; }
		public virtual bool? EditableValue { get; set; }
		public virtual bool? EditableMessage { get; set; }
		public virtual bool? Global { get; set; }
		public virtual DbFormFieldValidationRuleType ValidationType { get; set; }
	}
}
