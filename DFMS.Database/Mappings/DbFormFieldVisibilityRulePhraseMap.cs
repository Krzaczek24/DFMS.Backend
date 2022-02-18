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
                .HasConstraintName("fk_ffvrp_expression")
                .IsRequired();

            builder.HasOne(e => e.Field)
                .WithMany()
                .HasForeignKey("field_id")
                .HasConstraintName("fk_ffvrp_field")
                .IsRequired();

            builder.HasOne(e => e.Operator)
                .WithMany()
                .HasForeignKey("operator_id")
                .HasConstraintName("fk_ffvrp_operator")
                .IsRequired();

            builder.HasOne(e => e.Option)
                .WithMany()
                .HasForeignKey("option_id")
                .HasConstraintName("fk_ffvrp_option");

            builder.Property(e => e.Value)
                .HasColumnName("value")
                .HasPrecision(DECIMAL_PRECISION, DECIMAL_SCALE);
        }
    }
}
