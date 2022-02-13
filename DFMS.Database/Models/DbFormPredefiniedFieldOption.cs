using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormPredefiniedFieldOption : DbTableCommonModel
	{
		public virtual System.DateTime? Date { get; set; }
		public virtual string String { get; set; }
		public virtual bool? Boolean { get; set; }
		public virtual int? Integer { get; set; }
		public virtual decimal? Decimal { get; set; }
		public virtual DbFormPredefiniedField PredefiniedField { get; set; }
	}
}
