using SampSharp.Entities.SAMP;
using System;
using System.Linq;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyUserName.Components;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;

namespace TheEconomy.Server.Resources.Services.VerifyUserName;

public class VerifyUserNameUI(IWorldService worldService, ICorrectTextStrings correctTextStrings) : IVerifyUserNameUI
{
    public void CreatePlayerTextDrawings(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (player.GetComponent<VerifyUserNameUIComp>() is not null)
            return;

        PlayerTextDraw[] playerTextDraw = new PlayerTextDraw[8];

        playerTextDraw[0] = worldService.CreatePlayerTextDraw(player, position: new Vector2(-5.0f, -5.0f), "_"); // Un poco fuera para asegurar
        playerTextDraw[0].Font = TextDrawFont.Normal;
        playerTextDraw[0].LetterSize = new Vector2(0.0f, 55.0f);
        playerTextDraw[0].TextSize = new Vector2(645.0f, 0.0f);
        playerTextDraw[0].Outline = 0;
        playerTextDraw[0].Shadow = 0;
        playerTextDraw[0].Alignment = TextDrawAlignment.Left;
        playerTextDraw[0].ForeColor = -1;
        playerTextDraw[0].BackColor = 255;
        playerTextDraw[0].BoxColor = 320018163;
        playerTextDraw[0].UseBox = true;
        playerTextDraw[0].Proportional = true;

        playerTextDraw[1] = worldService.CreatePlayerTextDraw(player, position: new Vector2(298.000, 75.000), "mdl-1000:icon_prohibited");
        playerTextDraw[1].Font = TextDrawFont.DrawSprite;
        playerTextDraw[1].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[1].TextSize = new Vector2(43.500, 51.500);
        playerTextDraw[1].Outline = 1;
        playerTextDraw[1].Shadow = 0;
        playerTextDraw[1].Alignment = TextDrawAlignment.Center;
        playerTextDraw[1].ForeColor = -1;
        playerTextDraw[1].BackColor = 255;
        playerTextDraw[1].BoxColor = 135;
        playerTextDraw[1].UseBox = true;
        playerTextDraw[1].Proportional = true;

        playerTextDraw[2] = worldService.CreatePlayerTextDraw(player, position: new Vector2(321.000, 124.000), correctTextStrings.ObtainCorrection("Nombre de usuario no válido"));
        playerTextDraw[2].Font = TextDrawFont.Normal;
        playerTextDraw[2].LetterSize = new Vector2(0.508333, 1.950000);
        playerTextDraw[2].TextSize = new Vector2(400.000000, 17.000000);
        playerTextDraw[2].Outline = 0;
        playerTextDraw[2].Shadow = 0;
        playerTextDraw[2].Alignment = TextDrawAlignment.Center;
        playerTextDraw[2].ForeColor = -1;
        playerTextDraw[2].BackColor = 255;
        playerTextDraw[2].BoxColor = 50;
        playerTextDraw[2].UseBox = false;
        playerTextDraw[2].Proportional = true;

        playerTextDraw[3] = worldService.CreatePlayerTextDraw(player, position: new Vector2(321.000, 150.000), correctTextStrings.ObtainCorrection("El nombre de usuario con el que entraste al servidor no es válido"));
        playerTextDraw[3].Font = TextDrawFont.Normal;
        playerTextDraw[3].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[3].TextSize = new Vector2(400.000000, 17.000000);
        playerTextDraw[3].Outline = 0;
        playerTextDraw[3].Shadow = 0;
        playerTextDraw[3].Alignment = TextDrawAlignment.Center;
        playerTextDraw[3].ForeColor = -1448498689;
        playerTextDraw[3].BackColor = 255;
        playerTextDraw[3].BoxColor = 50;
        playerTextDraw[3].UseBox = false;
        playerTextDraw[3].Proportional = true;

        playerTextDraw[4] = worldService.CreatePlayerTextDraw(player, position: new Vector2(321.000, 162.0000), correctTextStrings.ObtainCorrection("Solo se admiten formatos alfanuméricos. Sí, estás usando un formato de raya al piso, debes saber que ya no es válido"));
        playerTextDraw[4].Font = TextDrawFont.Normal;
        playerTextDraw[4].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[4].TextSize = new Vector2(400.000000, 17.000000);
        playerTextDraw[4].Outline = 0;
        playerTextDraw[4].Shadow = 0;
        playerTextDraw[4].Alignment = TextDrawAlignment.Center;
        playerTextDraw[4].ForeColor = -1448498689;
        playerTextDraw[4].BackColor = 255;
        playerTextDraw[4].BoxColor = 50;
        playerTextDraw[4].UseBox = false;
        playerTextDraw[4].Proportional = true;

        playerTextDraw[5] = worldService.CreatePlayerTextDraw(player, position: new Vector2(321.000, 174.000), correctTextStrings.ObtainCorrection("Juan#Ospino y $JuanGamer no son nombres de usuario válidos para este servidor"));
        playerTextDraw[5].Font = TextDrawFont.Normal;
        playerTextDraw[5].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[5].TextSize = new Vector2(400.000000, 17.000000);
        playerTextDraw[5].Outline = 0;
        playerTextDraw[5].Shadow = 0;
        playerTextDraw[5].Alignment = TextDrawAlignment.Center;
        playerTextDraw[5].ForeColor = -1448498689;
        playerTextDraw[5].BackColor = 255;
        playerTextDraw[5].BoxColor = 50;
        playerTextDraw[5].UseBox = false;
        playerTextDraw[5].Proportional = true;

        playerTextDraw[6] = worldService.CreatePlayerTextDraw(player, position: new Vector2(321.000, 185.000), correctTextStrings.ObtainCorrection("Juan24691 y David24691 son algunos nombre de usuarios válidos para usar en este servidor"));
        playerTextDraw[6].Font = TextDrawFont.Normal;
        playerTextDraw[6].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[6].TextSize = new Vector2(400.000000, 17.000000);
        playerTextDraw[6].Outline = 0;
        playerTextDraw[6].Shadow = 0;
        playerTextDraw[6].Alignment = TextDrawAlignment.Center;
        playerTextDraw[6].ForeColor = -1448498689;
        playerTextDraw[6].BackColor = 255;
        playerTextDraw[6].BoxColor = 50;
        playerTextDraw[6].UseBox = false;
        playerTextDraw[6].Proportional = true;

        playerTextDraw[7] = worldService.CreatePlayerTextDraw(player, position: new Vector2(321.000, 211.000), correctTextStrings.ObtainCorrection("Por favor, vuelve a ingresar al servidor con un formato de nombre usuario válido."));
        playerTextDraw[7].Font = TextDrawFont.Normal;
        playerTextDraw[7].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[7].TextSize = new Vector2(400.000000, 17.000000);
        playerTextDraw[7].Outline = 0;
        playerTextDraw[7].Shadow = 0;
        playerTextDraw[7].Alignment = TextDrawAlignment.Center;
        playerTextDraw[7].ForeColor = -1448498689;
        playerTextDraw[7].BackColor = 255;
        playerTextDraw[7].BoxColor = 50;
        playerTextDraw[7].UseBox = false;
        playerTextDraw[7].Proportional = true;

        player.AddComponent<VerifyUserNameUIComp>((object)playerTextDraw);
    }

    public void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        VerifyUserNameUIComp verifyUserNameUIComp = GetTextDrawOrThrow(player);

        foreach (PlayerTextDraw playerTextdraw in verifyUserNameUIComp.PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw.Show();
    }

    public void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        VerifyUserNameUIComp verifyUserNameUIComp = GetTextDrawOrThrow(player);

        foreach (PlayerTextDraw playerTextdraw in verifyUserNameUIComp.PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw.Hide();
    }

    public void Destroy(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        VerifyUserNameUIComp verifyUserNameUIComp = GetTextDrawOrThrow(player);

        foreach (PlayerTextDraw playerTextdraw in verifyUserNameUIComp.PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw.Destroy();
    }

    private VerifyUserNameUIComp GetTextDrawOrThrow(Player player)
    {
        return player.GetComponent<VerifyUserNameUIComp>() ?? throw new InvalidOperationException($"The '{nameof(VerifyUserNameUIComp)}' component is not attached to the player");
    }
}