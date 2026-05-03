using SampSharp.Entities;
using TheEconomy.Database.Entity.Account;

namespace TheEconomy.Server.Resources.RegisterAccount.Components;

public class RegisterAccountComponent : Component
{
    public AccountEntity Account { get; set; } = new AccountEntity();

    public bool ShowPassword { get; set; }
}