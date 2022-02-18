using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormInstanceMap : DbTableCommonModelMap<DbFormInstance>
    {
        public DbFormInstanceMap(EntityTypeBuilder<DbFormInstance> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormInstance> builder)
        {
            builder.ToTable("form_instance");

            builder.Property(e => e.ReferenceNumber)
                .HasColumnName("reference_number")
                .IsRequired()
                .HasMaxLength(16);

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey("template_id")
                .HasConstraintName("fk_fi_template")
                .IsRequired();
        }
    }
}
