using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbWorkspaceUser : DbTableCommonModel
	{
		public virtual DbUser User { get; set; }
		public virtual DbWorkspace Workspace { get; set; }
		public virtual DbUserRole Role { get; set; }
	}
}
