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
            builder.Property(c => c.Online).HasColumnType("tinyint").HasMaxLength(1).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(12).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(12).IsRequired();
            builder.Property(c => c.Gender).HasColumnType("tinyint").HasMaxLength(1).IsRequired();
            builder.Property(c => c.BirthDate).HasColumnType("date").IsRequired();
            builder.Property(c => c.Appearance).IsRequired();
            builder.Property(c => c.Height).IsRequired();
            builder.Property(c => c.EyeColor).HasMaxLength(24).IsRequired();
            builder.Property(c => c.HairColor).HasMaxLength(24).IsRequired();
            builder.Property(c => c.SkinColor).HasMaxLength(24).IsRequired();
            builder.Property(c => c.SpawnX).IsRequired();
            builder.Property(c => c.SpawnY).IsRequired();
            builder.Property(c => c.SpawnZ).IsRequired();
            builder.Property(c => c.Angle).IsRequired();
            builder.Property(c => c.FirstConnection).HasColumnType("date").IsRequired();
            builder.Property(c => c.LastConnection).HasColumnType("date").IsRequired();
            builder.Property(c => c.StabbingGun).IsRequired(false);
            builder.Property(c => c.NumberOfBladedGun).IsRequired(false);
            builder.Property(c => c.ShortGun).IsRequired(false);
            builder.Property(c => c.ShortGunAmmunition).IsRequired(false);
            builder.Property(c => c.LongGun).IsRequired(false);
            builder.Property(c => c.LongGunAmmunition).IsRequired(false);
        }
    }
}