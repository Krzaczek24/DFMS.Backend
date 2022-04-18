using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormFieldVisibilityRuleOperator : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual string Value { get; set; }
	}
}
