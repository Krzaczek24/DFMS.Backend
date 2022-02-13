using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	public class DbFormInstance : DbTableCommonModel
	{
		public virtual string ReferenceNumber { get; set; }
		public virtual DbFormTemplateVersion Template { get; set; }
	}
}
