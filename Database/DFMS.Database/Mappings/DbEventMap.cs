using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbEventMap : DbTableCommonModelMap<DbEvent>
    {
        public DbEventMap(EntityTypeBuilder<DbEvent> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbEvent> builder)
        {
            builder.ToTable("event");

            builder.Property(e => e.IsProcessed)
                .HasColumnName("is_processed")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.Data)
                .HasColumnName("data")
                .HasMaxLength(4000);

            builder.Property(e => e.WorkerName)
                .HasColumnName("worker_name")
                .HasMaxLength(64);

            builder.Property(e => e.ProcessingStart)
                .HasColumnName("processing_start")
                .HasMaxLength(32);

            builder.Property(e => e.ProcessingEnd)
                .HasColumnName("processing_end")
                .HasMaxLength(32);

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey("type_id")
                .HasConstraintName("fk_e_type");
        }
    }
}
