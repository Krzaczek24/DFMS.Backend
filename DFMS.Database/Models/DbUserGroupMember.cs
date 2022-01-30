﻿using DFMS.Database.Models.Base;

namespace DFMS.Database.Models
{
    public class DbUserGroupMember : DbBaseModel
    {
        public virtual string UserLogin { get; set; }
        public virtual DbUserGroup Group { get; set; }
    }
}
