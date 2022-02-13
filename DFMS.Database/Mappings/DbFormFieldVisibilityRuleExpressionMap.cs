using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldVisibilityRuleExpressionMap : DbTableCommonModelMap<DbFormFieldVisibilityRuleExpression>
    {
        public DbFormFieldVisibilityRuleExpressionMap(EntityTypeBuilder<DbFormFieldVisibilityRuleExpression> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldVisibilityRuleExpression> builder)
        {
            builder.ToTable("form_field_visibility_rule_expression");

            builder.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey("parent_id");

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey("type_id")
                .IsRequired();
        }
    }
}
