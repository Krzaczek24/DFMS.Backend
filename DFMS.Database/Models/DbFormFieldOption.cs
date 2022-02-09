using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFieldOption : DbTableCommonModel
	{
		public virtual System.DateTime? Date { get; set; }
		public virtual string String { get; set; }
		public virtual bool? Boolean { get; set; }
		public virtual int? Integer { get; set; }
		public virtual DbFormTemplateField TemplateField { get; set; }
	}
}
