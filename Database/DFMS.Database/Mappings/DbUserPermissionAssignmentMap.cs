using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserPermissionAssignmentMap : DbTableCommonModelMap<DbUserPermissionAssignment>
    {
        public DbUserPermissionAssignmentMap(EntityTypeBuilder<DbUserPermissionAssignment> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserPermissionAssignment> builder)
        {
            builder.ToTable("user_permission_assignment");

            builder.HasOne(e => e.Permission)
                .WithMany()
                .HasForeignKey("permission_id")
                .HasConstraintName("fk_upa_permission")
                .IsRequired();

            builder.HasOne(e => e.PermissionGroup)
                .WithMany()
                .HasForeignKey("permission_group_id")
                .HasConstraintName("fk_upa_permission_group");
        }
    }
}
