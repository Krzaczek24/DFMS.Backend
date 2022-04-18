using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormInstance : DbTableCommonModel
	{
		public virtual string ReferenceNumber { get; set; }
		public virtual DbFormTemplateVersion Template { get; set; }
	}
}
