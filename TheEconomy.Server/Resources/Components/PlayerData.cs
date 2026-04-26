using TheEconomy.Database.Entity.Account;
using TheEconomy.Database.Entity.Prohibitions;
using TheEconomy.Database.Entity.Character;
using SampSharp.Entities;

namespace TheEconomy.Server.Resources.Components
{
    public class PlayerData : Component
    {
        public Prohibition Prohibition { get; set; }
        public Account Account { get; set; }
        public Character Character { get; set; }
    }
}