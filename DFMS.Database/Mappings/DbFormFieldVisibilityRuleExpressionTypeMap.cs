using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldVisibilityRuleExpressionTypeMap : DbTableCommonModelMap<DbFormFieldVisibilityRuleExpressionType>
    {
        public DbFormFieldVisibilityRuleExpressionTypeMap(EntityTypeBuilder<DbFormFieldVisibilityRuleExpressionType> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldVisibilityRuleExpressionType> builder)
        {
            builder.ToTable("form_field_visibility_rule_expression_type");

            builder.Property(e => e.Title)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(e => e.Description)
               .HasColumnName("name")
               .HasMaxLength(256);

            builder.Property(e => e.Value)
               .HasColumnName("name")
               .IsRequired()
               .HasMaxLength(16);
        }
    }
}
