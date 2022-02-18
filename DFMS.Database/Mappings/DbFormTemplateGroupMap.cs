using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormTemplateGroupMap : DbTableCommonModelMap<DbFormTemplateGroup>
    {
        public DbFormTemplateGroupMap(EntityTypeBuilder<DbFormTemplateGroup> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormTemplateGroup> builder)
        {
            builder.ToTable("form_template_group");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.Code)
                .HasColumnName("code")
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(e => e.PrivacyLevel)
                .HasColumnName("privacy_level")
                .IsRequired()
                .HasConversion<int>();

            builder.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey("parent_id")
                .HasConstraintName("fk_ftg_parent");
        }
    }
}
