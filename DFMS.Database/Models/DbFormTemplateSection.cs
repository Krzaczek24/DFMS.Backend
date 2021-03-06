using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormTemplateSection : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual int Sequence { get; set; }
		public virtual int? OuterColumns { get; set; }
		public virtual int? InnerColumns { get; set; }
		public virtual DbFormTemplateVersion Template { get; set; }
	}
}
