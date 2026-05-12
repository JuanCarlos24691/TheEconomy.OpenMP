using System;
using System.Linq;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Authenticator.EditCharacter.Components;

public class EditCharacterLayoutComponent : Component
{
    public PlayerTextDraw[] PlayerTextDrawings { get; set; }

    public EditCharacterLayoutComponent(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }

    protected override void OnDestroyComponent()
    {
        PlayerTextDrawings?.Where(t => t != null && t.IsComponentAlive).ToList().ForEach(t => t.Destroy());
    }
}