using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserGroupMemberMap : DbTableCommonModelMap<DbUserGroupMember>
    {
        public DbUserGroupMemberMap(EntityTypeBuilder<DbUserGroupMember> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserGroupMember> builder)
        {
            builder.ToTable("user_group_member");

            builder.HasOne(e => e.Group)
                .WithMany()
                .HasForeignKey("group_id")
                .HasConstraintName("fk_ugm_group")
                .IsRequired();

            builder.Property(e => e.UserLogin)
                .HasColumnName("user_login")
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
