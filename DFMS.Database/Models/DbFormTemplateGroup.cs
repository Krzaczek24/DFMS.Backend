using DFMS.Database.Enums;
using DFMS.Database.Models.Base;
using System.Collections.Generic;

namespace DFMS.Database.Models
{
	public class DbFormTemplateGroup : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual string Code { get; set; }
		public virtual DbPrivacyLevel PrivacyLevel { get; set; }
		public virtual DbFormTemplateGroup Parent { get; set; }
		public virtual IEnumerable<DbFormTemplateGroup> Children { get; set; }
	}
}
