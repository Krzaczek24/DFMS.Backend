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

            builder.Property(e => e.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired()
                .HasMaxLength(2048);

            builder.Property(e => e.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(64);

            builder.Property(e => e.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(64);

            builder.Property(e => e.LastLoginDate)
                .HasColumnName("last_login_date");

            builder.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey("role_id")
                .HasConstraintName("fk_u_role")
                .IsRequired();
        }
    }
}
