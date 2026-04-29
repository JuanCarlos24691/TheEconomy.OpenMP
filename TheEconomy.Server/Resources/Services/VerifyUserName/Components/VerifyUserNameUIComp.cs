using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;

namespace TheEconomy.Server.Resources.Services.VerifyUserName.Components;

public class VerifyUserNameUIComp : Component
{
    public PlayerTextDraw[] PlayerTextDrawings {  get; set; }

    public VerifyUserNameUIComp(PlayerTextDraw[] playerTextDraw)
    {
        ArgumentNullException.ThrowIfNull(playerTextDraw);
        PlayerTextDrawings = playerTextDraw;
    }
}