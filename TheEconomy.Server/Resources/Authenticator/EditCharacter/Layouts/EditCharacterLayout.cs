using System;
using System.Linq;
using SampSharp.Entities.SAMP;
using TheEconomy.Database.Entity.Character;
using TheEconomy.Server.Resources.Authenticator.EditCharacter.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Authenticator.EditCharacter.Components;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.EditCharacter.Layouts;

public class EditCharacterLayout(IWorldService worldService, IServerInformation serverInformation, ICorrectTextStrings correctTextStrings, IColors colors, IBlackBackgroundLayout blackBackgroundLayout) : IEditCharacterLayout
{
    public void Create(Player player, CharacterEntity characterEntity)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (player.GetComponent<EditCharacterLayoutComponent>()?.PlayerTextDrawings is not null)
            return;

        PlayerTextDraw[] playerTextDraw = new PlayerTextDraw[33];

        playerTextDraw[0] = worldService.CreatePlayerTextDraw(player, new Vector2(320.000000, 110.000000), "_");
        playerTextDraw[0].Font = (TextDrawFont)1;
        playerTextDraw[0].LetterSize = new Vector2(0.933332, 25.300003);
        playerTextDraw[0].TextSize = new Vector2(298.5f, 310.0f);
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

        playerTextDraw[3] = worldService.CreatePlayerTextDraw(player, new Vector2(366.000, 120.000), correctTextStrings.Correct("Editar personaje"));
        playerTextDraw[3].Font = (TextDrawFont)1;
        playerTextDraw[3].LetterSize = new Vector2(0.508, 1.950);
        playerTextDraw[3].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[3].Outline = 0;
        playerTextDraw[3].Shadow = 0;
        playerTextDraw[3].Alignment = (TextDrawAlignment)2;
        playerTextDraw[3].ForeColor = -1;
        playerTextDraw[3].BackColor = 255;
        playerTextDraw[3].BoxColor = 135;
        playerTextDraw[3].Proportional = true;

