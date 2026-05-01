using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.RegisterAccount.Components;

public class RegisterAccountViewComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public RegisterAccountViewComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}