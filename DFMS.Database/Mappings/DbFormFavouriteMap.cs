using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFavouriteMap : DbTableCommonModelMap<DbFormFavourite>
    {
        public DbFormFavouriteMap(EntityTypeBuilder<DbFormFavourite> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFavourite> builder)
        {
            builder.ToTable("form_favourite");

            builder.Property(e => e.Sequence)
                .HasColumnName("sequence")
                .IsRequired();

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey("template_id")
                .HasConstraintName("fk_ff_template");
        }
    }
}
