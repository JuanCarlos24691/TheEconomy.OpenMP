using SampSharp.Entities;
using TheEconomy.Database.Entity.Character;

namespace TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Components;

public class RegisterCharacterComponent : Component
{
    public CharacterEntity Character { get; set; } = new CharacterEntity();
}