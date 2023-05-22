using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbWorkspaceUserMap : DbTableCommonModelMap<DbWorkspaceUser>
    {
        public DbWorkspaceUserMap(EntityTypeBuilder<DbWorkspaceUser> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbWorkspaceUser> builder)
        {
            builder.ToTable("workspace_user");

            builder.HasOne(e => e.Workspace)
                .WithMany()
                .HasForeignKey("workspace_id")
                .HasConstraintName("fk_wu_workspace");

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey("user_id")
                .HasConstraintName("fk_wu_user");

            builder.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey("role_id")
                .HasConstraintName("fk_wu_role");
        }
    }
}
