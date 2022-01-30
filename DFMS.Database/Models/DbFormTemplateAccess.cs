using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
    public class DbFormTemplateAccess : DbBaseModel
    {
        public virtual DbFormTemplateGroup TemplateGroup { get; set; }
        public virtual DbFormTemplate Template { get; set; }
        public virtual DbUserGroup UserGroup { get; set; }
        public virtual string UserLogin { get; set; }
        public virtual bool View { get; set; }
        public virtual bool Edit { get; set; }
        public virtual bool Delete { get; set; }
    }
}
