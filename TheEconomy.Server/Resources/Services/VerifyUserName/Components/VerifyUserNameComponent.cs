using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.Services.VerifyUserName.Components;

public class VerifyUserNameComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public VerifyUserNameComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}