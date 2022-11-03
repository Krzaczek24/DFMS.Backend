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

            builder.HasOne(e => e.TemplateGroup)
                .WithMany()
                .HasForeignKey("template_group_id")
                .HasConstraintName("fk_fta_template_group");

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey("template_id")
                .HasConstraintName("fk_fta_template");

            builder.HasOne(e => e.UserGroup)
                .WithMany()
                .HasForeignKey("user_group_id")
                .HasConstraintName("fk_fta_user_group");

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey("user_id")
                .HasConstraintName("fk_fta_user");

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
        }
    }
}
