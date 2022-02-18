using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldVisibilityRuleOperatorMap : DbTableCommonModelMap<DbFormFieldVisibilityRuleOperator>
    {
        public DbFormFieldVisibilityRuleOperatorMap(EntityTypeBuilder<DbFormFieldVisibilityRuleOperator> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldVisibilityRuleOperator> builder)
        {
            builder.ToTable("form_field_visibility_rule_operator");

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(e => e.Description)
               .HasColumnName("description")
               .HasMaxLength(256);

            builder.Property(e => e.Value)
               .HasColumnName("value")
               .IsRequired()
               .HasMaxLength(16);
        }
    }
}
