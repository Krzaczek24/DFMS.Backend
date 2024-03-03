using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormTemplateAccessMap : DbTableCommonModelMap<DbFormTemplateAccess>
    {
        public DbFormTemplateAccessMap(EntityTypeBuilder<DbFormTemplateAccess> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormTemplateAccess> builder)
        {
            builder.ToTable("form_template_access");

            builder.HasOne(e => e.FormCategory)
                .WithMany()
                .HasForeignKey("form_category_id")
                .HasConstraintName("fk_fta_form_category");

            builder.HasOne(e => e.UserGroup)
                .WithMany()
                .HasForeignKey("user_group_id")
                .HasConstraintName("fk_fta_user_group");

            builder.Property(e => e.Use)
                .HasColumnName("use")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.View)
                .HasColumnName("view")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.Edit)
                .HasColumnName("edit")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.Delete)
                .HasColumnName("delete")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.Publish)
                .HasColumnName("publish")
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
