using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormTemplateVersion : DbTableCommonModel
	{
		public virtual int Version { get; set; }
		public virtual int Columns { get; set; }
		public virtual DbFormTemplate Template { get; set; }
	}
}
