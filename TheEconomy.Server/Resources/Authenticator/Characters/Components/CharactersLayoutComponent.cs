using System;
using System.Linq;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Authenticator.Characters.Components;

public class CharactersLayoutComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings { get; set; }

    public CharactersLayoutComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }

    protected override void OnDestroyComponent()
    {
        PlayerTextDrawings?.Where(t => t != null && t.IsComponentAlive).ToList().ForEach(t => t.Destroy());
    }
}