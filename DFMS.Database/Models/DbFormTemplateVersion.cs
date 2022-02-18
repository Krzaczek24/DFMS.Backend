using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormTemplateVersion : DbTableCommonModel
	{
		public virtual int Version { get; set; }
		public virtual int? Columns { get; set; }
		public virtual DbFormTemplate Template { get; set; }
	}
}
