using System;
using System.Linq;
using SampSharp.Entities.SAMP;
using SampSharp.Entities;
using TheEconomy.Server.Resources.Services;
using TheEconomy.Server.Resources.Components;

namespace TheEconomy.Server.Resources.Systems.PlayerSystems.UserAuthentication
{
    public class VerifyProhibition(IWorldService worldService, ServerInformation serverInformation, CorrectTextStrings correctTextStrings) : ISystem
    {
        public bool ObtainVerification(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "La entidad player no puede ser nula. Por favor, asegúrese de que se haya inicializado correctamente.");

            PlayerData playerData = player.GetComponent<PlayerData>();

            string[] paragraphs = new string[6];

            if (playerData.Prohibition is not null)
            {
                paragraphs[0] = "Prohibido";
                paragraphs[1] = correctTextStrings.ObtainCorrection($"Hola {player.Name}, fuiste prohibido del servidor por el administrador {playerData.Prohibition.ProhibitedBy}.");
                paragraphs[2] = correctTextStrings.ObtainCorrection($"Razón: {playerData.Prohibition.Reason} - Fecha: {playerData.Prohibition.DateOfProhibition}");
                paragraphs[3] = correctTextStrings.ObtainCorrection($"Sí, quieres apelar a esta decision, puedes contactarnos en el Foro({serverInformation.Forum})");
                paragraphs[4] = correctTextStrings.ObtainCorrection($"o en nuestro discord({serverInformation.Discord})");
                paragraphs[5] = correctTextStrings.ObtainCorrection("Tenga en cuenta que no todos pueden apelar a un desbaneo por diversos motivos");
            }
            else if (playerData.Account is not null)
            {
                paragraphs[0] = correctTextStrings.ObtainCorrection("Cuenta restringida");
                paragraphs[1] = correctTextStrings.ObtainCorrection($"Hola {player.Name}, esta cuenta fue prohibida por el administrador {playerData.Account.AccountProhibitedBy}.");
                paragraphs[2] = correctTextStrings.ObtainCorrection($"Razón: {playerData.Account.ReasonForProhibition} - Fecha: {playerData.Account.DateOfProhibition}{(playerData.Account.ProhibitedAccount > 0 ? $" - Días ({playerData.Account.ProhibitedAccount})" : "")}");
                paragraphs[3] = correctTextStrings.ObtainCorrection($"Sí, quieres apelar a esta decision, puedes contactarnos en el Foro({serverInformation.Forum})");
                paragraphs[4] = correctTextStrings.ObtainCorrection($"o en nuestro discord({serverInformation.Discord})");
                paragraphs[5] = correctTextStrings.ObtainCorrection("Tenga en cuenta que no todos pueden apelar a un desbaneo por diversos motivos");
            }
            else return false;

            PlayerTextDraw[] playerTextDraw = new PlayerTextDraw[8];

            playerTextDraw[0] = worldService.CreatePlayerTextDraw(player, new Vector2(319.438100, 0.000000), "_");
            playerTextDraw[0].Font = TextDrawFont.Normal;
            playerTextDraw[0].LetterSize = new Vector2(0.612500, 49.565000);
            playerTextDraw[0].TextSize = new Vector2(389.500, 638.083);
            playerTextDraw[0].Outline = 1;
            playerTextDraw[0].Shadow = 0;
            playerTextDraw[0].Alignment = TextDrawAlignment.Center;
            playerTextDraw[0].ForeColor = -1;
            playerTextDraw[0].BackColor = 255;
            playerTextDraw[0].BoxColor = 320018163;
            playerTextDraw[0].UseBox = true;
            playerTextDraw[0].Proportional = true;

            playerTextDraw[1] = worldService.CreatePlayerTextDraw(player, new Vector2(298.000, 75.000), "mdl-1000:icon_prohibited");
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

            playerTextDraw[2] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 124.000), paragraphs[0]);
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

            playerTextDraw[3] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 150.000), paragraphs[1]);
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

            playerTextDraw[4] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 162.0000), paragraphs[2]);
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

            playerTextDraw[5] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 174.000), paragraphs[3]);
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

            playerTextDraw[6] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 185.000), paragraphs[4]);
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

            playerTextDraw[7] = worldService.CreatePlayerTextDraw(player, new Vector2(321.000, 211.000), paragraphs[5]);
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

            ShowPlayerTextDraw(player, playerTextDraw);

            return true;
        }

        private static void ShowPlayerTextDraw(Player player, PlayerTextDraw[] playerTextDraw)
        {
            foreach (PlayerTextDraw playerTextdraw in playerTextDraw.Where(t => t is not null))
                playerTextdraw.Show();

            if (player.IsSelectingTextDraw is false)
                player.SelectTextDraw(0x393939ff);
        }
    }
}