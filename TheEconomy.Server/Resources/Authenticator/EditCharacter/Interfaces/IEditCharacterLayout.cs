using SampSharp.Entities.SAMP;
using TheEconomy.Database.Entity.Character;
using TheEconomy.Server.Resources.Authenticator.EditCharacter.Components;

namespace TheEconomy.Server.Resources.Authenticator.EditCharacter.Interfaces;

public interface IEditCharacterLayout
{
    public void Create(Player player, CharacterEntity characterEntity);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);

    public EditCharacterLayoutComponent GetEditCharacterLayoutComponent(Player player);
}