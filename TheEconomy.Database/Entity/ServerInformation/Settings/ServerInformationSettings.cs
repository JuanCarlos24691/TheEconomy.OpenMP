using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheEconomy.Database.Entity.ServerInformation.Settings
{
    public class ServerInformationSettings : IEntityTypeConfiguration<ServerInformationEntity>
    {
        public void Configure(EntityTypeBuilder<ServerInformationEntity> builder)
        {
            builder.HasKey(a => a.UUID);
            builder.HasIndex(a => a.UUID).IsUnique();
            builder.Property(a => a.UUID).HasColumnType("binary(16)");
            builder.HasIndex(a => a.Name).IsUnique();
            builder.Property(a => a.Name).IsRequired();
            builder.HasIndex(a => a.Mode).IsUnique();
            builder.Property(a => a.Mode).IsRequired();
            builder.HasIndex(a => a.Language).IsUnique();
            builder.Property(a => a.Language).IsRequired();
            builder.HasIndex(a => a.Version).IsUnique();
            builder.Property(a => a.Version).IsRequired();
            builder.HasIndex(a => a.WebSite).IsUnique();
            builder.Property(a => a.WebSite).IsRequired();
            builder.HasIndex(a => a.Forum).IsUnique();
            builder.Property(a => a.Forum).IsRequired();
            builder.HasIndex(a => a.Discord).IsUnique();
            builder.Property(a => a.Discord).IsRequired();
        }
    }
}
