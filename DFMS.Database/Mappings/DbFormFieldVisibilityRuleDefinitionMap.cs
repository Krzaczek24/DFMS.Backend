using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldVisibilityRuleDefinitionMap : DbTableCommonModelMap<DbFormFieldVisibilityRuleDefinition>
    {
        public DbFormFieldVisibilityRuleDefinitionMap(EntityTypeBuilder<DbFormFieldVisibilityRuleDefinition> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldVisibilityRuleDefinition> builder)
        {
            builder.ToTable("form_field_visibility_rule_definition");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(64);

            builder.HasOne(e => e.Expression)
                .WithMany()
                .HasForeignKey("expression_id")
                .IsRequired();
        }
    }
}
