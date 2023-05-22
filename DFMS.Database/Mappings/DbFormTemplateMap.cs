using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormTemplateMap : DbTableCommonModelMap<DbFormTemplate>
    {
        public DbFormTemplateMap(EntityTypeBuilder<DbFormTemplate> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormTemplate> builder)
        {
            builder.ToTable("form_template");

            builder.HasOne(e => e.Workspace)
                .WithMany()
                .HasForeignKey("workspace_id")
                .HasConstraintName("fk_ft_workspace")
                .IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey("category_id")
                .HasConstraintName("fk_ft_category")
                .IsRequired();

            builder.HasOne(e => e.PublishedVersion)
                .WithMany()
                .HasForeignKey("published_version_id")
                .HasConstraintName("fk_ft_published_version");

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.ReferenceNumberPattern)
                .HasColumnName("reference_number_pattern")
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}
