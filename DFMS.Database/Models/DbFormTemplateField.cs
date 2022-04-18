using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormTemplateField : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string Code { get; set; }
		public virtual string Description { get; set; }
		public virtual int Sequence { get; set; }
		public virtual DbFormFieldDefinition FieldDefinition { get; set; }
		public virtual DbFormTemplateSection TemplateSection { get; set; }
		public virtual DbFormFieldGroup FieldGroup { get; set; }
		public virtual DbFormFieldValueType ValueType { get; set; }
	}
}
