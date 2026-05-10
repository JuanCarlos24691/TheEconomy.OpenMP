using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Authenticator.Characters.Components;

namespace TheEconomy.Server.Resources.Authenticator.Characters.Interfaces;

public interface ICharactersLayout
{
    public void Create(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);

    public CharactersLayoutComponent GetCharactersLayoutComponent(Player player);
}