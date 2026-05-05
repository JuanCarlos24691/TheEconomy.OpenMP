using SampSharp.Entities;
using TheEconomy.Database.Entity.Account;

namespace TheEconomy.Server.Resources.Authenticator.RegisterAccount.Components;

public class RegisterAccountComponent : Component
{
    public AccountEntity Account { get; set; } = new AccountEntity();

    public bool ShowResgiterCharacterLayout { get; set; }
}