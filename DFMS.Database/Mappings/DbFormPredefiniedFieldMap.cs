using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormPredefiniedFieldMap : DbTableCommonModelMap<DbFormPredefiniedField>
    {
        public DbFormPredefiniedFieldMap(EntityTypeBuilder<DbFormPredefiniedField> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormPredefiniedField> builder)
        {
            builder.ToTable("form_predefinied_field");

            builder.HasOne(e => e.BaseDefinition)
                .WithMany()
                .HasForeignKey("base_definition_id")
                .IsRequired();

            builder.HasOne(e => e.ValueType)
                .WithMany()
                .HasForeignKey("value_type_id")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.Global)
                .HasColumnName("global")
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
