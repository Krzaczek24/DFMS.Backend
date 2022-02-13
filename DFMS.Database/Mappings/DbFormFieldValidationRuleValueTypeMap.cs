using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldValidationRuleValueTypeMap : DbTableCommonModelMap<DbFormFieldValidationRuleValueType>
    {
        public DbFormFieldValidationRuleValueTypeMap(EntityTypeBuilder<DbFormFieldValidationRuleValueType> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldValidationRuleValueType> builder)
        {
            builder.ToTable("form_field_validation_rule_value_type");

            builder.HasOne(e => e.ValueType)
                .WithMany()
                .HasForeignKey("value_type_id")
                .IsRequired();

            builder.HasOne(e => e.ValidationDefinition)
                .WithMany()
                .HasForeignKey("validation_definition_id")
                .IsRequired();
        }
    }
}
