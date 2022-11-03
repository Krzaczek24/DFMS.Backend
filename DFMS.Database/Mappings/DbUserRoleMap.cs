using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserRoleMap : DbTableCommonModelMap<DbUserRole>
    {
        public DbUserRoleMap(EntityTypeBuilder<DbUserRole> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserRole> builder)
        {
            builder.ToTable("user_role");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
