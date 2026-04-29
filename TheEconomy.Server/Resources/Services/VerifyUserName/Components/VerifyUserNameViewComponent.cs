using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.Services.VerifyUserName.Components;

public class VerifyUserNameViewComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public VerifyUserNameViewComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}