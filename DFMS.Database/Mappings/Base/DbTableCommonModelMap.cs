using DFMS.Database.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings.Base
{
    internal abstract class DbTableCommonModelMap<T> : IEntityTypeConfiguration<T> where T : DbTableCommonModel
    {
        internal const string CURRENT_TIMESTAMP = "CURRENT_TIMESTAMP";
        internal const int MAX_VARCHAR_LENGTH = 4000;
        internal const int DECIMAL_PRECISION = 9;
        internal const int DECIMAL_SCALE = 3;

        internal DbTableCommonModelMap(EntityTypeBuilder<T> builder)
        {
            builder.HasKey("Id");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);

            builder.Property(e => e.AddLogin)
                .HasColumnName("add_login")
                .IsRequired()
                .HasMaxLength(32);
                //.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);

            builder.Property(e => e.AddDate)
                .HasColumnName("add_date")
                .IsRequired()
                .HasDefaultValueSql(CURRENT_TIMESTAMP)
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);

            builder.Property(e => e.ModifLogin)
                .HasColumnName("modif_login")
                .HasMaxLength(32);
                //.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);

            builder.Property(e => e.ModifDate)
                .HasColumnName("modif_date")
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);

            builder.Property(e => e.Active)
                .HasColumnName("active")
                .IsRequired()
                .HasDefaultValue(true);
        }

        public abstract void Configure(EntityTypeBuilder<T> builder);
    }
}
