using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;
using System.Linq;

namespace TheEconomy.Server.Resources.Authenticator.RegisterAccount.Components;

public class RegisterAccountLayoutComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings { get; set; }

    public RegisterAccountLayoutComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }

    protected override void OnDestroyComponent()
    {
        PlayerTextDrawings?.ToList().ForEach(t => t?.Destroy());
    }
}