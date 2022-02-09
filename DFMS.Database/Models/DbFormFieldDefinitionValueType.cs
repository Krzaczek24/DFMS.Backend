using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldDefinitionValueType : DbTableCommonModel
	{
		public virtual DbFormValueType ValueType { get; set; }
		public virtual DbFormFieldDefinition FieldDefinition { get; set; }
	}
}
