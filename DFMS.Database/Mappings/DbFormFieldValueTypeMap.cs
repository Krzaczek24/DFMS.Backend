using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldValueTypeMap : DbTableCommonModelMap<DbFormFieldValueType>
    {
        public DbFormFieldValueTypeMap(EntityTypeBuilder<DbFormFieldValueType> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldValueType> builder)
        {
            builder.ToTable("form_field_value_type");

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.Code)
                .HasColumnName("code")
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}
