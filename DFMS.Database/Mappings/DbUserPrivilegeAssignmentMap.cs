using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserPrivilegeAssignmentMap : DbTableCommonModelMap<DbUserPrivilegeAssignment>
    {
        public DbUserPrivilegeAssignmentMap(EntityTypeBuilder<DbUserPrivilegeAssignment> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserPrivilegeAssignment> builder)
        {
            builder.ToTable("user_privilege_assignment");

            builder.HasOne(e => e.Privilege)
                .WithMany()
                .HasForeignKey("privilege_id")
                .HasConstraintName("fk_upa_privilege")
                .IsRequired();

            builder.HasOne(e => e.PrivilegeGroup)
                .WithMany()
                .HasForeignKey("privilege_group_id")
                .HasConstraintName("fk_upa_privilege_group");
        }
    }
}
