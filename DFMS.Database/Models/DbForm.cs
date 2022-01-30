using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
    public class DbForm : DbBaseModel
    {
        public virtual string ReferenceNumber { get; set; }
        public virtual DbFormTemplate Template { get; set; }
    }
}
