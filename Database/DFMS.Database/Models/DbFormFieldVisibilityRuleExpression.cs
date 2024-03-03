using DFMS.Database.Models.Base;
using System.Collections.Generic;

namespace DFMS.Database.Models
{
	internal class DbFormFieldVisibilityRuleExpression : DbTableCommonModel
	{
		public virtual DbFormFieldVisibilityRuleExpression Parent { get; set; }
		public virtual DbFormFieldVisibilityRuleExpressionType Type { get; set; }
		public virtual IEnumerable<DbFormFieldVisibilityRuleExpression> Children { get; set; }
	}
}
