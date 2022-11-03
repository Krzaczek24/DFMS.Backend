using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserPrivilegeMap : DbTableCommonModelMap<DbUserPrivilege>
    {
        public DbUserPrivilegeMap(EntityTypeBuilder<DbUserPrivilege> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserPrivilege> builder)
        {
            builder.ToTable("user_privilege");

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
