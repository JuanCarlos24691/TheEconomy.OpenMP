using System;
using System.Linq;
using System.Collections.Generic;
using SampSharp.Entities.SAMP;
using TheEconomy.Database.Entity.Character;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Characters.Interfaces;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Components;
using TheEconomy.Server.Resources.Authenticator.Characters.Components;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.Characters.Layouts;

public class CharactersLayout(IWorldService worldService, IServerInformation serverInformation, ICorrectTextStrings correctTextStrings, IColors colors, IBlackBackgroundLayout blackBackgroundLayout) : ICharactersLayout
{
    public void Create(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (player.GetComponent<CharactersLayoutComponent>()?.PlayerTextDrawings is not null)
            return;

        AccountComponent accountComponent = player.GetComponent<AccountComponent>();

        if (accountComponent?.Account?.Characters is null)
        {
            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que tu entidad no cuenta con los componentes necesarios para realizar esta acción; por favor, vuelve a intentarlo.");
            return;
        }

        List<CharacterEntity> characters = [.. accountComponent.Account.Characters];

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

        playerTextDraw[4] = worldService.CreatePlayerTextDraw(player, new Vector2(366.000, 126.000), correctTextStrings.Correct("Seleccionar personaje"));
        playerTextDraw[4].Font = (TextDrawFont)1;
        playerTextDraw[4].LetterSize = new Vector2(0.508, 1.950);
        playerTextDraw[4].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[4].Outline = 0;
        playerTextDraw[4].Shadow = 0;
        playerTextDraw[4].Alignment = (TextDrawAlignment)2;
        playerTextDraw[4].ForeColor = -1;
        playerTextDraw[4].BackColor = 255;
        playerTextDraw[4].Proportional = true;

        playerTextDraw[5] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 149.000), "LD_SPAc:white");
        playerTextDraw[5].Font = (TextDrawFont)4;
        playerTextDraw[5].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[5].TextSize = new Vector2(42.500f, 104.000f);
        playerTextDraw[5].Outline = 2;
        playerTextDraw[5].Shadow = 0;
        playerTextDraw[5].Alignment = (TextDrawAlignment)2;
        playerTextDraw[5].ForeColor = 320018175;
        playerTextDraw[5].BackColor = 16843263;
        playerTextDraw[5].BoxColor = 320017407;
        playerTextDraw[5].UseBox = true;
        playerTextDraw[5].Proportional = true;
        playerTextDraw[5].Selectable = true;

        if (characters.Count > 0 && characters[0] is not null)
        {
            playerTextDraw[6] = worldService.CreatePlayerTextDraw(player, new Vector2(270.000, 147.000), "_");
            playerTextDraw[6].Font = (TextDrawFont)5;
            playerTextDraw[6].TextSize = new Vector2(90.000f, 104.000f);
            playerTextDraw[6].Outline = 0;
            playerTextDraw[6].Shadow = 0;
            playerTextDraw[6].Alignment = (TextDrawAlignment)1;
            playerTextDraw[6].ForeColor = -1;
            playerTextDraw[6].BackColor = 0;
            playerTextDraw[6].Proportional = false;
            playerTextDraw[6].PreviewModel = characters[0].Appearance;
            playerTextDraw[6].SetPreviewRotation(new Vector3(0.000, 0.000, -53.000), 1.0f);
        }
        else
        {
            playerTextDraw[6] = worldService.CreatePlayerTextDraw(player, new Vector2(302.000, 180.000), "mdl-1000:icon_plus");
            playerTextDraw[6].Font = (TextDrawFont)4;
            playerTextDraw[6].LetterSize = new Vector2(0.600, 10.300);
            playerTextDraw[6].TextSize = new Vector2(28.000f, 35.000f);
            playerTextDraw[6].Outline = 1;
            playerTextDraw[6].Shadow = 0;
            playerTextDraw[6].Alignment = (TextDrawAlignment)2;
            playerTextDraw[6].ForeColor = -1;
            playerTextDraw[6].BackColor = 255;
            playerTextDraw[6].BoxColor = 135;
            playerTextDraw[6].UseBox = true;
            playerTextDraw[6].Proportional = true;
        }

        playerTextDraw[7] = worldService.CreatePlayerTextDraw(player, new Vector2(342.000, 149.000), "LD_SPAc:white");
        playerTextDraw[7].Font = (TextDrawFont)4;
        playerTextDraw[7].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[7].TextSize = new Vector2(42.500f, 104.000f);
        playerTextDraw[7].Outline = 2;
        playerTextDraw[7].Shadow = 0;
        playerTextDraw[7].Alignment = (TextDrawAlignment)2;
        playerTextDraw[7].ForeColor = 320018175;
        playerTextDraw[7].BackColor = 16843263;
        playerTextDraw[7].BoxColor = 320017407;
        playerTextDraw[7].UseBox = true;
        playerTextDraw[7].Proportional = true;
        playerTextDraw[7].Selectable = true;

        if (characters.Count > 1 && characters[1] is not null)
        {
            playerTextDraw[8] = worldService.CreatePlayerTextDraw(player, new Vector2(317.000, 147.000), "_");
            playerTextDraw[8].Font = (TextDrawFont)5;
            playerTextDraw[8].TextSize = new Vector2(90.000f, 104.000f);
            playerTextDraw[8].Outline = 0;
            playerTextDraw[8].Shadow = 0;
            playerTextDraw[8].Alignment = (TextDrawAlignment)1;
            playerTextDraw[8].ForeColor = -1;
            playerTextDraw[8].BackColor = 0;
            playerTextDraw[8].Proportional = false;
            playerTextDraw[8].PreviewModel = characters[0].Appearance;
            playerTextDraw[8].SetPreviewRotation(new Vector3(0.000, 0.000, -53.000), 1.0f);
        }
        else
        {
            playerTextDraw[8] = worldService.CreatePlayerTextDraw(player, new Vector2(349.000, 180.000), "mdl-1000:icon_plus");
            playerTextDraw[8].Font = (TextDrawFont)4;
            playerTextDraw[8].LetterSize = new Vector2(0.600, 10.300);
            playerTextDraw[8].TextSize = new Vector2(28.000f, 35.000f);
            playerTextDraw[8].Outline = 1;
            playerTextDraw[8].Shadow = 0;
            playerTextDraw[8].Alignment = (TextDrawAlignment)2;
            playerTextDraw[8].ForeColor = -1;
            playerTextDraw[8].BackColor = 255;
            playerTextDraw[8].BoxColor = 135;
            playerTextDraw[8].UseBox = true;
            playerTextDraw[8].Proportional = true;
        }

        playerTextDraw[9] = worldService.CreatePlayerTextDraw(player, new Vector2(389.000, 149.000), "LD_SPAc:white");
        playerTextDraw[9].Font = (TextDrawFont)4;
        playerTextDraw[9].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[9].TextSize = new Vector2(42.500f, 104.000f);
        playerTextDraw[9].Outline = 2;
        playerTextDraw[9].Shadow = 0;
        playerTextDraw[9].Alignment = (TextDrawAlignment)2;
        playerTextDraw[9].ForeColor = 320018175;
        playerTextDraw[9].BackColor = 16843263;
        playerTextDraw[9].BoxColor = 320017407;
        playerTextDraw[9].UseBox = true;
        playerTextDraw[9].Proportional = true;
        playerTextDraw[9].Selectable = true;

        if (characters.Count > 2 && characters[2] is not null)
        {
            playerTextDraw[10] = worldService.CreatePlayerTextDraw(player, new Vector2(363.000, 147.000), "_");
            playerTextDraw[10].Font = (TextDrawFont)5;
            playerTextDraw[10].TextSize = new Vector2(90.000f, 104.000f);
            playerTextDraw[10].Outline = 0;
            playerTextDraw[10].Shadow = 0;
            playerTextDraw[10].Alignment = (TextDrawAlignment)1;
            playerTextDraw[10].ForeColor = -1;
            playerTextDraw[10].BackColor = 0;
            playerTextDraw[10].Proportional = false;
            playerTextDraw[10].PreviewModel = characters[0].Appearance;
            playerTextDraw[8].SetPreviewRotation(new Vector3(0.000, 0.000, -53.000), 1.0f);
        }
        else
        {
            playerTextDraw[10] = worldService.CreatePlayerTextDraw(player, new Vector2(396.000, 180.000), "mdl-1000:icon_plus");
            playerTextDraw[10].Font = (TextDrawFont)4;
            playerTextDraw[10].LetterSize = new Vector2(0.600, 10.300);
            playerTextDraw[10].TextSize = new Vector2(28.000f, 35.000f);
            playerTextDraw[10].Outline = 1;
            playerTextDraw[10].Shadow = 0;
            playerTextDraw[10].Alignment = (TextDrawAlignment)2;
            playerTextDraw[10].ForeColor = -1;
            playerTextDraw[10].BackColor = 255;
            playerTextDraw[10].BoxColor = 135;
            playerTextDraw[10].UseBox = true;
            playerTextDraw[10].Proportional = true;
        }

        playerTextDraw[11] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 255.000), "LD_SPAc:white");
        playerTextDraw[11].Font = (TextDrawFont)4;
        playerTextDraw[11].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[11].TextSize = new Vector2(136.500f, 18.000f);
        playerTextDraw[11].Outline = 2;
        playerTextDraw[11].Shadow = 0;
        playerTextDraw[11].Alignment = (TextDrawAlignment)2;
        playerTextDraw[11].ForeColor = 320018175;
        playerTextDraw[11].BackColor = 16843263;
        playerTextDraw[11].BoxColor = 320017407;
        playerTextDraw[11].UseBox = true;
        playerTextDraw[11].Proportional = true;
        playerTextDraw[11].Selectable = true;

        playerTextDraw[12] = worldService.CreatePlayerTextDraw(player, new Vector2(363.000, 258.000), correctTextStrings.Correct("Selecciona un personaje"));
        playerTextDraw[12].Font = (TextDrawFont)1;
        playerTextDraw[12].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[12].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[12].Outline = 0;
        playerTextDraw[12].Shadow = 0;
        playerTextDraw[12].Alignment = (TextDrawAlignment)2;
        playerTextDraw[12].ForeColor = -1448498689;
        playerTextDraw[12].BackColor = 255;
        playerTextDraw[12].Proportional = true;

        playerTextDraw[13] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 282.000), "LD_SPAc:white");
        playerTextDraw[13].Font = (TextDrawFont)4;
        playerTextDraw[13].LetterSize = new Vector2(0.564, 1.598);
        playerTextDraw[13].TextSize = new Vector2(67.000f, 18.000f);
        playerTextDraw[13].Outline = 2;
        playerTextDraw[13].Shadow = 0;
        playerTextDraw[13].Alignment = (TextDrawAlignment)2;
        playerTextDraw[13].ForeColor = colors.ObtainRGB("secondaryGreen");
        playerTextDraw[13].BackColor = 255;
        playerTextDraw[13].BoxColor = 9109759;
        playerTextDraw[13].UseBox = true;
        playerTextDraw[13].Proportional = true;
        playerTextDraw[13].Selectable = true;

        playerTextDraw[14] = worldService.CreatePlayerTextDraw(player, new Vector2(328.000, 285.000), "Jugar");
        playerTextDraw[14].Font = (TextDrawFont)1;
        playerTextDraw[14].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[14].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[14].Outline = 0;
        playerTextDraw[14].Shadow = 0;
        playerTextDraw[14].Alignment = (TextDrawAlignment)2;
        playerTextDraw[14].ForeColor = colors.ObtainRGB("primaryGreen");
        playerTextDraw[14].BackColor = 255;
        playerTextDraw[14].Proportional = true;

        playerTextDraw[15] = worldService.CreatePlayerTextDraw(player, new Vector2(364.000, 282.000), "LD_SPAc:white");
        playerTextDraw[15].Font = (TextDrawFont)4;
        playerTextDraw[15].LetterSize = new Vector2(0.564, 1.598);
        playerTextDraw[15].TextSize = new Vector2(67.000f, 18.000f);
        playerTextDraw[15].Outline = 2;
        playerTextDraw[15].Shadow = 0;
        playerTextDraw[15].Alignment = (TextDrawAlignment)2;
        playerTextDraw[15].ForeColor = colors.ObtainRGB("secondaryRed");
        playerTextDraw[15].BackColor = 255;
        playerTextDraw[15].BoxColor = 9109759;
        playerTextDraw[15].UseBox = true;
        playerTextDraw[15].Proportional = true;
        playerTextDraw[15].Selectable = true;

        playerTextDraw[16] = worldService.CreatePlayerTextDraw(player, new Vector2(397.000, 285.000), "Eliminar");
        playerTextDraw[16].Font = (TextDrawFont)1;
        playerTextDraw[16].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[16].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[16].Outline = 0;
        playerTextDraw[16].Shadow = 0;
        playerTextDraw[16].Alignment = (TextDrawAlignment)2;
        playerTextDraw[16].ForeColor = colors.ObtainRGB("primaryRed");
        playerTextDraw[16].BackColor = 255;
        playerTextDraw[16].Proportional = true;

        playerTextDraw[17] = worldService.CreatePlayerTextDraw(player, new Vector2(320.000, 318.000), correctTextStrings.Correct(serverInformation.WebSite));
        playerTextDraw[17].Font = (TextDrawFont)1;
        playerTextDraw[17].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[17].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[17].Outline = 0;
        playerTextDraw[17].Shadow = 0;
        playerTextDraw[17].Alignment = (TextDrawAlignment)2;
        playerTextDraw[17].ForeColor = -1;
        playerTextDraw[17].BackColor = 255;
        playerTextDraw[17].Proportional = true;

        player.AddComponent<CharactersLayoutComponent>((object)playerTextDraw);
        Show(player);
    }

    public void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Show(player);

        foreach (PlayerTextDraw playerTextdraw in GetCharactersLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Show();

        if (player.IsSelectingTextDraw is false)
            player.SelectTextDraw(0x393939ff);
    }

    public void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Hide(player);

        foreach (PlayerTextDraw playerTextdraw in GetCharactersLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Hide();
    }

    public void Destroy(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Hide(player);

        foreach (PlayerTextDraw playerTextdraw in GetCharactersLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Destroy();
    }

    public CharactersLayoutComponent GetCharactersLayoutComponent(Player player)
    {
        return player.GetComponent<CharactersLayoutComponent>() ?? throw new InvalidOperationException($"The '{nameof(CharactersLayoutComponent)}' component is not attached to the player");
    }

    private static dynamic GetRandomCharacter()
    {
        dynamic[] characters =
        [
            new { modelId = "mdl-1000:police_character", positionX = 121.0f, positionY = 86.0f, letterSizeY = 0.6f, letterSizeX = 10.3f, width = 140.0f, height = 280.0f },
            new { modelId = "mdl-1000:tramp_character", positionX = 90.0f, positionY = 66.0f, letterSizeY = 0.6f, letterSizeX = 10.3f, width = 240.0f, height = 280.0f },
            new { modelId = "mdl-1000:mafia_character", positionX = 88.0f, positionY = 66.0f, letterSizeY = 0.6f, letterSizeX = 10.3f, width = 240.0f, height = 280.0f },
        ];

        Random random = new();
        return characters[random.Next(characters.Length)];
    }
}