using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormTemplate : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string ReferenceNumberPattern { get; set; }
		public virtual DbFormCategory Category { get; set; }
		public virtual DbWorkspace Workspace { get; set; }
		public virtual DbFormTemplateVersion PublishedVersion { get; set; }
	}
}
