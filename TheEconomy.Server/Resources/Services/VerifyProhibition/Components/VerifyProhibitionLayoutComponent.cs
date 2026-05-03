using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.Services.VerifyProhibition.Components;

public class VerifyProhibitionLayoutComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public VerifyProhibitionLayoutComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}