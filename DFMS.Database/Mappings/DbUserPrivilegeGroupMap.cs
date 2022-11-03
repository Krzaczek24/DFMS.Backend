using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserPrivilegeGroupMap : DbTableCommonModelMap<DbUserPrivilegeGroup>
    {
        public DbUserPrivilegeGroupMap(EntityTypeBuilder<DbUserPrivilegeGroup> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserPrivilegeGroup> builder)
        {
            builder.ToTable("user_privilege_group");

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
