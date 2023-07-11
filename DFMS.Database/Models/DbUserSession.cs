using DFMS.Database.Models.Base;
using System;

namespace DFMS.Database.Models
{
	internal class DbUserSession : DbTableCommonModel
	{
		public virtual string RefreshToken { get; set; }
		public virtual string ClientIp { get; set; }
		public virtual DateTime? ValidUntil { get; set; }
		public virtual DbUser User { get; set; }
	}
}
