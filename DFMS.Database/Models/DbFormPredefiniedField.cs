using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormPredefiniedField : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual bool? Global { get; set; }
		public virtual DbFormFieldDefinition BaseDefinition { get; set; }
		public virtual DbFormFieldValueType ValueType { get; set; }
	}
}
