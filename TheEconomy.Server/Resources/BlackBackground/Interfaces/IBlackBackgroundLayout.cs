using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.BlackBackground.Components;

namespace TheEconomy.Server.Resources.BlackBackground.Interfaces;

public interface IBlackBackgroundLayout
{
    public void Create(Player player);

    public void Show(Player player);

    public void Hide(Player player);

    public void Destroy(Player player);

    public BlackBackgroundLayoutComponent GetBlackBackgroundLayoutComponent(Player player);
}