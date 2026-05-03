using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.Services.VerifyUserName.Components;

public class VerifyUserNameLayoutComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public VerifyUserNameLayoutComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}