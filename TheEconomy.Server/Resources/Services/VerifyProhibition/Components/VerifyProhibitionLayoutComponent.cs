using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;
using System.Linq;

namespace TheEconomy.Server.Resources.Services.VerifyProhibition.Components;

public class VerifyProhibitionLayoutComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings { get; set; }

    public VerifyProhibitionLayoutComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }

    protected override void OnDestroyComponent()
    {
        PlayerTextDrawings?.ToList().ForEach(t => t?.Destroy());
    }
}