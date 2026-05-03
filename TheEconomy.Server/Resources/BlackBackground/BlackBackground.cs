using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;

namespace TheEconomy.Server.Resources.BlackBackground;

public class BlackBackground(IBlackBackgroundLayout blackBackgroundLayout) : ISystem
{
    [Event]
    public void OnPlayerConnect(Player player)
    {
        blackBackgroundLayout.Create(player);
    }
}