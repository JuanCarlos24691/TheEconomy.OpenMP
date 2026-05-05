using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Components;

public class RegisterCharacterLayoutComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public RegisterCharacterLayoutComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}