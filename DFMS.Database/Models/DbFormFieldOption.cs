using DFMS.Database.Models.Base;
using System;

namespace DFMS.Database.Models
{
	internal class DbFormFieldOption : DbTableCommonModel
	{
		public virtual DateTime? Date { get; set; }
		public virtual string String { get; set; }
		public virtual bool? Boolean { get; set; }
		public virtual int? Integer { get; set; }
		public virtual decimal? Decimal { get; set; }
		public virtual DbFormTemplateField TemplateField { get; set; }
	}
}
