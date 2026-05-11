using TheEconomy.Database.Entity.Account;
using SampSharp.Entities;

namespace TheEconomy.Server.Resources.DatabaseEntities.Account.Components;

public class AccountComponent : Component
{
    public AccountEntity Account { get; set; }
    
    public bool IsLoggedIn { get; set; }
}