using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldValueMap : DbTableCommonModelMap<DbFormFieldValue>
    {
        public DbFormFieldValueMap(EntityTypeBuilder<DbFormFieldValue> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldValue> builder)
        {
            builder.ToTable("form_field_value");

            builder.HasOne(e => e.Form)
                .WithMany()
                .HasForeignKey("form_id")
                .HasConstraintName("fk_ffv_form");

            builder.HasOne(e => e.Field)
                .WithMany()
                .HasForeignKey("field_id")
                .HasConstraintName("fk_ffv_field");

            builder.Property(e => e.Date)
                .HasColumnName("date");

            builder.Property(e => e.String)
                .HasColumnName("string")
                .HasMaxLength(MAX_VARCHAR_LENGTH);

            builder.Property(e => e.Boolean)
                .HasColumnName("boolean");

            builder.Property(e => e.Integer)
                .HasColumnName("integer");

            builder.Property(e => e.Decimal)
                .HasColumnName("decimal")
                .HasPrecision(DECIMAL_PRECISION, DECIMAL_SCALE);
        }
    }
}
