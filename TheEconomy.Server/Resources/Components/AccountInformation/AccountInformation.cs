using TheEconomy.Database.Entity.Account;
using TheEconomy.Database.Entity.Prohibitions;
using TheEconomy.Database.Entity.Character;
using SampSharp.Entities;

namespace TheEconomy.Server.Resources.Components.AccountInformation;

public class AccountInformation : Component
{
    public ProhibitionEntity Prohibition { get; set; }
    public AccountEntity Account { get; set; }
    public CharacterEntity Character { get; set; }

    public bool IsLoadedProhibition => Prohibition != null;
    public bool IsLoadedAccount => Account != null;
    public bool IsLoadedCharacter => Character != null;
}