using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbUserSessionMap : DbTableCommonModelMap<DbUserSession>
    {
        public DbUserSessionMap(EntityTypeBuilder<DbUserSession> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbUserSession> builder)
        {
            builder.ToTable("user_session");

            builder.Property(e => e.RefreshToken)
                .HasColumnName("refresh_token")
                .HasMaxLength(1024);

            builder.Property(e => e.ClientIp)
                .HasColumnName("client_ip")
                .HasMaxLength(64);

            builder.Property(e => e.ValidUntil)
                .HasColumnName("valid_until");

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey("user_id")
                .HasConstraintName("fk_us_user")
                .IsRequired();
        }
    }
}
