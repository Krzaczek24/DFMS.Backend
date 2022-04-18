using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
	internal class DbFormTemplate : DbTableCommonModel
	{
		public virtual string Title { get; set; }
		public virtual string ReferenceNumberPattern { get; set; }
		public virtual int? PublishedVersion { get; set; }
		public virtual int? PrivacyLevel { get; set; }
		public virtual DbFormTemplateGroup Group { get; set; }
	}
}
