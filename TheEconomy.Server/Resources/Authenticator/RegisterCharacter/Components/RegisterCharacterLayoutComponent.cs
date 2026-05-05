using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;
using System.Linq;

namespace TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Components;

public class RegisterCharacterLayoutComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings { get; set; }

    public RegisterCharacterLayoutComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }

    protected override void OnDestroyComponent()
    {
        PlayerTextDrawings?.Where(t => t != null && t.IsComponentAlive).ToList().ForEach(t => t.Destroy());
    }
}