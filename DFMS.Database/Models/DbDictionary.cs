using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
    public class DbDictionary : DbBaseModel
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Value { get; set; }
        public virtual DbDictionary Parent { get; set; }
    }
}
