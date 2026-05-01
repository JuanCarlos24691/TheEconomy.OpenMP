using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Components.AccountInformation;

namespace TheEconomy.Server.Resources.RegisterAccount.Interfaces;

public interface IRegisterAccountView
{
    public void CreatePlayerTextDrawings(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);
}