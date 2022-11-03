using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserPermissionGroupAssignmentMap : DbTableCommonModelMap<DbUserPermissionGroupAssignment>
    {
        public DbUserPermissionGroupAssignmentMap(EntityTypeBuilder<DbUserPermissionGroupAssignment> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserPermissionGroupAssignment> builder)
        {
            builder.ToTable("user_permission_group_assignment");

            builder.HasOne(e => e.PermissionGroup)
                .WithMany()
                .HasForeignKey("permission_group_id")
                .HasConstraintName("fk_upga_permission_group")
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey("user_id")
                .HasConstraintName("fk_upga_user");

            builder.Property(e => e.ValidUntil)
                .HasColumnName("valid_until");
        }
    }
}
