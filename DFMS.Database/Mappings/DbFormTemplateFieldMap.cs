using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormTemplateFieldMap : DbTableCommonModelMap<DbFormTemplateField>
    {
        public DbFormTemplateFieldMap(EntityTypeBuilder<DbFormTemplateField> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormTemplateField> builder)
        {
            builder.ToTable("form_template_field");

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Code)
                .HasColumnName("code")
                .HasMaxLength(32);

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(MAX_VARCHAR_LENGTH);

            builder.Property(e => e.Sequence)
                .HasColumnName("sequence")
                .IsRequired();

            builder.HasOne(e => e.FieldDefinition)
                .WithMany()
                .HasForeignKey("field_definition_id")
                .HasConstraintName("fk_ftf_field_definition")
                .IsRequired();

            builder.HasOne(e => e.TemplateSection)
                .WithMany()
                .HasForeignKey("template_section_id")
                .HasConstraintName("fk_ftf_template_section")
                .IsRequired();

            builder.HasOne(e => e.FieldGroup)
                .WithMany()
                .HasForeignKey("field_group_id")
                .HasConstraintName("fk_ftf_field_group");

            builder.HasOne(e => e.ValueType)
                .WithMany()
                .HasForeignKey("value_type_id")
                .HasConstraintName("fk_ftf_value_type")
                .IsRequired();
        }
    }
}
