using DFMS.Database.Models.Base;
using System.Collections.Generic;

namespace DFMS.Database.Models
{
	internal class DbFormTemplateGroup : DbTableCommonModel
	{
		public virtual string Name { get; set; }
		public virtual string Code { get; set; }
		public virtual int? PrivacyLevel { get; set; }
		public virtual DbFormTemplateGroup Parent { get; set; }
		public virtual IEnumerable<DbFormTemplateGroup> Children { get; set; }
	}
}
