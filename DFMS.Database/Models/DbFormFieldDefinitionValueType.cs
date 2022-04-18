using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormFieldDefinitionValueType : DbTableCommonModel
	{
		public virtual DbFormFieldValueType ValueType { get; set; }
		public virtual DbFormFieldDefinition FieldDefinition { get; set; }
	}
}
