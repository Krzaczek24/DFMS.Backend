using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbDictionaryMap : DbTableCommonModelMap<DbDictionary>
    {
        public DbDictionaryMap(EntityTypeBuilder<DbDictionary> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbDictionary> builder)
        {
            builder.ToTable("dictionary");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(256);

            builder.Property(e => e.Value)
                .HasColumnName("value")
                .HasMaxLength(64);

            builder.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey("parent_id")
                .HasConstraintName("fk_d_parent");
        }
    }
}
