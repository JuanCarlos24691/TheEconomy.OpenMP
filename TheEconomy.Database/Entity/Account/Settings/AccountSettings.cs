using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheEconomy.Database.Entity.Account.Settings
{
    internal class AccountSettings : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            // Account
            builder.HasKey(a => a.UUID);
            builder.HasIndex(a => a.UUID).IsUnique();
            builder.Property(a => a.UUID).HasColumnType("binary(16)");
            builder.HasIndex(a => a.Name).IsUnique();
            builder.Property(a => a.Name).HasMaxLength(24).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(128).IsRequired();
            builder.Property(a => a.Mail).HasMaxLength(319).IsRequired();
            builder.Property(a => a.AdministrativeLevel).HasMaxLength(5).IsRequired(false);
            builder.Property(a => a.SelectedCharacter).HasMaxLength(2).IsRequired(false);
            builder.Property(a => a.AccountProhibitedBy).HasMaxLength(24).IsRequired(false);
            builder.Property(a => a.ReasonForProhibition).HasMaxLength(128).IsRequired(false);
            builder.Property(a => a.DateOfProhibition).HasColumnType("date").IsRequired(false);
        }
    }
}