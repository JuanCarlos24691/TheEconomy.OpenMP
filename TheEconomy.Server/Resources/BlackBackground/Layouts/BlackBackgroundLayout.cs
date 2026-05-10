using System;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;
using TheEconomy.Server.Resources.BlackBackground.Components;

namespace TheEconomy.Server.Resources.BlackBackground.Layouts;

public class BlackBackgroundLayout(IWorldService worldService) : IBlackBackgroundLayout
{
    public void Create(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (player.GetComponent<BlackBackgroundLayoutComponent>()?.PlayerTextDrawings is not null)
            return;

        PlayerTextDraw playerTextDraw = worldService.CreatePlayerTextDraw(player, position: new Vector2(-5.0f, -5.0f), "_"); 
        playerTextDraw.Font = TextDrawFont.Normal;
        playerTextDraw.LetterSize = new Vector2(0.0f, 55.0f);
        playerTextDraw.TextSize = new Vector2(645.0f, 0.0f);
        playerTextDraw.Outline = 0;
        playerTextDraw.Shadow = 0;
        playerTextDraw.Alignment = TextDrawAlignment.Left;
        playerTextDraw.ForeColor = -1;
        playerTextDraw.BackColor = 255;
        playerTextDraw.BoxColor = 320018163;
        playerTextDraw.UseBox = true;
        playerTextDraw.Proportional = true;

        player.AddComponent<BlackBackgroundLayoutComponent>(playerTextDraw);
        Show(player);
    }

    public void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        GetBlackBackgroundLayoutComponent(player)?.PlayerTextDrawings.Show();
    }

    public void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        GetBlackBackgroundLayoutComponent(player)?.PlayerTextDrawings.Hide();
    }

    public void Destroy(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        GetBlackBackgroundLayoutComponent(player)?.PlayerTextDrawings.Destroy();
    }

    public BlackBackgroundLayoutComponent GetBlackBackgroundLayoutComponent(Player player)
    {
        return player.GetComponent<BlackBackgroundLayoutComponent>() ?? throw new InvalidOperationException($"The '{nameof(BlackBackgroundLayoutComponent)}' component is not attached to the player");
    }
}