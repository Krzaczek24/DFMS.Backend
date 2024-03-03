using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserPermissionMap : DbTableCommonModelMap<DbUserPermission>
    {
        public DbUserPermissionMap(EntityTypeBuilder<DbUserPermission> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserPermission> builder)
        {
            builder.ToTable("user_permission");

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
