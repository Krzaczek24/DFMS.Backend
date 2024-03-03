using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormTemplateVersionMap : DbTableCommonModelMap<DbFormTemplateVersion>
    {
        public DbFormTemplateVersionMap(EntityTypeBuilder<DbFormTemplateVersion> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormTemplateVersion> builder)
        {
            builder.ToTable("form_template_version");

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey("template_id")
                .HasConstraintName("fk_fts_template")
                .IsRequired();

            builder.Property(e => e.Version)
                .HasColumnName("version")
                .IsRequired();

            builder.Property(e => e.Columns)
                .HasColumnName("columns")
                .IsRequired()
                .HasDefaultValue(1);
        }
    }
}
