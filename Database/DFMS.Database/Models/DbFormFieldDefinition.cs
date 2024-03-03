using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormFieldDefinition : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string Type { get; set; }
		public virtual bool? Visible { get; set; }
	}
}
