using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.RegisterAccount.Components;

namespace TheEconomy.Server.Resources.RegisterAccount.Interfaces;

public interface IRegisterAccountLayout
{
    public void Create(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);

    public RegisterAccountLayoutComponent GetRegisterAccountLayoutComponent(Player player);
}