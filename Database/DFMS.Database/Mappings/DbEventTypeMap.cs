using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbEventTypeMap : DbTableCommonModelMap<DbEventType>
    {
        public DbEventTypeMap(EntityTypeBuilder<DbEventType> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbEventType> builder)
        {
            builder.ToTable("event_type");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}
