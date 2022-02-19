using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldDefinitionMap : DbTableCommonModelMap<DbFormFieldDefinition>
    {
        public DbFormFieldDefinitionMap(EntityTypeBuilder<DbFormFieldDefinition> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldDefinition> builder)
        {
            builder.ToTable("form_field_definition");

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired();

            builder.Property(e => e.Type)
                .HasColumnName("type")
                .IsRequired();

            builder.Property(e => e.Visible)
                .HasColumnName("visible")
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
