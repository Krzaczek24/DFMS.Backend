using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserPermissionGroupMap : DbTableCommonModelMap<DbUserPermissionGroup>
    {
        public DbUserPermissionGroupMap(EntityTypeBuilder<DbUserPermissionGroup> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserPermissionGroup> builder)
        {
            builder.ToTable("user_permission_group");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(1024);
        }
    }
}
