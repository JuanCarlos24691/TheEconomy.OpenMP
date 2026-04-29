using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Components.AccountInformation;

namespace TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;

public interface IVerifyProhibitionView
{
    public void CreatePlayerTextDrawings(Player player, AccountInformation accountInformation);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);
}