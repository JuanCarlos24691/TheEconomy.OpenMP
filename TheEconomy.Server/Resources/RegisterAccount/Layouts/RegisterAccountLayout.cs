using SampSharp.Entities.SAMP;
using System;
using System.Linq;
using TheEconomy.Server.Resources.RegisterAccount.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.RegisterAccount.Components;

namespace TheEconomy.Server.Resources.RegisterAccount.Layouts;

public class RegisterAccountLayout(IWorldService worldService, IServerInformation serverInformation, ICorrectTextStrings correctTextStrings, IColors colors) : IRegisterAccountLayout
{
    public void Create(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        PlayerTextDraw[] playerTextDraw = new PlayerTextDraw[21];

        playerTextDraw[0] = worldService.CreatePlayerTextDraw(player, position: new Vector2(-5.0f, -5.0f), "_");
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

        playerTextDraw[1] = worldService.CreatePlayerTextDraw(player, new Vector2(320.000000, 110.000000), "_");
        playerTextDraw[1].Font = (TextDrawFont)1;
        playerTextDraw[1].LetterSize = new Vector2(0.933332, 25.300003);
        playerTextDraw[1].TextSize = new Vector2(298.500, 310.000);
        playerTextDraw[1].Outline = 1;
        playerTextDraw[1].Shadow = 0;
        playerTextDraw[1].Alignment = (TextDrawAlignment)2;
        playerTextDraw[1].ForeColor = -1;
        playerTextDraw[1].BackColor = 255;
        playerTextDraw[1].BoxColor = 255;
        playerTextDraw[1].UseBox = true;
        playerTextDraw[1].Proportional = true;
        playerTextDraw[1].Selectable = false;

        playerTextDraw[2] = worldService.CreatePlayerTextDraw(player, new Vector2(456.000000, 115.000000), "mdl-1000:icon_cancel");
        playerTextDraw[2].Font = (TextDrawFont)4;
        playerTextDraw[2].LetterSize = new Vector2(0.600000, 10.300003);
        playerTextDraw[2].TextSize = new Vector2(13.000, 16.000);
        playerTextDraw[2].Outline = 1;
        playerTextDraw[2].Shadow = 0;
        playerTextDraw[2].Alignment = (TextDrawAlignment)2;
        playerTextDraw[2].ForeColor = -1;
        playerTextDraw[2].BackColor = 255;
        playerTextDraw[2].BoxColor = 135;
        playerTextDraw[2].UseBox = true;
        playerTextDraw[2].Proportional = true;
        playerTextDraw[2].Selectable = true;

        dynamic character = RegisterAccountLayout.GetRandomCharacter();

        playerTextDraw[3] = worldService.CreatePlayerTextDraw(player, new Vector2(character.positionX, character.positionY), character.modelId);
        playerTextDraw[3].Font = TextDrawFont.DrawSprite;
        playerTextDraw[3].LetterSize = new Vector2(character.letterSizeY, character.letterSizeX);
        playerTextDraw[3].TextSize = new Vector2(character.width, character.height);
        playerTextDraw[3].Outline = 1;
        playerTextDraw[3].Shadow = 0;
        playerTextDraw[3].Alignment = TextDrawAlignment.Center;
        playerTextDraw[3].ForeColor = -1;
        playerTextDraw[3].BackColor = 255;
        playerTextDraw[3].BoxColor = 255;
        playerTextDraw[3].UseBox = true;
        playerTextDraw[3].Proportional = true;
        playerTextDraw[3].Selectable = false;

        playerTextDraw[4] = worldService.CreatePlayerTextDraw(player, new Vector2(366.000, 135.000), correctTextStrings.ObtainCorrection("Registrar cuenta"));
        playerTextDraw[4].Font = TextDrawFont.Normal;
        playerTextDraw[4].LetterSize = new Vector2(0.508, 1.950);
        playerTextDraw[4].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[4].Outline = 0;
        playerTextDraw[4].Shadow = 0;
        playerTextDraw[4].Alignment = TextDrawAlignment.Center;
        playerTextDraw[4].ForeColor = -1;
        playerTextDraw[4].BackColor = 255;
        playerTextDraw[4].Proportional = true;

        playerTextDraw[5] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 169.000), "LD_SPAc:white");
        playerTextDraw[5].Font = TextDrawFont.DrawSprite;
        playerTextDraw[5].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[5].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[5].Outline = 2;
        playerTextDraw[5].Shadow = 0;
        playerTextDraw[5].Alignment = TextDrawAlignment.Center;
        playerTextDraw[5].ForeColor = 320017407;
        playerTextDraw[5].BackColor = 16843263;
        playerTextDraw[5].BoxColor = 320017407;
        playerTextDraw[5].UseBox = true;
        playerTextDraw[5].Proportional = true;

        playerTextDraw[6] = worldService.CreatePlayerTextDraw(player, new Vector2(299.000, 170.000), "mdl-1000:icon_profile");
        playerTextDraw[6].Font = TextDrawFont.DrawSprite;
        playerTextDraw[6].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[6].TextSize = new Vector2(12.500f, 15.500f);
        playerTextDraw[6].Outline = 1;
        playerTextDraw[6].Shadow = 0;
        playerTextDraw[6].Alignment = TextDrawAlignment.Center;
        playerTextDraw[6].ForeColor = -1;
        playerTextDraw[6].BackColor = 255;
        playerTextDraw[6].BoxColor = 135;
        playerTextDraw[6].UseBox = true;
        playerTextDraw[6].Proportional = true;

        playerTextDraw[7] = worldService.CreatePlayerTextDraw(player, new Vector2(368.000, 172.000), player.Name);
        playerTextDraw[7].Font = TextDrawFont.Normal;
        playerTextDraw[7].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[7].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[7].Outline = 0;
        playerTextDraw[7].Shadow = 0;
        playerTextDraw[7].Alignment = TextDrawAlignment.Center;
        playerTextDraw[7].ForeColor = -1448498689;
        playerTextDraw[7].BackColor = 255;
        playerTextDraw[7].Proportional = true;

        playerTextDraw[8] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 189.000), "LD_SPAc:white");
        playerTextDraw[8].Font = TextDrawFont.DrawSprite;
        playerTextDraw[8].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[8].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[8].Outline = 2;
        playerTextDraw[8].Shadow = 0;
        playerTextDraw[8].Alignment = TextDrawAlignment.Center;
        playerTextDraw[8].ForeColor = 320017407;
        playerTextDraw[8].BackColor = 16843263;
        playerTextDraw[8].BoxColor = 320017407;
        playerTextDraw[8].UseBox = true;
        playerTextDraw[8].Proportional = true;
        playerTextDraw[8].Selectable = true;

        playerTextDraw[9] = worldService.CreatePlayerTextDraw(player, new Vector2(299.000, 192.000), "mdl-1000:icon_password");
        playerTextDraw[9].Font = TextDrawFont.DrawSprite;
        playerTextDraw[9].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[9].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[9].Outline = 1;
        playerTextDraw[9].Shadow = 0;
        playerTextDraw[9].Alignment = TextDrawAlignment.Center;
        playerTextDraw[9].ForeColor = -1;
        playerTextDraw[9].BackColor = 255;
        playerTextDraw[9].BoxColor = 135;
        playerTextDraw[9].UseBox = true;
        playerTextDraw[9].Proportional = true;

        playerTextDraw[10] = worldService.CreatePlayerTextDraw(player, new Vector2(367.000, 189.400), "........");
        playerTextDraw[10].Font = TextDrawFont.Normal;
        playerTextDraw[10].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[10].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[10].Outline = 0;
        playerTextDraw[10].Shadow = 0;
        playerTextDraw[10].Alignment = TextDrawAlignment.Center;
        playerTextDraw[10].ForeColor = -1448498689;
        playerTextDraw[10].BackColor = 255;
        playerTextDraw[10].Proportional = true;

        playerTextDraw[11] = worldService.CreatePlayerTextDraw(player, new Vector2(440.000, 189.000), "LD_SPAc:white");
        playerTextDraw[11].Font = TextDrawFont.DrawSprite;
        playerTextDraw[11].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[11].TextSize = new Vector2(14.500f, 17.500f);
        playerTextDraw[11].Outline = 2;
        playerTextDraw[11].Shadow = 0;
        playerTextDraw[11].Alignment = TextDrawAlignment.Center;
        playerTextDraw[11].ForeColor = 320017407;
        playerTextDraw[11].BackColor = 16843263;
        playerTextDraw[11].BoxColor = 320017407;
        playerTextDraw[11].UseBox = true;
        playerTextDraw[11].Proportional = true;
        playerTextDraw[11].Selectable = true;

        playerTextDraw[12] = worldService.CreatePlayerTextDraw(player, new Vector2(442.000, 192.000), "mdl-1000:icon_show_password");
        playerTextDraw[12].Font = TextDrawFont.DrawSprite;
        playerTextDraw[12].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[12].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[12].Outline = 1;
        playerTextDraw[12].Shadow = 0;
        playerTextDraw[12].Alignment = TextDrawAlignment.Center;
        playerTextDraw[12].ForeColor = -1;
        playerTextDraw[12].BackColor = 255;
        playerTextDraw[12].BoxColor = 135;
        playerTextDraw[12].UseBox = true;
        playerTextDraw[12].Proportional = true;

        playerTextDraw[13] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 209.000), "LD_SPAc:white");
        playerTextDraw[13].Font = TextDrawFont.DrawSprite;
        playerTextDraw[13].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[13].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[13].Outline = 2;
        playerTextDraw[13].Shadow = 0;
        playerTextDraw[13].Alignment = TextDrawAlignment.Center;
        playerTextDraw[13].ForeColor = 320017407;
        playerTextDraw[13].BackColor = 16843263;
        playerTextDraw[13].BoxColor = 320017407;
        playerTextDraw[13].UseBox = true;
        playerTextDraw[13].Proportional = true;
        playerTextDraw[13].Selectable = true;

        playerTextDraw[14] = worldService.CreatePlayerTextDraw(player, new Vector2(299.000, 212.000), "mdl-1000:icon_email");
        playerTextDraw[14].Font = TextDrawFont.DrawSprite;
        playerTextDraw[14].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[14].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[14].Outline = 1;
        playerTextDraw[14].Shadow = 0;
        playerTextDraw[14].Alignment = TextDrawAlignment.Center;
        playerTextDraw[14].ForeColor = -1;
        playerTextDraw[14].BackColor = 255;
        playerTextDraw[14].BoxColor = 135;
        playerTextDraw[14].UseBox = true;
        playerTextDraw[14].Proportional = true;

        playerTextDraw[15] = worldService.CreatePlayerTextDraw(player, new Vector2(369.000, 212.000), correctTextStrings.ObtainCorrection("Correo Electrónico"));
        playerTextDraw[15].Font = TextDrawFont.Normal;
        playerTextDraw[15].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[15].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[15].Outline = 0;
        playerTextDraw[15].Shadow = 0;
        playerTextDraw[15].Alignment = TextDrawAlignment.Center;
        playerTextDraw[15].ForeColor = -1448498689;
        playerTextDraw[15].BackColor = 255;
        playerTextDraw[15].Proportional = true;

        playerTextDraw[16] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 238.000), "LD_SPAc:white");
        playerTextDraw[16].Font = TextDrawFont.DrawSprite;
        playerTextDraw[16].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[16].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[16].Outline = 2;
        playerTextDraw[16].Shadow = 0;
        playerTextDraw[16].Alignment = TextDrawAlignment.Center;
        playerTextDraw[16].ForeColor = colors.ObtainRGB("secondaryColor");
        playerTextDraw[16].BackColor = 16843263;
        playerTextDraw[16].BoxColor = -11710977;
        playerTextDraw[16].UseBox = true;
        playerTextDraw[16].Proportional = true;
        playerTextDraw[16].Selectable = true;

        playerTextDraw[17] = worldService.CreatePlayerTextDraw(player, new Vector2(367.000, 240.000), "Siguiente");
        playerTextDraw[17].Font = TextDrawFont.Normal;
        playerTextDraw[17].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[17].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[17].Outline = 0;
        playerTextDraw[17].Shadow = 0;
        playerTextDraw[17].Alignment = TextDrawAlignment.Center;
        playerTextDraw[17].ForeColor = colors.ObtainRGB("primaryColor");
        playerTextDraw[17].BackColor = 255;
        playerTextDraw[17].Proportional = true;

        playerTextDraw[18] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 257.000), "LD_SPAc:white");
        playerTextDraw[18].Font = TextDrawFont.DrawSprite;
        playerTextDraw[18].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[18].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[18].Outline = 2;
        playerTextDraw[18].Shadow = 0;
        playerTextDraw[18].Alignment = TextDrawAlignment.Center;
        playerTextDraw[18].ForeColor = -84215041;
        playerTextDraw[18].BackColor = 16843263;
        playerTextDraw[18].BoxColor = 320017407;
        playerTextDraw[18].UseBox = true;
        playerTextDraw[18].Proportional = true;
        playerTextDraw[18].Selectable = true;

        playerTextDraw[19] = worldService.CreatePlayerTextDraw(player, new Vector2(367.000, 259.000), correctTextStrings.ObtainCorrection("Iniciar sesión"));
        playerTextDraw[19].Font = TextDrawFont.Normal;
        playerTextDraw[19].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[19].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[19].Outline = 0;
        playerTextDraw[19].Shadow = 0;
        playerTextDraw[19].Alignment = TextDrawAlignment.Center;
        playerTextDraw[19].ForeColor = 255;
        playerTextDraw[19].BackColor = 255;
        playerTextDraw[19].Proportional = true;

        playerTextDraw[20] = worldService.CreatePlayerTextDraw(player, new Vector2(322.000, 318.000), correctTextStrings.ObtainCorrection(serverInformation.WebSite));
        playerTextDraw[20].Font = TextDrawFont.Normal;
        playerTextDraw[20].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[20].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[20].Outline = 0;
        playerTextDraw[20].Shadow = 0;
        playerTextDraw[20].Alignment = TextDrawAlignment.Center;
        playerTextDraw[20].ForeColor = -1448498689;
        playerTextDraw[20].BackColor = 255;
        playerTextDraw[20].Proportional = true;

        player.AddComponent<RegisterAccountComponent>((object)playerTextDraw);
    }

    public void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        foreach (PlayerTextDraw playerTextdraw in GetRegisterAccountComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw.Show();
    }

    public void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        foreach (PlayerTextDraw playerTextdraw in GetRegisterAccountComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw.Hide();
    }

    public void Destroy(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        foreach (PlayerTextDraw playerTextdraw in GetRegisterAccountComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw.Destroy();
    }

    private RegisterAccountComponent GetRegisterAccountComponent(Player player)
    {
        return player.GetComponent<RegisterAccountComponent>() ?? throw new InvalidOperationException($"The '{nameof(RegisterAccountComponent)}' component is not attached to the player");
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