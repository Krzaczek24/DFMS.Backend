using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbFormFavourite : DbTableCommonModel
	{
		public virtual int Sequence { get; set; }
		public virtual DbFormTemplate Template { get; set; }
	}
}
