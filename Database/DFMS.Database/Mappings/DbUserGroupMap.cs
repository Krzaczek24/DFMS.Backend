using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserGroupMap : DbTableCommonModelMap<DbUserGroup>
    {
        public DbUserGroupMap(EntityTypeBuilder<DbUserGroup> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserGroup> builder)
        {
            builder.ToTable("user_group");

            builder.HasOne(e => e.Workspace)
                .WithMany()
                .HasForeignKey("workspace_id")
                .HasConstraintName("fk_ug_workspace")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}
