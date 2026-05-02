using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.RegisterAccount.Components;

public class RegisterAccountComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public RegisterAccountComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}