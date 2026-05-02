using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;

public interface IVerifyUserNameLayout
{
    public void Create(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);
}