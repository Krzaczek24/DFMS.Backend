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

            builder.HasOne(e => e.Group)
                .WithMany()
                .HasForeignKey("group_id")
                .HasConstraintName("fk_ft_group")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.ReferenceNumberPattern)
                .HasColumnName("reference_number_pattern")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(e => e.PublishedVersion)
                .HasColumnName("published_version");

            builder.Property(e => e.PrivacyLevel)
                .HasColumnName("privacy_level")
                .IsRequired()
                .HasConversion<int>();
        }
    }
}
