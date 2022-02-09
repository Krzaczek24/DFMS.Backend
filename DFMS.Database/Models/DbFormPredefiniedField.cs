using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormPredefiniedField : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual bool Global { get; set; }
		public virtual DbFormFieldDefinition BaseDefinition { get; set; }
		public virtual DbFormValueType ValueType { get; set; }
	}
}
