using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormTemplateSectionMap : DbTableCommonModelMap<DbFormTemplateSection>
    {
        public DbFormTemplateSectionMap(EntityTypeBuilder<DbFormTemplateSection> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormTemplateSection> builder)
        {
            builder.ToTable("form_template_section");

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey("template_id")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(256);

            builder.Property(e => e.Sequence)
                .HasColumnName("sequence")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.OuterColumns)
                .HasColumnName("outer_columns")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.InnerColumns)
                .HasColumnName("inner_columns")
                .IsRequired()
                .HasDefaultValue(1);

        }
    }
}
