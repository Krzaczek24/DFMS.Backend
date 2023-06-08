using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormCategoryMap : DbTableCommonModelMap<DbFormCategory>
    {
        public DbFormCategoryMap(EntityTypeBuilder<DbFormCategory> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormCategory> builder)
        {
            builder.ToTable("form_category");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(64);

            builder.HasOne(e => e.Parent)
                .WithMany()
                .HasForeignKey("parent_id")
                .HasConstraintName("fk_fc_parent");
        }
    }
}
