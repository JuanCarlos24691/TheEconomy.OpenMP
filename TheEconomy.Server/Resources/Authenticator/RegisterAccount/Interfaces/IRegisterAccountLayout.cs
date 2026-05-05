using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Components;

namespace TheEconomy.Server.Resources.Authenticator.RegisterAccount.Interfaces;

public interface IRegisterAccountLayout
{
    public void Create(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);

    public RegisterAccountLayoutComponent GetRegisterAccountLayoutComponent(Player player);
}