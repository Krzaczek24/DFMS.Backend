using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldVisibilityRulePhraseMap : DbTableCommonModelMap<DbFormFieldVisibilityRulePhrase>
    {
        public DbFormFieldVisibilityRulePhraseMap(EntityTypeBuilder<DbFormFieldVisibilityRulePhrase> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldVisibilityRulePhrase> builder)
        {
            builder.ToTable("form_field_visibility_rule_phrase");

            builder.HasOne(e => e.Expression)
                .WithMany()
                .HasForeignKey("expression_id")
                .IsRequired();

            builder.HasOne(e => e.Field)
                .WithMany()
                .HasForeignKey("field_id")
                .IsRequired();

            builder.HasOne(e => e.Operator)
                .WithMany()
                .HasForeignKey("operator_id")
                .IsRequired();

            builder.HasOne(e => e.Option)
                .WithMany()
                .HasForeignKey("option_id");

            builder.Property(e => e.Value)
                .HasColumnName("value")
                .HasPrecision(DECIMAL_PRECISION, DECIMAL_SCALE);
        }
    }
}
