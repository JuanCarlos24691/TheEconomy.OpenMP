using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Components;

namespace TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Interfaces;

public interface IRegisterCharacterLayout
{
    public void Create(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);

    public RegisterCharacterLayoutComponent GetRegisterCharacterLayoutComponent(Player player);
}