        playerTextDraw[4] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 146.000), "LD_SPAc:white");
        playerTextDraw[4].Font = (TextDrawFont)4;
        playerTextDraw[4].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[4].TextSize = new Vector2(15.500f, 17.500f);
        playerTextDraw[4].Outline = 2;
        playerTextDraw[4].Shadow = 0;
        playerTextDraw[4].Alignment = (TextDrawAlignment)2;
        playerTextDraw[4].ForeColor = 320017407;
        playerTextDraw[4].BackColor = 16843263;
        playerTextDraw[4].BoxColor = 320017407;
        playerTextDraw[4].UseBox = true;
        playerTextDraw[4].Proportional = true;

        playerTextDraw[5] = worldService.CreatePlayerTextDraw(player, new Vector2(297.000, 148.000), "mdl-1000:icon_profile");
        playerTextDraw[5].Font = (TextDrawFont)4;
        playerTextDraw[5].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[5].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[5].Outline = 1;
        playerTextDraw[5].Shadow = 0;
        playerTextDraw[5].Alignment = (TextDrawAlignment)2;
        playerTextDraw[5].ForeColor = -1;
        playerTextDraw[5].BackColor = 255;
        playerTextDraw[5].BoxColor = 135;
        playerTextDraw[5].UseBox = true;
        playerTextDraw[5].Proportional = true;

        playerTextDraw[6] = worldService.CreatePlayerTextDraw(player, new Vector2(312.000, 146.000), "LD_SPAc:white");
        playerTextDraw[6].Font = (TextDrawFont)4;
        playerTextDraw[6].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[6].TextSize = new Vector2(62.500f, 17.500f);
        playerTextDraw[6].Outline = 2;
        playerTextDraw[6].Shadow = 0;
        playerTextDraw[6].Alignment = (TextDrawAlignment)2;
        playerTextDraw[6].ForeColor = 320017407;
        playerTextDraw[6].BackColor = 16843263;
        playerTextDraw[6].BoxColor = 320017407;
        playerTextDraw[6].UseBox = true;
        playerTextDraw[6].Proportional = true;
        playerTextDraw[6].Selectable = true;

        playerTextDraw[7] = worldService.CreatePlayerTextDraw(player, new Vector2(344.000, 148.000), correctTextStrings.Correct(characterEntity.Name));
        playerTextDraw[7].Font = (TextDrawFont)1;
        playerTextDraw[7].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[7].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[7].Outline = 0;
        playerTextDraw[7].Shadow = 0;
        playerTextDraw[7].Alignment = (TextDrawAlignment)2;
        playerTextDraw[7].ForeColor = -1448498689;
        playerTextDraw[7].BackColor = 255;
        playerTextDraw[7].BoxColor = 135;
        playerTextDraw[7].Proportional = true;

        playerTextDraw[8] = worldService.CreatePlayerTextDraw(player, new Vector2(376.000, 146.000), "LD_SPAc:white");
        playerTextDraw[8].Font = (TextDrawFont)4;
        playerTextDraw[8].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[8].TextSize = new Vector2(62.500f, 17.500f);
        playerTextDraw[8].Outline = 2;
        playerTextDraw[8].Shadow = 0;
        playerTextDraw[8].Alignment = (TextDrawAlignment)2;
        playerTextDraw[8].ForeColor = 320017407;
        playerTextDraw[8].BackColor = 16843263;
        playerTextDraw[8].BoxColor = 320017407;
        playerTextDraw[8].UseBox = true;
        playerTextDraw[8].Proportional = true;
        playerTextDraw[8].Selectable = true;

        playerTextDraw[9] = worldService.CreatePlayerTextDraw(player, new Vector2(406.000, 148.000), correctTextStrings.Correct(characterEntity.LastName));
        playerTextDraw[9].Font = (TextDrawFont)1;
        playerTextDraw[9].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[9].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[9].Outline = 0;
        playerTextDraw[9].Shadow = 0;
        playerTextDraw[9].Alignment = (TextDrawAlignment)2;
        playerTextDraw[9].ForeColor = -1448498689;
        playerTextDraw[9].BackColor = 255;
        playerTextDraw[9].BoxColor = 135;
        playerTextDraw[9].Proportional = true;

        playerTextDraw[10] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 166.000), "LD_SPAc:white");
        playerTextDraw[10].Font = (TextDrawFont)4;
        playerTextDraw[10].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[10].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[10].Outline = 2;
        playerTextDraw[10].Shadow = 0;
        playerTextDraw[10].Alignment = (TextDrawAlignment)2;
        playerTextDraw[10].ForeColor = 320017407;
        playerTextDraw[10].BackColor = 16843263;
        playerTextDraw[10].BoxColor = 320017407;
        playerTextDraw[10].UseBox = true;
        playerTextDraw[10].Proportional = true;
        playerTextDraw[10].Selectable = true;

        playerTextDraw[11] = worldService.CreatePlayerTextDraw(player, new Vector2(297.000, 169.000), "mdl-1000:icon_gender");
        playerTextDraw[11].Font = (TextDrawFont)4;
        playerTextDraw[11].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[11].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[11].Outline = 1;
        playerTextDraw[11].Shadow = 0;
        playerTextDraw[11].Alignment = (TextDrawAlignment)2;
        playerTextDraw[11].ForeColor = -1;
        playerTextDraw[11].BackColor = 255;
        playerTextDraw[11].BoxColor = 135;
        playerTextDraw[11].UseBox = true;
        playerTextDraw[11].Proportional = true;

        playerTextDraw[12] = worldService.CreatePlayerTextDraw(player, new Vector2(364.000, 168.000), characterEntity.Gender == 0 ? "Hombre" : "Mujer");
        playerTextDraw[12].Font = (TextDrawFont)1;
        playerTextDraw[12].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[12].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[12].Outline = 0;
        playerTextDraw[12].Shadow = 0;
        playerTextDraw[12].Alignment = (TextDrawAlignment)2;
        playerTextDraw[12].ForeColor = -1448498689;
        playerTextDraw[12].BackColor = 255;
        playerTextDraw[12].BoxColor = 135;
        playerTextDraw[12].Proportional = true;

        playerTextDraw[13] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 186.000), "LD_SPAc:white");
        playerTextDraw[13].Font = (TextDrawFont)4;
        playerTextDraw[13].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[13].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[13].Outline = 2;
        playerTextDraw[13].Shadow = 0;
        playerTextDraw[13].Alignment = (TextDrawAlignment)2;
        playerTextDraw[13].ForeColor = 320017407;
        playerTextDraw[13].BackColor = 16843263;
        playerTextDraw[13].BoxColor = 320017407;
        playerTextDraw[13].UseBox = true;
        playerTextDraw[13].Proportional = true;
        playerTextDraw[13].Selectable = true;

        playerTextDraw[14] = worldService.CreatePlayerTextDraw(player, new Vector2(297.000, 189.000), "mdl-1000:icon_calendar");
        playerTextDraw[14].Font = (TextDrawFont)4;
        playerTextDraw[14].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[14].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[14].Outline = 1;
        playerTextDraw[14].Shadow = 0;
        playerTextDraw[14].Alignment = (TextDrawAlignment)2;
        playerTextDraw[14].ForeColor = -1;
        playerTextDraw[14].BackColor = 255;
        playerTextDraw[14].BoxColor = 135;
        playerTextDraw[14].UseBox = true;
        playerTextDraw[14].Proportional = true;

        playerTextDraw[15] = worldService.CreatePlayerTextDraw(player, new Vector2(370.000, 188.000), correctTextStrings.Correct(characterEntity.BirthDate.ToString("dd/MM/yyyy")));
        playerTextDraw[15].Font = (TextDrawFont)1;
        playerTextDraw[15].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[15].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[15].Outline = 0;
        playerTextDraw[15].Shadow = 0;
        playerTextDraw[15].Alignment = (TextDrawAlignment)2;
        playerTextDraw[15].ForeColor = -1448498689;
        playerTextDraw[15].BackColor = 255;
        playerTextDraw[15].Proportional = true;

        playerTextDraw[16] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 206.000), "LD_SPAc:white");
        playerTextDraw[16].Font = (TextDrawFont)4;
        playerTextDraw[16].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[16].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[16].Outline = 2;
        playerTextDraw[16].Shadow = 0;
        playerTextDraw[16].Alignment = (TextDrawAlignment)2;
        playerTextDraw[16].ForeColor = 320017407;
        playerTextDraw[16].BackColor = 16843263;
        playerTextDraw[16].BoxColor = 320017407;
        playerTextDraw[16].UseBox = true;
        playerTextDraw[16].Proportional = true;
        playerTextDraw[16].Selectable = true;

        playerTextDraw[17] = worldService.CreatePlayerTextDraw(player, new Vector2(297.000, 208.000), "mdl-1000:icon_ruler");
        playerTextDraw[17].Font = (TextDrawFont)4;
        playerTextDraw[17].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[17].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[17].Outline = 1;
        playerTextDraw[17].Shadow = 0;
        playerTextDraw[17].Alignment = (TextDrawAlignment)2;
        playerTextDraw[17].ForeColor = -1;
        playerTextDraw[17].BackColor = 255;
        playerTextDraw[17].BoxColor = 135;
        playerTextDraw[17].UseBox = true;
        playerTextDraw[17].Proportional = true;

        playerTextDraw[18] = worldService.CreatePlayerTextDraw(player, new Vector2(366.000, 208.000), correctTextStrings.Correct(characterEntity.Height));
        playerTextDraw[18].Font = (TextDrawFont)1;
        playerTextDraw[18].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[18].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[18].Outline = 0;
        playerTextDraw[18].Shadow = 0;
        playerTextDraw[18].Alignment = (TextDrawAlignment)2;
        playerTextDraw[18].ForeColor = -1448498689;
        playerTextDraw[18].BackColor = 255;
        playerTextDraw[18].Proportional = true;

        playerTextDraw[19] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 226.000), "LD_SPAc:white");
        playerTextDraw[19].Font = (TextDrawFont)4;
        playerTextDraw[19].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[19].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[19].Outline = 2;
        playerTextDraw[19].Shadow = 0;
        playerTextDraw[19].Alignment = (TextDrawAlignment)2;
        playerTextDraw[19].ForeColor = 320017407;
        playerTextDraw[19].BackColor = 16843263;
        playerTextDraw[19].BoxColor = 320017407;
        playerTextDraw[19].UseBox = true;
        playerTextDraw[19].Proportional = true;
        playerTextDraw[19].Selectable = true;

        playerTextDraw[20] = worldService.CreatePlayerTextDraw(player, new Vector2(297.000, 228.000), "mdl-1000:icon_eye");
        playerTextDraw[20].Font = (TextDrawFont)4;
        playerTextDraw[20].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[20].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[20].Outline = 1;
        playerTextDraw[20].Shadow = 0;
        playerTextDraw[20].Alignment = (TextDrawAlignment)2;
        playerTextDraw[20].ForeColor = -1;
        playerTextDraw[20].BackColor = 255;
        playerTextDraw[20].BoxColor = 135;
        playerTextDraw[20].UseBox = true;
        playerTextDraw[20].Proportional = true;

        playerTextDraw[21] = worldService.CreatePlayerTextDraw(player, new Vector2(366.000, 228.000), correctTextStrings.Correct(characterEntity.EyeColor));
        playerTextDraw[21].Font = (TextDrawFont)1;
        playerTextDraw[21].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[21].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[21].Outline = 0;
        playerTextDraw[21].Shadow = 0;
        playerTextDraw[21].Alignment = (TextDrawAlignment)2;
        playerTextDraw[21].ForeColor = -1448498689;
        playerTextDraw[21].BackColor = 255;
        playerTextDraw[21].Proportional = true;

        playerTextDraw[22] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 246.000), "LD_SPAc:white");
        playerTextDraw[22].Font = (TextDrawFont)4;
        playerTextDraw[22].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[22].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[22].Outline = 2;
        playerTextDraw[22].Shadow = 0;
        playerTextDraw[22].Alignment = (TextDrawAlignment)2;
        playerTextDraw[22].ForeColor = 320017407;
        playerTextDraw[22].BackColor = 16843263;
        playerTextDraw[22].BoxColor = 320017407;
        playerTextDraw[22].UseBox = true;
        playerTextDraw[22].Proportional = true;
        playerTextDraw[22].Selectable = true;

        playerTextDraw[23] = worldService.CreatePlayerTextDraw(player, new Vector2(297.000, 248.000), "mdl-1000:icon_hair");
        playerTextDraw[23].Font = (TextDrawFont)4;
        playerTextDraw[23].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[23].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[23].Outline = 1;
        playerTextDraw[23].Shadow = 0;
        playerTextDraw[23].Alignment = (TextDrawAlignment)2;
        playerTextDraw[23].ForeColor = -1;
        playerTextDraw[23].BackColor = 255;
        playerTextDraw[23].BoxColor = 135;
        playerTextDraw[23].UseBox = true;
        playerTextDraw[23].Proportional = true;

        playerTextDraw[24] = worldService.CreatePlayerTextDraw(player, new Vector2(369.000, 249.000), correctTextStrings.Correct(characterEntity.HairColor));
        playerTextDraw[24].Font = (TextDrawFont)1;
        playerTextDraw[24].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[24].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[24].Outline = 0;
        playerTextDraw[24].Shadow = 0;
        playerTextDraw[24].Alignment = (TextDrawAlignment)2;
        playerTextDraw[24].ForeColor = -1448498689;
        playerTextDraw[24].BackColor = 255;
        playerTextDraw[24].Proportional = true;

        playerTextDraw[25] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 266.000), "LD_SPAc:white");
        playerTextDraw[25].Font = (TextDrawFont)4;
        playerTextDraw[25].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[25].TextSize = new Vector2(143.500f, 17.500f);
        playerTextDraw[25].Outline = 2;
        playerTextDraw[25].Shadow = 0;
        playerTextDraw[25].Alignment = (TextDrawAlignment)2;
        playerTextDraw[25].ForeColor = 320017407;
        playerTextDraw[25].BackColor = 16843263;
        playerTextDraw[25].BoxColor = 320017407;
        playerTextDraw[25].UseBox = true;
        playerTextDraw[25].Proportional = true;
        playerTextDraw[25].Selectable = true;

        playerTextDraw[26] = worldService.CreatePlayerTextDraw(player, new Vector2(297.000, 269.000), "mdl-1000:icon_skin_color");
        playerTextDraw[26].Font = (TextDrawFont)4;
        playerTextDraw[26].LetterSize = new Vector2(0.600, 10.300);
        playerTextDraw[26].TextSize = new Vector2(11.000f, 13.000f);
        playerTextDraw[26].Outline = 1;
        playerTextDraw[26].Shadow = 0;
        playerTextDraw[26].Alignment = (TextDrawAlignment)2;
        playerTextDraw[26].ForeColor = -1;
        playerTextDraw[26].BackColor = 255;
        playerTextDraw[26].BoxColor = 135;
        playerTextDraw[26].UseBox = true;
        playerTextDraw[26].Proportional = true;

        playerTextDraw[27] = worldService.CreatePlayerTextDraw(player, new Vector2(368.000, 268.000), correctTextStrings.Correct(characterEntity.SkinColor));
        playerTextDraw[27].Font = (TextDrawFont)1;
        playerTextDraw[27].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[27].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[27].Outline = 0;
        playerTextDraw[27].Shadow = 0;
        playerTextDraw[27].Alignment = (TextDrawAlignment)2;
        playerTextDraw[27].ForeColor = -1448498689;
        playerTextDraw[27].BackColor = 255;
        playerTextDraw[27].Proportional = true;

        playerTextDraw[28] = worldService.CreatePlayerTextDraw(player, new Vector2(295.000, 290.000), "LD_SPAc:white");
        playerTextDraw[28].Font = (TextDrawFont)4;
        playerTextDraw[28].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[28].TextSize = new Vector2(70.500f, 17.500f);
        playerTextDraw[28].Outline = 2;
        playerTextDraw[28].Shadow = 0;
        playerTextDraw[28].Alignment = (TextDrawAlignment)2;
        playerTextDraw[28].ForeColor = colors.ObtainRGB("secondaryGreen");
        playerTextDraw[28].BackColor = 16843263;
        playerTextDraw[28].BoxColor = -11710977;
        playerTextDraw[28].UseBox = true;
        playerTextDraw[28].Proportional = true;
        playerTextDraw[28].Selectable = true;

        playerTextDraw[29] = worldService.CreatePlayerTextDraw(player, new Vector2(330.000, 292.000), "Guardar");
        playerTextDraw[29].Font = (TextDrawFont)1;
        playerTextDraw[29].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[29].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[29].Outline = 0;
        playerTextDraw[29].Shadow = 0;
        playerTextDraw[29].Alignment = (TextDrawAlignment)2;
        playerTextDraw[29].ForeColor = colors.ObtainRGB("primaryGreen");
        playerTextDraw[29].BackColor = 255;
        playerTextDraw[29].Proportional = true;

        playerTextDraw[30] = worldService.CreatePlayerTextDraw(player, new Vector2(368.000, 290.000), "LD_SPAc:white");
        playerTextDraw[30].Font = (TextDrawFont)4;
        playerTextDraw[30].LetterSize = new Vector2(0.600, 1.399);
        playerTextDraw[30].TextSize = new Vector2(70.500f, 17.500f);
        playerTextDraw[30].Outline = 2;
        playerTextDraw[30].Shadow = 0;
        playerTextDraw[30].Alignment = (TextDrawAlignment)2;
        playerTextDraw[30].ForeColor = colors.ObtainRGB("secondaryRed");
        playerTextDraw[30].BackColor = 16843263;
        playerTextDraw[30].BoxColor = -11710977;
        playerTextDraw[30].UseBox = true;
        playerTextDraw[30].Proportional = true;
        playerTextDraw[30].Selectable = true;

        playerTextDraw[31] = worldService.CreatePlayerTextDraw(player, new Vector2(401.000, 292.000), "Cancelar");
        playerTextDraw[31].Font = (TextDrawFont)1;
        playerTextDraw[31].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[31].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[31].Outline = 0;
        playerTextDraw[31].Shadow = 0;
        playerTextDraw[31].Alignment = (TextDrawAlignment)2;
        playerTextDraw[31].ForeColor = colors.ObtainRGB("primaryRed");
        playerTextDraw[31].BackColor = 255;
        playerTextDraw[31].Proportional = true;

        playerTextDraw[32] = worldService.CreatePlayerTextDraw(player, new Vector2(320.000, 318.000), correctTextStrings.Correct(serverInformation.WebSite));
        playerTextDraw[32].Font = (TextDrawFont)1;
        playerTextDraw[32].LetterSize = new Vector2(0.287, 1.299);
        playerTextDraw[32].TextSize = new Vector2(400.000f, 17.000f);
        playerTextDraw[32].Outline = 0;
        playerTextDraw[32].Shadow = 0;
        playerTextDraw[32].Alignment = (TextDrawAlignment)2;
        playerTextDraw[32].ForeColor = -1;
        playerTextDraw[32].BackColor = 255;
        playerTextDraw[32].Proportional = true;

        player.AddComponent<EditCharacterLayoutComponent>((object)playerTextDraw);
        Show(player);
    }

    public void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Show(player);

        foreach (PlayerTextDraw playerTextdraw in GetEditCharacterLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Show();

        if (player.IsSelectingTextDraw is false)
            player.SelectTextDraw(0x393939ff);
    }

    public void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Hide(player);

        foreach (PlayerTextDraw playerTextdraw in GetEditCharacterLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Hide();
    }

    public void Destroy(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        blackBackgroundLayout.Hide(player);

        foreach (PlayerTextDraw playerTextdraw in GetEditCharacterLayoutComponent(player).PlayerTextDrawings.Where(t => t is not null))
            playerTextdraw?.Destroy();
    }

    public EditCharacterLayoutComponent GetEditCharacterLayoutComponent(Player player)
    {
        return player.GetComponent<EditCharacterLayoutComponent>() ?? throw new InvalidOperationException($"The '{nameof(EditCharacterLayoutComponent)}' component is not attached to the player");
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