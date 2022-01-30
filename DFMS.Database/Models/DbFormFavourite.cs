using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
    public class DbFormFavourite : DbBaseModel
    {
        public virtual int Sequence { get; set; }
        public virtual DbFormTemplate Template { get; set; }
    }
}
