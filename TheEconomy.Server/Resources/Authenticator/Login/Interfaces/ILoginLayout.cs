using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Authenticator.Login.Components;

namespace TheEconomy.Server.Resources.Authenticator.Login.Interfaces;

public interface ILoginLayout
{
    public void Create(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);

    public LoginLayoutComponent GetLoginLayoutComponent(Player player);
}