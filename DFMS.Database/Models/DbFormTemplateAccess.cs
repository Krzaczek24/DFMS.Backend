using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormTemplateAccess : DbTableCommonModel
	{
		public virtual string UserLogin { get; set; }
		public virtual bool View { get; set; }
		public virtual bool Edit { get; set; }
		public virtual bool Delete { get; set; }
		public virtual DbFormTemplateGroup TemplateGroup { get; set; }
		public virtual DbFormTemplate Template { get; set; }
		public virtual DbUserGroup UserGroup { get; set; }
	}
}
