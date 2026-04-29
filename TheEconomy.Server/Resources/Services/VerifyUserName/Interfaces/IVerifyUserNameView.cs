using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;

public interface IVerifyUserNameView
{
    public void CreatePlayerTextDrawings(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);
}