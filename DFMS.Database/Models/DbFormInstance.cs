using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormInstance : DbTableCommonModel
	{
		public virtual string ReferenceNumber { get; set; }
		public virtual DbFormTemplateVersion Template { get; set; }
	}
}
