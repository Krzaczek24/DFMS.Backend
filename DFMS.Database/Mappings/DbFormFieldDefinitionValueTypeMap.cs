using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldDefinitionValueTypeMap : DbTableCommonModelMap<DbFormFieldDefinitionValueType>
    {
        public DbFormFieldDefinitionValueTypeMap(EntityTypeBuilder<DbFormFieldDefinitionValueType> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldDefinitionValueType> builder)
        {
            builder.ToTable("form_field_definition_value_type");

            builder.HasOne(e => e.FieldDefinition)
                .WithMany()
                .HasForeignKey("field_definition_id")
                .HasConstraintName("fk_ffdvt_field_definition")
                .IsRequired();

            builder.HasOne(e => e.ValueType)
                .WithMany()
                .HasForeignKey("value_type_id")
                .HasConstraintName("fk_ffdvt_value_type")
                .IsRequired();
        }
    }
}
