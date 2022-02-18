using DFMS.Database.Models.Base;
using System.Collections.Generic;

namespace DFMS.Database.Models
{
	public class DbDictionary : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual string Value { get; set; }
		public virtual DbDictionary Parent { get; set; }
		public virtual IEnumerable<DbDictionary> Children { get; set; }
	}
}
