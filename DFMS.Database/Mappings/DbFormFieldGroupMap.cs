using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldGroupMap : DbTableCommonModelMap<DbFormFieldGroup>
    {
        public DbFormFieldGroupMap(EntityTypeBuilder<DbFormFieldGroup> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldGroup> builder)
        {
            builder.ToTable("form_field_group");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(64);

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey("template_id")
                .IsRequired();
        }
    }
}
