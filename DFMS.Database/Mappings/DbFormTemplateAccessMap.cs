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
                .HasForeignKey("template_group_id");

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey("template_id");

            builder.HasOne(e => e.UserGroup)
                .WithMany()
                .HasForeignKey("user_group_id");

            builder.Property(e => e.UserLogin)
                .HasColumnName("user_login")
                .HasMaxLength(32);

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
