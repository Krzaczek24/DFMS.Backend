using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormTemplateAccess : DbTableCommonModel
	{
		public virtual bool? Use { get; set; }
		public virtual bool? View { get; set; }
		public virtual bool? Edit { get; set; }
		public virtual bool? Delete { get; set; }
		public virtual bool? Publish { get; set; }
		public virtual DbUserGroup UserGroup { get; set; }
		public virtual DbFormCategory FormCategory { get; set; }
	}
}
