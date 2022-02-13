using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldOptionMap : DbTableCommonModelMap<DbFormFieldOption>
    {
        public DbFormFieldOptionMap(EntityTypeBuilder<DbFormFieldOption> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldOption> builder)
        {
            builder.ToTable("form_field_option");

            builder.HasOne(e => e.TemplateField)
                .WithMany()
                .HasForeignKey("template_field_id");

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
