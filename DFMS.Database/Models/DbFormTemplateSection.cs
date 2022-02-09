using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormTemplateSection : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual int Sequence { get; set; }
		public virtual int OuterColumns { get; set; }
		public virtual int InnerColumns { get; set; }
		public virtual DbFormTemplateVersion Template { get; set; }
	}
}
