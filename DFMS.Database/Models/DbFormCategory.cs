using DFMS.Database.Models.Base;
using System.Collections.Generic;

namespace DFMS.Database.Models
{
	internal class DbFormCategory : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual DbFormCategory Parent { get; set; }
		public virtual IEnumerable<DbFormCategory> Children { get; set; }
	}
}
