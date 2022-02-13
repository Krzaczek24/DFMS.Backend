using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldVisibilityRuleMap : DbTableCommonModelMap<DbFormFieldVisibilityRule>
    {
        public DbFormFieldVisibilityRuleMap(EntityTypeBuilder<DbFormFieldVisibilityRule> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldVisibilityRule> builder)
        {
            builder.ToTable("form_field_visibility_rule");

            builder.HasOne(e => e.Field)
                .WithMany()
                .HasForeignKey("field_id");

            builder.HasOne(e => e.Section)
                .WithMany()
                .HasForeignKey("section_id");

            builder.HasOne(e => e.Rule)
                .WithMany()
                .HasForeignKey("rule_id")
                .IsRequired();
        }
    }
}
