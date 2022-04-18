using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormFavourite : DbTableCommonModel
	{
		public virtual int Sequence { get; set; }
		public virtual DbFormTemplate Template { get; set; }
	}
}
