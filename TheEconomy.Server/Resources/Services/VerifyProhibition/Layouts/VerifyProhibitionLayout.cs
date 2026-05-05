using SampSharp.Entities.SAMP;
using System;
using System.Linq;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Components;
using TheEconomy.Server.Resources.Components.AccountInformation;
using System.Threading.Tasks;

namespace TheEconomy.Server.Resources.Services.VerifyProhibition.Layouts;

public class VerifyProhibitionLayout(IWorldService worldService, IServerInformation serverInformation, ICorrectTextStrings correctTextStrings) : IVerifyProhibitionLayout
{
    public void Create(Player player, AccountInformation accountInformation)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(accountInformation);

        string[] paragraphs = new string[5];

        if (accountInformation.Prohibition is not null)
        {
            paragraphs[0] = "Prohibido";
            paragraphs[0] = correctTextStrings.Correct($"Hola {player.Name}, fuiste prohibido del servidor por el administrador {accountInformation.Prohibition.ProhibitedBy}.");
            paragraphs[1] = correctTextStrings.Correct($"Razón: {accountInformation.Prohibition.Reason} - Fecha: {accountInformation.Prohibition.DateOfProhibition}");
            paragraphs[2] = correctTextStrings.Correct($"Sí, quieres apelar a esta decision, puedes contactarnos en el Foro({serverInformation.Forum})");
            paragraphs[3] = correctTextStrings.Correct($"o en nuestro discord({serverInformation.Discord})");
            paragraphs[4] = correctTextStrings.Correct("Tenga en cuenta que no todos pueden apelar a un desbaneo por diversos motivos");
        }
        else if (accountInformation.Account is not null)
        {
            paragraphs[0] = correctTextStrings.Correct("Cuenta restringida");
            paragraphs[0] = correctTextStrings.Correct($"Hola {player.Name}, esta cuenta fue prohibida por el administrador {accountInformation.Account.AccountProhibitedBy}.");
            paragraphs[1] = correctTextStrings.Correct($"Razón: {accountInformation.Account.ReasonForProhibition} - Fecha: {accountInformation.Account.DateOfProhibition}{(accountInformation.Account.ProhibitedAccount > 0 ? $" - Días ({accountInformation.Account.ProhibitedAccount})" : "")}");
            paragraphs[2] = correctTextStrings.Correct($"Sí, quieres apelar a esta decision, puedes contactarnos en el Foro({serverInformation.Forum})");
            paragraphs[3] = correctTextStrings.Correct($"o en nuestro discord({serverInformation.Discord})");
            paragraphs[4] = correctTextStrings.Correct("Tenga en cuenta que no todos pueden apelar a un desbaneo por diversos motivos");
        }

        PlayerTextDraw[] playerTextDraw = new PlayerTextDraw[6];

        playerTextDraw[0] = worldService.CreatePlayerTextDraw(player, new Vector2(298.000, 75.000), "mdl-1000:icon_prohibited");
        playerTextDraw[0].Font = TextDrawFont.DrawSprite;
        playerTextDraw[0].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[0].TextSize = new Vector2(43.500, 51.500);
        playerTextDraw[0].Outline = 1;
        playerTextDraw[0].Shadow = 0;
        playerTextDraw[0].Alignment = TextDrawAlignment.Center;
        playerTextDraw[0].ForeColor = -1;
        playerTextDraw[0].BackColor = 255;
        playerTextDraw[0].BoxColor = 135;
        playerTextDraw[0].UseBox = true;
        playerTextDraw[0].Proportional = true;

        playerTextDraw[1] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 124.000), paragraphs[0]);
        playerTextDraw[1].Font = TextDrawFont.Normal;
        playerTextDraw[1].LetterSize = new Vector2(0.508333, 1.950000);
        playerTextDraw[1].TextSize = new Vector2(400.000000, 17.000000);
        playerTextDraw[1].Outline = 0;
        playerTextDraw[1].Shadow = 0;
        playerTextDraw[1].Alignment = TextDrawAlignment.Center;
        playerTextDraw[1].ForeColor = -1;
        playerTextDraw[1].BackColor = 255;
        playerTextDraw[1].BoxColor = 50;
        playerTextDraw[1].UseBox = false;
        playerTextDraw[1].Proportional = true;

        playerTextDraw[2] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 150.000), paragraphs[0]);
        playerTextDraw[2].Font = TextDrawFont.Normal;
        playerTextDraw[2].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[2].TextSize = new Vector2(400.000000, 17.000000);
        playerTextDraw[2].Outline = 0;
        playerTextDraw[2].Shadow = 0;
        playerTextDraw[2].Alignment = TextDrawAlignment.Center;
        playerTextDraw[2].ForeColor = -1448498689;
        playerTextDraw[2].BackColor = 255;
        playerTextDraw[2].BoxColor = 50;
        playerTextDraw[2].UseBox = false;
        playerTextDraw[2].Proportional = true;

        playerTextDraw[3] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 162.0000), paragraphs[1]);
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

        playerTextDraw[4] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 174.000), paragraphs[2]);
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

        playerTextDraw[5] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 185.000), paragraphs[3]);
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

        playerTextDraw[6] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 211.000), paragraphs[4]);
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

        player.AddComponent<VerifyProhibitionLayoutComponent>((object)playerTextDraw);
    }

    public void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        foreach (PlayerTextDraw playerTextdraw in GetVerifyProhibitionLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Show();
    }

    public void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        foreach (PlayerTextDraw playerTextdraw in GetVerifyProhibitionLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Hide();
    }

    public void Destroy(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        foreach (PlayerTextDraw playerTextdraw in GetVerifyProhibitionLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Destroy();
    }

    public VerifyProhibitionLayoutComponent GetVerifyProhibitionLayoutComponent(Player player)
    {
        return player.GetComponent<VerifyProhibitionLayoutComponent>() ?? throw new InvalidOperationException($"The '{nameof(VerifyProhibitionLayoutComponent)}' component is not attached to the player");
    }
}