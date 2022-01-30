using DFMS.Database.Enums;
using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
    public class DbFormTemplateGroup : DbBaseModel
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual DbPrivacyLevel PrivacyLevel { get; set; }
        public virtual DbFormTemplateGroup Parent { get; set; }
    }
}
