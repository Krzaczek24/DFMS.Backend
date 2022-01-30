using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
    public class DbUserGroup : DbBaseModel
    {
        public virtual string Name { get; set; }
    }
}
