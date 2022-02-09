using DFMS.Database.Base;

namespace DFMS.Database
{
	public class DbDictionary : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual string Value { get; set; }
		public virtual DbDictionary Parent { get; set; }
	}
}
