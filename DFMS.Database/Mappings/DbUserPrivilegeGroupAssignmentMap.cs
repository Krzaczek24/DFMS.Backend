using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserPrivilegeGroupAssignmentMap : DbTableCommonModelMap<DbUserPrivilegeGroupAssignment>
    {
        public DbUserPrivilegeGroupAssignmentMap(EntityTypeBuilder<DbUserPrivilegeGroupAssignment> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserPrivilegeGroupAssignment> builder)
        {
            builder.ToTable("user_privilege_group_assignment");

            builder.HasOne(e => e.PrivilegeGroup)
                .WithMany()
                .HasForeignKey("privilege_group_id")
                .HasConstraintName("fk_upga_privilege_group")
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
