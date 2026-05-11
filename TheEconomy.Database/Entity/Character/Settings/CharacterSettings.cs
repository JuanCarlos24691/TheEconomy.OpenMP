using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheEconomy.Database.Entity.Character.Settings
{
    internal class CharacterSettings : IEntityTypeConfiguration<CharacterEntity>
    {
        public void Configure(EntityTypeBuilder<CharacterEntity> builder)
        {
            // Character
            builder.HasKey(c => c.UUID);
            builder.HasIndex(c => c.UUID).IsUnique();
            builder.Property(a => a.UUID).HasColumnType("binary(16)");
            builder.Property(a => a.AUUID).HasColumnType("binary(16)");
            builder.HasOne(c => c.Account).WithMany(a => a.Characters).HasForeignKey(c => c.AUUID).OnDelete(DeleteBehavior.Cascade);
            builder.Property(c => c.Online).HasColumnType("tinyint").HasDefaultValue(1).HasMaxLength(1).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(12).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(12).IsRequired();
            builder.Property(c => c.Gender).HasColumnType("tinyint").HasMaxLength(1).IsRequired();
            builder.Property(c => c.BirthDate).HasColumnType("date").IsRequired();
            builder.Property(c => c.Appearance).HasDefaultValue(137).IsRequired();
            builder.Property(c => c.Height).HasMaxLength(24).IsRequired();
            builder.Property(c => c.EyeColor).HasMaxLength(24).IsRequired();
            builder.Property(c => c.HairColor).HasMaxLength(24).IsRequired();
            builder.Property(c => c.SkinColor).HasMaxLength(24).IsRequired();
            builder.Property(c => c.SpawnX).HasDefaultValue(2243.8772).IsRequired();
            builder.Property(c => c.SpawnY).HasDefaultValue(-1260.7579).IsRequired();
            builder.Property(c => c.SpawnZ).HasDefaultValue(23.9486).IsRequired();
            builder.Property(c => c.Angle).HasDefaultValue(271.7634).IsRequired();
            builder.Property(c => c.FirstConnection).HasColumnType("date").IsRequired();
            builder.Property(c => c.LastConnection).HasColumnType("date").IsRequired();
        }
    }
}