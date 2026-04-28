using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheEconomy.Database.Entity.Prohibitions.Settings
{
    internal class ProhibitionSettings : IEntityTypeConfiguration<ProhibitionEntity>
    {
        public void Configure(EntityTypeBuilder<ProhibitionEntity> builder)
        {
            // Prohibition
            builder.HasKey(p => p.UUID);
            builder.HasIndex(p => p.UUID).IsUnique();
            builder.Property(a => a.UUID).HasColumnType("binary(16)");
            builder.Property(p => p.IP).HasMaxLength(15).IsRequired();
            builder.Property(p => p.ProhibitedBy).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Reason).HasMaxLength(128).IsRequired();
            builder.Property(p => p.DateOfProhibition).HasColumnType("date").IsRequired();
        }
    }
}