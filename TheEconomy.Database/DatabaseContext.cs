using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TheEconomy.Database.Entity.ServerInformation;
using TheEconomy.Database.Entity.Prohibitions;
using TheEconomy.Database.Entity.Account;
using TheEconomy.Database.Entity.Character;

namespace TheEconomy.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(new ConectionString().ConnectionStrings, new MySqlServerVersion(new Version(9, 0, 0)));
        }

        public DbSet<ServerInformation> ServerInformation => Set<ServerInformation>();
        public DbSet<Prohibition> Prohibitions => Set<Prohibition>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Character> Characters => Set<Character>();
    }
}