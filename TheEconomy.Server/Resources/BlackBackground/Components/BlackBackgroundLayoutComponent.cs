using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.BlackBackground.Components;

public class BlackBackgroundLayoutComponent : Component
{
    public PlayerTextDraw PlayerTextDrawings { get; set; }

    public BlackBackgroundLayoutComponent(PlayerTextDraw playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }

    protected override void OnDestroyComponent()
    {
        PlayerTextDrawings?.Destroy();
    }
}