using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbWorkspaceMap : DbTableCommonModelMap<DbWorkspace>
    {
        public DbWorkspaceMap(EntityTypeBuilder<DbWorkspace> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbWorkspace> builder)
        {
            builder.ToTable("workspace");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Public)
                .HasColumnName("public")
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
