namespace DFMS.Database.Base
{
	public abstract class DbTableCommonModel
	{
		public virtual int Id { get; set; }
		public virtual string AddLogin { get; set; }
		public virtual System.DateTime AddDate { get; set; }
		public virtual string ModifLogin { get; set; }
		public virtual System.DateTime? ModifDate { get; set; }
		public virtual bool Active { get; set; }
	}
}
