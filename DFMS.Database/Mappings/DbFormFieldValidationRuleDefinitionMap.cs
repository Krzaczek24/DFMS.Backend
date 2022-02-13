using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldValidationRuleDefinitionMap : DbTableCommonModelMap<DbFormFieldValidationRuleDefinition>
    {
        public DbFormFieldValidationRuleDefinitionMap(EntityTypeBuilder<DbFormFieldValidationRuleDefinition> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldValidationRuleDefinition> builder)
        {
            builder.ToTable("form_field_validation_rule_definition");

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(64);

            builder.HasOne(e => e.ValidationType)
                .WithMany()
                .HasForeignKey("validation_type_id");

            builder.Property(e => e.DefaultValue)
                .HasColumnName("default_value")
                .HasMaxLength(256);

            builder.Property(e => e.DefaultMessage)
                .HasColumnName("default_message")
                .HasMaxLength(256);

            builder.Property(e => e.EditableValue)
                .HasColumnName("editable_value")
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.EditableMessage)
                .HasColumnName("editable_message")
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.EditableMessage)
                .HasColumnName("editable_message")
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
