using DFMS.Database.Models.Base;
using System;

namespace DFMS.Database.Models
{
	internal class DbEvent : DbTableCommonModel
	{
		public virtual bool? IsProcessed { get; set; }
		public virtual string Data { get; set; }
		public virtual string WorkerName { get; set; }
		public virtual DateTime? ProcessingStarted { get; set; }
		public virtual DateTime? ProcessingEdned { get; set; }
		public virtual DbEventType Type { get; set; }
	}
}
