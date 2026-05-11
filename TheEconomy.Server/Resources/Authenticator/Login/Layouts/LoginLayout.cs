using System;
using System.Linq;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Authenticator.Login.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Components;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.Login.Layouts;

public class LoginLayout(IWorldService worldService, IServerInformation serverInformation, ICorrectTextStrings correctTextStrings, IColors colors, IBlackBackgroundLayout blackBackgroundLayout) : ILoginLayout
{
    public void Create(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (player.GetComponent<LoginLayoutComponent>()?.PlayerTextDrawings is not null)
            return;

        PlayerTextDraw[] playerTextDraw = new PlayerTextDraw[19];

        playerTextDraw[0] = worldService.CreatePlayerTextDraw(player, new Vector2(320.000000, 110.000000), "_");
        playerTextDraw[0].Font = (TextDrawFont)1;
        playerTextDraw[0].LetterSize = new Vector2(0.933332, 25.300003);
        playerTextDraw[0].TextSize = new Vector2(298.500000f, 310.000000f);
        playerTextDraw[0].Outline = 1;
        playerTextDraw[0].Shadow = 0;
        playerTextDraw[0].Alignment = (TextDrawAlignment)2;
        playerTextDraw[0].ForeColor = -1;
        playerTextDraw[0].BackColor = 255;
        playerTextDraw[0].BoxColor = 255;
        playerTextDraw[0].UseBox = true;
        playerTextDraw[0].Proportional = true;
        playerTextDraw[0].Selectable = false;

        playerTextDraw[1] = worldService.CreatePlayerTextDraw(player, new Vector2(456.000000, 115.000000), "mdl-1000:icon_cancel");
        playerTextDraw[1].Font = (TextDrawFont)4;
        playerTextDraw[1].LetterSize = new Vector2(0.600000, 10.300003);
        playerTextDraw[1].TextSize = new Vector2(13.000000f, 16.000000f);
        playerTextDraw[1].Outline = 1;
        playerTextDraw[1].Shadow = 0;
        playerTextDraw[1].Alignment = (TextDrawAlignment)2;
        playerTextDraw[1].ForeColor = -1;
        playerTextDraw[1].BackColor = 255;
        playerTextDraw[1].BoxColor = 135;
        playerTextDraw[1].UseBox = true;
        playerTextDraw[1].Proportional = true;
        playerTextDraw[1].Selectable = true;

        dynamic character = GetRandomCharacter();

        playerTextDraw[2] = worldService.CreatePlayerTextDraw(player, new Vector2(character.positionX, character.positionY), character.modelId);
        playerTextDraw[2].Font = (TextDrawFont)4;
        playerTextDraw[2].LetterSize = new Vector2(character.letterSizeY, character.letterSizeX);
        playerTextDraw[2].TextSize = new Vector2(character.width, character.height);
        playerTextDraw[2].Outline = 1;
        playerTextDraw[2].Shadow = 0;
        playerTextDraw[2].Alignment = (TextDrawAlignment)2;
        playerTextDraw[2].ForeColor = -1;
        playerTextDraw[2].BackColor = 255;
        playerTextDraw[2].BoxColor = 255;
        playerTextDraw[2].UseBox = true;
        playerTextDraw[2].Proportional = true;
        playerTextDraw[2].Selectable = false;

        playerTextDraw[3] = worldService.CreatePlayerTextDraw(player, new Vector2(367.000000, 131.000000), correctTextStrings.Correct("Iniciar sesión"));
        playerTextDraw[3].Font = (TextDrawFont)1;
        playerTextDraw[3].LetterSize = new Vector2(0.508333, 1.950000);
        playerTextDraw[3].TextSize = new Vector2(400.000000f, 17.000000f);
        playerTextDraw[3].Outline = 0;
        playerTextDraw[3].Shadow = 0;
        playerTextDraw[3].Alignment = (TextDrawAlignment)2;
        playerTextDraw[3].ForeColor = -1;
        playerTextDraw[3].BackColor = 255;
        playerTextDraw[3].BoxColor = 50;
        playerTextDraw[3].UseBox = false;
        playerTextDraw[3].Proportional = true;
        playerTextDraw[3].Selectable = false;

        playerTextDraw[4] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000000, 167.000000), "LD_SPAC:white");
        playerTextDraw[4].Font = (TextDrawFont)4;
        playerTextDraw[4].LetterSize = new Vector2(0.600000, 1.399996);
        playerTextDraw[4].TextSize = new Vector2(143.500000f, 18.000000f);
        playerTextDraw[4].Outline = 2;
        playerTextDraw[4].Shadow = 0;
        playerTextDraw[4].Alignment = (TextDrawAlignment)2;
        playerTextDraw[4].ForeColor = 320018175;
        playerTextDraw[4].BackColor = 16843263;
        playerTextDraw[4].BoxColor = 320017407;
        playerTextDraw[4].UseBox = true;
        playerTextDraw[4].Proportional = true;
        playerTextDraw[4].Selectable = true;

        playerTextDraw[5] = worldService.CreatePlayerTextDraw(player, new Vector2(299.000000, 168.400000), "mdl-1000:icon_profile");
        playerTextDraw[5].Font = (TextDrawFont)4;
        playerTextDraw[5].LetterSize = new Vector2(0.600000, 10.300003);
        playerTextDraw[5].TextSize = new Vector2(12.500f, 15.500f);
        playerTextDraw[5].Outline = 1;
        playerTextDraw[5].Shadow = 0;
        playerTextDraw[5].Alignment = (TextDrawAlignment)2;
        playerTextDraw[5].ForeColor = -1;
        playerTextDraw[5].BackColor = 255;
        playerTextDraw[5].BoxColor = 135;
        playerTextDraw[5].UseBox = true;
        playerTextDraw[5].Proportional = true;
        playerTextDraw[5].Selectable = false;

        playerTextDraw[6] = worldService.CreatePlayerTextDraw(player, new Vector2(367.000000, 170.000000), player.Name);
        playerTextDraw[6].Font = (TextDrawFont)1;
        playerTextDraw[6].LetterSize = new Vector2(0.283333, 1.299998);
        playerTextDraw[6].TextSize = new Vector2(400.000000f, 17.000000f);
        playerTextDraw[6].Outline = 0;
        playerTextDraw[6].Shadow = 0;
        playerTextDraw[6].Alignment = (TextDrawAlignment)2;
        playerTextDraw[6].ForeColor = -1448498689;
        playerTextDraw[6].BackColor = 255;
        playerTextDraw[6].BoxColor = 50;
        playerTextDraw[6].UseBox = false;
        playerTextDraw[6].Proportional = true;
        playerTextDraw[6].Selectable = false;

        playerTextDraw[7] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 187.000), "LD_SPAC:white");
        playerTextDraw[7].Font = (TextDrawFont)4;
        playerTextDraw[7].LetterSize = new Vector2(0.600000, 1.399996);
        playerTextDraw[7].TextSize = new Vector2(143.500000f, 18.000000f);
        playerTextDraw[7].Outline = 2;
        playerTextDraw[7].Shadow = 0;
        playerTextDraw[7].Alignment = (TextDrawAlignment)2;
        playerTextDraw[7].ForeColor = 320018175;
        playerTextDraw[7].BackColor = 16843263;
        playerTextDraw[7].BoxColor = 320017407;
        playerTextDraw[7].UseBox = true;
        playerTextDraw[7].Proportional = true;
        playerTextDraw[7].Selectable = true;

        playerTextDraw[8] = worldService.CreatePlayerTextDraw(player, new Vector2(299.000, 190.000), "mdl-1000:icon_password");
        playerTextDraw[8].Font = (TextDrawFont)4;
        playerTextDraw[8].LetterSize = new Vector2(0.600000, 10.300003);
        playerTextDraw[8].TextSize = new Vector2(11.000000f, 13.000000f);
        playerTextDraw[8].Outline = 1;
        playerTextDraw[8].Shadow = 0;
        playerTextDraw[8].Alignment = (TextDrawAlignment)2;
        playerTextDraw[8].ForeColor = -1;
        playerTextDraw[8].BackColor = 255;
        playerTextDraw[8].BoxColor = 135;
        playerTextDraw[8].UseBox = true;
        playerTextDraw[8].Proportional = true;
        playerTextDraw[8].Selectable = false;

        playerTextDraw[9] = worldService.CreatePlayerTextDraw(player, new Vector2(367.000, 188.900), "........");
        playerTextDraw[9].Font = (TextDrawFont)1;
        playerTextDraw[9].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[9].TextSize = new Vector2(400.000000f, 17.000000f);
        playerTextDraw[9].Outline = 0;
        playerTextDraw[9].Shadow = 0;
        playerTextDraw[9].Alignment = (TextDrawAlignment)2;
        playerTextDraw[9].ForeColor = -1448498689;
        playerTextDraw[9].BackColor = 255;
        playerTextDraw[9].BoxColor = 50;
        playerTextDraw[9].UseBox = false;
        playerTextDraw[9].Proportional = true;
        playerTextDraw[9].Selectable = false;

        playerTextDraw[10] = worldService.CreatePlayerTextDraw(player, new Vector2(440.000, 187.000), "LD_SPAC:white");
        playerTextDraw[10].Font = (TextDrawFont)4;
        playerTextDraw[10].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[10].TextSize = new Vector2(14.500f, 18.000f);
        playerTextDraw[10].Outline = 1;
        playerTextDraw[10].Shadow = 0;
        playerTextDraw[10].Alignment = (TextDrawAlignment)2;
        playerTextDraw[10].ForeColor = 320018175;
        playerTextDraw[10].BackColor = 16843263;
        playerTextDraw[10].BoxColor = 320017407;
        playerTextDraw[10].UseBox = true;
        playerTextDraw[10].Proportional = true;
        playerTextDraw[10].Selectable = true;

        playerTextDraw[11] = worldService.CreatePlayerTextDraw(player, new Vector2(441.000, 189.000), "mdl-1000:icon_show_password");
        playerTextDraw[11].Font = (TextDrawFont)4;
        playerTextDraw[11].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[11].TextSize = new Vector2(12.500f, 14.500f);
        playerTextDraw[11].Outline = 1;
        playerTextDraw[11].Shadow = 0;
        playerTextDraw[11].Alignment = (TextDrawAlignment)2;
        playerTextDraw[11].ForeColor = -1;
        playerTextDraw[11].BackColor = 255;
        playerTextDraw[11].BoxColor = 135;
        playerTextDraw[11].UseBox = true;
        playerTextDraw[11].Proportional = true;
        playerTextDraw[11].Selectable = false;

        playerTextDraw[12] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000000, 225.000000), "LD_SPAC:white");
        playerTextDraw[12].Font = (TextDrawFont)4;
        playerTextDraw[12].LetterSize = new Vector2(0.566666, 1.599995);
        playerTextDraw[12].TextSize = new Vector2(47.000000f, 18.000000f);
        playerTextDraw[12].Outline = 1;
        playerTextDraw[12].Shadow = 0;
        playerTextDraw[12].Alignment = (TextDrawAlignment)2;
        playerTextDraw[12].ForeColor = colors.ObtainRGB("secondaryColor");
        playerTextDraw[12].BackColor = 255;
        playerTextDraw[12].BoxColor = 9109759;
        playerTextDraw[12].UseBox = true;
        playerTextDraw[12].Proportional = true;
        playerTextDraw[12].Selectable = true;

        playerTextDraw[13] = worldService.CreatePlayerTextDraw(player, new Vector2(319.000000, 227.000000), "Ingresar");
        playerTextDraw[13].Font = (TextDrawFont)1;
        playerTextDraw[13].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[13].TextSize = new Vector2(400.000000f, 17.000000f);
        playerTextDraw[13].Outline = 0;
        playerTextDraw[13].Shadow = 0;
        playerTextDraw[13].Alignment = (TextDrawAlignment)2;
        playerTextDraw[13].ForeColor = colors.ObtainRGB("primaryColor");
        playerTextDraw[13].BackColor = 255;
        playerTextDraw[13].BoxColor = 50;
        playerTextDraw[13].UseBox = false;
        playerTextDraw[13].Proportional = true;
        playerTextDraw[13].Selectable = false;

        playerTextDraw[14] = worldService.CreatePlayerTextDraw(player, new Vector2(344.000, 225.000), "LD_SPAC:white");
        playerTextDraw[14].Font = (TextDrawFont)4;
        playerTextDraw[14].LetterSize = new Vector2(0.564, 1.598);
        playerTextDraw[14].TextSize = new Vector2(94.500f, 18.000f);
        playerTextDraw[14].Outline = 1;
        playerTextDraw[14].Shadow = 0;
        playerTextDraw[14].Alignment = (TextDrawAlignment)2;
        playerTextDraw[14].ForeColor = -50529025;
        playerTextDraw[14].BackColor = 255;
        playerTextDraw[14].BoxColor = 9109759;
        playerTextDraw[14].UseBox = true;
        playerTextDraw[14].Proportional = true;
        playerTextDraw[14].Selectable = true;

        playerTextDraw[15] = worldService.CreatePlayerTextDraw(player, new Vector2(390.000, 227.000), correctTextStrings.Correct($"Recuperar cuenta"));
        playerTextDraw[15].Font = (TextDrawFont)1;
        playerTextDraw[15].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[15].TextSize = new Vector2(400.000000f, 17.000000f);
        playerTextDraw[15].Outline = 0;
        playerTextDraw[15].Shadow = 0;
        playerTextDraw[15].Alignment = (TextDrawAlignment)2;
        playerTextDraw[15].ForeColor = 255;
        playerTextDraw[15].BackColor = 255;
        playerTextDraw[15].BoxColor = 50;
        playerTextDraw[15].UseBox = false;
        playerTextDraw[15].Proportional = true;
        playerTextDraw[15].Selectable = false;

        playerTextDraw[16] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 245.000), "LD_SPAC:white");
        playerTextDraw[16].Font = (TextDrawFont)4;
        playerTextDraw[16].LetterSize = new Vector2(0.565, 1.598);
        playerTextDraw[16].TextSize = new Vector2(143.500f, 18.000f);
        playerTextDraw[16].Outline = 1;
        playerTextDraw[16].Shadow = 0;
        playerTextDraw[16].Alignment = (TextDrawAlignment)2;
        playerTextDraw[16].ForeColor = -50529025;
        playerTextDraw[16].BackColor = 255;
        playerTextDraw[16].BoxColor = 9109759;
        playerTextDraw[16].UseBox = true;
        playerTextDraw[16].Proportional = true;
        playerTextDraw[16].Selectable = true;

        playerTextDraw[17] = worldService.CreatePlayerTextDraw(player, new Vector2(366.000, 248.000), correctTextStrings.Correct("Crear cuenta"));
        playerTextDraw[17].Font = (TextDrawFont)1;
        playerTextDraw[17].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[17].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[17].Outline = 0;
        playerTextDraw[17].Shadow = 0;
        playerTextDraw[17].Alignment = (TextDrawAlignment)2;
        playerTextDraw[17].ForeColor = 255;
        playerTextDraw[17].BackColor = 255;
        playerTextDraw[17].BoxColor = 50;
        playerTextDraw[17].UseBox = false;
        playerTextDraw[17].Proportional = true;
        playerTextDraw[17].Selectable = false;

        playerTextDraw[18] = worldService.CreatePlayerTextDraw(player, new Vector2(322.000, 318.000), correctTextStrings.Correct(serverInformation.WebSite));
        playerTextDraw[18].Font = (TextDrawFont)1;
        playerTextDraw[18].LetterSize = new Vector2(0.287499, 1.299998);
        playerTextDraw[18].TextSize = new Vector2(400.000000f, 17.000000f);
        playerTextDraw[18].Outline = 0;
        playerTextDraw[18].Shadow = 0;
        playerTextDraw[18].Alignment = (TextDrawAlignment)2;
        playerTextDraw[18].ForeColor = -1;
        playerTextDraw[18].BackColor = 255;
        playerTextDraw[18].BoxColor = 50;
        playerTextDraw[18].UseBox = false;
        playerTextDraw[18].Proportional = true;
        playerTextDraw[18].Selectable = false;

        player.AddComponent<LoginLayoutComponent>((object)playerTextDraw);
        Show(player);
    }

    public void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Show(player);

        foreach (PlayerTextDraw playerTextdraw in GetLoginLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Show();

        if (player.IsSelectingTextDraw is false)
            player.SelectTextDraw(0x393939ff);
    }

    public void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Hide(player);

        foreach (PlayerTextDraw playerTextdraw in GetLoginLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Hide();
    }

    public void Destroy(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Hide(player);

        foreach (PlayerTextDraw playerTextdraw in GetLoginLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Destroy();
    }

    public LoginLayoutComponent GetLoginLayoutComponent(Player player)
    {
        return player.GetComponent<LoginLayoutComponent>() ?? throw new InvalidOperationException($"The '{nameof(LoginLayoutComponent)}' component is not attached to the player");
    }

    private static dynamic GetRandomCharacter()
    {
        dynamic[] characters =
        [
            new { modelId = "mdl-1000:police_character", positionX = 121.0f, positionY = 86.0f, letterSizeY = 0.6f, letterSizeX = 10.3f, width = 140.0f, height = 280.0f },
            new { modelId = "mdl-1000:tramp_character", positionX = 90.0f, positionY = 66.0f, letterSizeY = 0.6f, letterSizeX = 10.3f, width = 240.0f, height = 280.0f },
            new { modelId = "mdl-1000:mafia_character", positionX = 88.0f, positionY = 66.0f, letterSizeY = 0.6f, letterSizeX = 10.3f, width = 240.0f, height = 280.0f },
            new { modelId = "mdl-1000:soldier_character", positionX = 101.0f, positionY = 58.0f, letterSizeY = 0.6f, letterSizeX = 10.3f, width = 240.0f, height = 280.0f }
        ];

        Random random = new();
        return characters[random.Next(characters.Length)];
    }
}