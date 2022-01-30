using DFMS.Database.Enums;
using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
    public class DbFormTemplate : DbBaseModel
    {
        public virtual string Title { get; set; }
        public virtual string ReferenceNumberPattern { get; set; }
        public virtual int? PublishedVersion { get; set; }
        public virtual DbPrivacyLevel PrivacyLevel { get; set; }
        public virtual DbFormTemplateGroup Group { get; set; }
    }
}
