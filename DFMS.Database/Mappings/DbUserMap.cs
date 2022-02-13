using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserMap : DbTableCommonModelMap<DbUser>
    {
        public DbUserMap(EntityTypeBuilder<DbUser> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder.ToTable("user");

            builder.Property(e => e.Login)
                .HasColumnName("login")
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
