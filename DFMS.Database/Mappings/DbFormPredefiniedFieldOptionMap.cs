using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormPredefiniedFieldOptionMap : DbTableCommonModelMap<DbFormPredefiniedFieldOption>
    {
        public DbFormPredefiniedFieldOptionMap(EntityTypeBuilder<DbFormPredefiniedFieldOption> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormPredefiniedFieldOption> builder)
        {
            builder.ToTable("form_predefinied_field_option");

            builder.HasOne(e => e.PredefiniedField)
                .WithMany()
                .HasForeignKey("predefinied_field_id")
                .HasConstraintName("fk_fpfo_predefinied_field");

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
