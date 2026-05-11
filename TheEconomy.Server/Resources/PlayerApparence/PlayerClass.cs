using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.PlayerApparence;

public class PlayerClass() : ISystem
{
    [Event]
    public static void OnGameModeInit(IServerService serverService)
    {
        serverService.AddPlayerClass(0, 0, new Vector3(2243.8772, -1260.7579, 23.9486), (float)271.7634);
    }

    [Event]
    public static void OnPlayerConnect(Player player)
    {
        player.ToggleSpectating(true);
    }
}