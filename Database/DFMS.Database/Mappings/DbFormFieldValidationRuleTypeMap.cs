using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldValidationRuleTypeMap : DbTableCommonModelMap<DbFormFieldValidationRuleType>
    {
        public DbFormFieldValidationRuleTypeMap(EntityTypeBuilder<DbFormFieldValidationRuleType> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldValidationRuleType> builder)
        {
            builder.ToTable("form_field_validation_rule_type");

            builder.Property(e => e.Code)
                .HasColumnName("code")
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}
