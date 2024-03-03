using Core.Database.Models;
using System;
using System.Collections.Generic;

namespace DFMS.Database.Models.Base
{
	internal abstract class DbTableCommonModel : IDbTableCommonModel
	{
		public virtual int Id { get; set; }
		public virtual string AddLogin { get; set; }
		public virtual DateTime? AddDate { get; set; }
		public virtual string? ModifLogin { get; set; }
		public virtual DateTime? ModifDate { get; set; }
		public virtual bool? Active { get; set; }

        public bool IsActive() => Active.HasValue && Active.Value;

        public void SetModifDate(DateTime modifDate)
        {
            ModifDate = modifDate;
        }

        public void SetModifLogin(string modifLogin)
        {
			ModifLogin = modifLogin;
        }

		public static IEnumerable<string> UnmodifiableMembers { get; } = new[]
		{
			nameof(Id),
			nameof(AddLogin),
			nameof(AddDate),
			nameof(ModifDate)
		};
	}
}