using TheEconomy.Database.Entity.Account;
using SampSharp.Entities;

namespace TheEconomy.Server.Resources.Components.AccountInformation;

public class AccountInformation : Component
{
    public AccountEntity Account { get; set; }
    
    public bool IsLoggedIn { get; set; }
}