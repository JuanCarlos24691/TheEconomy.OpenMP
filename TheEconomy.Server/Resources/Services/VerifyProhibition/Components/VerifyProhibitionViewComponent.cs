using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.Services.VerifyProhibition.Components;

public class VerifyProhibitionViewComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public VerifyProhibitionViewComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}