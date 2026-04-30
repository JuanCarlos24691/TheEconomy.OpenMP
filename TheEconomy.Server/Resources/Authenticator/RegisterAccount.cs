using System;
using System.Collections.Generic;
using System.Linq;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Components;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;

namespace TheEconomy.Server.Resources.Systems.PlayerSystems.UserAuthentication
{
    /* public class RegisterAccount(IWorldService worldService, IServerInformation serverInformation, IColors color, ICorrectTextStrings correctTextStrings) : ISystem
    {
        private string registerAccountPassword;
        private string registerAccountEmail;
        private bool showRegisterAccountUserPassword;

        private static readonly PlayerTextDraw[] playerTextDraw = new PlayerTextDraw[21];

        private InputDialog userPasswordConfirmationDialog;
        private InputDialog userEmailConfirmationDialog;
        private InputDialog loginUserNameConfirmationDialog;
        private MessageDialog dialogConfirmationFromRegister;

        public void StartRegistration(Player player)
        {
            PlayerData playerData = player.GetComponent<PlayerData>();

            if (playerData.Account is null)
            {
                playerTextDraw[0] = worldService.CreatePlayerTextDraw(player, new Vector2(319.438100, 0.000000), "_");
                playerTextDraw[0].Font = (TextDrawFont)1;
                playerTextDraw[0].LetterSize = new Vector2(0.612500, 49.565000);
                playerTextDraw[0].TextSize = new Vector2(389.500, 638.083);
                playerTextDraw[0].Outline = 1;
                playerTextDraw[0].Shadow = 0;
                playerTextDraw[0].Alignment = (TextDrawAlignment)2;
                playerTextDraw[0].ForeColor = -1;
                playerTextDraw[0].BackColor = 255;
                playerTextDraw[0].BoxColor = 320018163;
                playerTextDraw[0].UseBox = true;
                playerTextDraw[0].Proportional = true;
                playerTextDraw[0].Selectable = false;

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

                Dictionary<int, dynamic> registerAccountTextDrawingCharacterSettings = new()
                {
                    [0] = new
                    {
                        modelId = "mdl-1000:police_character",
                        positionX = 121.000000,
                        positionY = 86.000000,
                        letterSizeY = 0.600000,
                        letterSizeX = 10.300003,
                        width = 140.000000f,
                        height = 280.000000f
                    },
                    [1] = new
                    {
                        modelId = "mdl-1000:tramp_character",
                        positionX = 90.000000,
                        positionY = 66.000000,
                        letterSizeY = 0.600000,
                        letterSizeX = 10.300003,
                        width = 240.000000f,
                        height = 280.000000f
                    },
                    [2] = new
                    {
                        modelId = "mdl-1000:mafia_character",
                        positionX = 88.000000,
                        positionY = 66.000000,
                        letterSizeY = 0.600000,
                        letterSizeX = 10.300003,
                        width = 240.000000f,
                        height = 280.000000f
                    },
                    [3] = new
                    {
                        modelId = "mdl-1000:soldier_character",
                        positionX = 101.000000,
                        positionY = 58.000000,
                        letterSizeY = 0.600000,
                        letterSizeX = 10.300003,
                        width = 240.000000f,
                        height = 280.000000f
                    }
                };

                int random = new Random().Next(registerAccountTextDrawingCharacterSettings.Count);
                playerTextDraw[3] = worldService.CreatePlayerTextDraw(player, new Vector2(registerAccountTextDrawingCharacterSettings[random].positionX, registerAccountTextDrawingCharacterSettings[random].positionY), registerAccountTextDrawingCharacterSettings[random].modelId);
                playerTextDraw[3].Font = TextDrawFont.DrawSprite;
                playerTextDraw[3].LetterSize = new Vector2(registerAccountTextDrawingCharacterSettings[random].letterSizeY, registerAccountTextDrawingCharacterSettings[random].letterSizeX);
                playerTextDraw[3].TextSize = new Vector2(registerAccountTextDrawingCharacterSettings[random].width, registerAccountTextDrawingCharacterSettings[random].height);
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
                playerTextDraw[16].ForeColor = color.ObtainRGB("secondaryColor");
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
                playerTextDraw[17].ForeColor = color.ObtainRGB("primaryColor");
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

                ShowPlayerTextDraw(player, playerTextDraw);
            }
        }

        private static void ShowPlayerTextDraw(Player player, PlayerTextDraw[] playerTextDraw)
        {
            foreach (PlayerTextDraw playerTextdraw in playerTextDraw.Where(t => t is not null))
                playerTextdraw.Show();

            if (player.IsSelectingTextDraw is false)
                player.SelectTextDraw(0x393939ff);
        }

        private static void HidePlayerTextDraw(Player player, PlayerTextDraw[] playerTextDraw)
        {
            foreach (PlayerTextDraw playerTextdraw in playerTextDraw.Where(t => t is not null && t.IsComponentAlive))
                playerTextdraw.Hide();

            if (player.IsSelectingTextDraw)
                player.CancelSelectTextDraw();
        }

        private static void DestroyPlayerTextDraw(Player player, PlayerTextDraw[] playerTextDraw)
        {
            foreach (PlayerTextDraw playerTextdraw in playerTextDraw.Where(t => t is not null && t.IsComponentAlive))
                playerTextdraw.Destroy();

            if (player.IsSelectingTextDraw)
                player.CancelSelectTextDraw();
        }

        [Event]
        public void OnPlayerClickTextDraw(Player player)
        {
            player.SendClientMessage("200");
        } */

        /* public virtual void RegisterEvents(BaseMode gameMode) =>
            gameMode.PlayerClickPlayerTextDraw += PlayerClickPlayerTextDraw;

        private void PlayerClickPlayerTextDraw(object sender, ClickPlayerTextDrawEventArgs e)
        {
            if (e.PlayerTextDraw == playerTextDraw[2])
            {
                HidePlayerTextDraw(e.Player);

                MessageDialog dialogButtonExit = new($"{{{Colors.PrimaryRedColorHexadecimal}}}Cancelar registro de cuenta", $"{{FFFFFF}}¿Realmente desees cancelar el registro de una nueva cuenta?", "Sí", "No");
                dialogButtonExit.Response += DialogButtonExitEvent;
                dialogButtonExit.Show(e.Player);
                e.Player.PlaySound(1058);
            }
            else if (e.PlayerTextDraw == playerTextDraw[8])
            {
                HidePlayerTextDraw(e.Player);

                bool passwordAndEmailReady;

                if (!string.IsNullOrEmpty(registerAccountPassword) && !string.IsNullOrEmpty(registerAccountEmail))
                    passwordAndEmailReady = true;
                else
                    passwordAndEmailReady = false;

                userPasswordConfirmationDialog = new($"{{{Colors.PrimaryColorHexadecimal}}}Registrar cuenta", $"{{FFFFFF}}Hola, {{{Colors.PrimaryColorHexadecimal}}}{e.Player.Name}{{FFFFFF}}, ingresa una contraseña para continuar con el registro." +
                    $"\n\n\t{(string.IsNullOrEmpty(registerAccountPassword) ? "{FFFFFF}" : "{454545}")}1): Contraseña" +
                    $"\n\t{(string.IsNullOrEmpty(registerAccountEmail) ? "{FFFFFF}" : "{454545}")}2): Correo Electrónico" +
                    $"\n\t{(passwordAndEmailReady ? $"{{{Colors.PrimaryColorHexadecimal}}}" : "{FFFFFF}")}3): ¡Listo!" +
                    $"\n\n{{869BB4}}La contraseña debe estar entre 8 y 16 caracteres.", false, "Siguiente", "Atras")
                { Style = DialogStyle.Password };
                userPasswordConfirmationDialog.Response += UserPasswordConfirmationEvent;
                userPasswordConfirmationDialog.Show(e.Player);
                e.Player.PlaySound(1058);
            }
            else if (e.PlayerTextDraw == playerTextDraw[11])
            {
                if (!string.IsNullOrEmpty(registerAccountPassword))
                {
                    if (!showRegisterAccountUserPassword)
                    {
                        playerTextDraw[10].Position = new Vector2(367.000, 191.000);
                        playerTextDraw[10].Text = CorrectTextStrings.Correct(registerAccountPassword);
                        showRegisterAccountUserPassword = true;
                    }
                    else
                    {
                        playerTextDraw[10].Position = new Vector2(367.000, 189.400);
                        playerTextDraw[10].Text = "";

                        for (int i = 0; i < registerAccountPassword.Length; i++)
                            playerTextDraw[10].Text += ".";

                        showRegisterAccountUserPassword = false;
                    }
                }
                else
                {
                    e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}No has ingresado una contraseña para tu cuenta. Por favor vuelve a intentarlo.");
                    e.Player.PlaySound(1085);
                }
            }
            else if (e.PlayerTextDraw == playerTextDraw[13])
            {
                HidePlayerTextDraw(e.Player);

                bool passwordAndEmailReady;

                if (!string.IsNullOrEmpty(registerAccountPassword) && !string.IsNullOrEmpty(registerAccountEmail))
                    passwordAndEmailReady = true;
                else
                    passwordAndEmailReady = false;

                userEmailConfirmationDialog = new($"{{{Colors.PrimaryColorHexadecimal}}}Registrar cuenta", $"{{FFFFFF}}¡Ya casi! {{{Colors.PrimaryColorHexadecimal}}}{e.Player.Name}{{FFFFFF}}, vas por buen camino, ahora ingresa un\nCorreo electronico válido para continuar." +
                    $"\n\n\t{(string.IsNullOrEmpty(registerAccountPassword) ? "{FFFFFF}" : "{454545}")}1): Contraseña" +
                    $"\n\t{(string.IsNullOrEmpty(registerAccountEmail) ? "{FFFFFF}" : "{454545}")}2): Correo Electrónico" +
                    $"\n\t{(passwordAndEmailReady ? $"{{{Colors.PrimaryColorHexadecimal}}}" : "{FFFFFF}")}3): ¡Listo!" +
                    $"\n\n{{869BB4}}La dirección de correo electrónico debe ser parecida a juancarlos@gmail.com, por ejemplo\nY tener una longitud entre 4 y 319 caracteres.", false, "Siguiente", "Atras");
                userEmailConfirmationDialog.Response += UserEmailConfirmationDialogEvent;
                userEmailConfirmationDialog.Show(e.Player);
                e.Player.PlaySound(1058);
            }
            else if (e.PlayerTextDraw == playerTextDraw[16])
            {
                if (!string.IsNullOrEmpty(registerAccountPassword))
                {
                    if (!string.IsNullOrEmpty(registerAccountEmail))
                    {
                        HidePlayerTextDraw(e.Player);

                        dialogConfirmationFromRegister = new($"{{{Colors.PrimaryColorHexadecimal}}}Registrar cuenta", $"{{FFFFFF}}¡EnHorabuena! {{{Colors.PrimaryColorHexadecimal}}}{e.Player.Name}{{FFFFFF}}, Has terminado de configurar tu cuenta.\nLo siguiente es modelar las características de un nuevo personaje." +
                            $"\n\n\t{(string.IsNullOrEmpty(registerAccountPassword) ? "{FFFFFF}" : "{454545}")}1): Contraseña" +
                            $"\n\t{(string.IsNullOrEmpty(registerAccountEmail) ? "{FFFFFF}" : "{454545}")}2): Correo Electrónico" +
                            $"\n\t{{454545}}3): ¡Listo!" +
                            $"\n\n{{869BB4}}Si hay alguna información que quieras modificar de la cuenta, pulsa el botón 'Atrás'. De lo contrario, pulsa el botón 'Configurar'.", "Configurar", "Atras");
                        dialogConfirmationFromRegister.Response += DialogConfirmationFromRegisterEvent;
                        dialogConfirmationFromRegister.Show(e.Player);
                        e.Player.PlaySound(1058);
                    }
                    else
                    {
                        e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Parece que no haz ingresado un correo electronico para cuenta. Por favor vuelve a intentarlo.");
                        e.Player.PlaySound(1085);
                    }
                }
                else
                {
                    e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Parece que no haz ingresado una contraseña para cuenta. Por favor vuelve a intentarlo.");
                    e.Player.PlaySound(1085);
                }
            }
            else if (e.PlayerTextDraw == playerTextDraw[18])
            {
                /* if (account.Exists(e.Player.Name))
                {
                    HidePlayerTextDraw(e.Player);

                    MessageDialog loginExistingUserNameConfirmationDialog = new($"{{{Colors.PrimaryColorHexadecimal}}}Iniciar sesión", $"{{FFFFFF}}Ey {{{Colors.PrimaryColorHexadecimal}}}{e.Player.Name}{{FFFFFF}}, Ya existe una cuenta con tu nombre de usuario.\n¿Deseas iniciar sesión con esta cuenta?", "Sí", "No");
                    loginExistingUserNameConfirmationDialog.Response += LoginExistingUserNameConfirmationDialogEvent;
                    loginExistingUserNameConfirmationDialog.Show(e.Player);
                    e.Player.PlaySound(1058);
                }
                else
                {
                    HidePlayerTextDraw(e.Player);

                    loginUserNameConfirmationDialog = new($"{{{Colors.PrimaryColorHexadecimal}}}Iniciar sesión", $"{{FFFFFF}}¡Ey! {{{Colors.PrimaryColorHexadecimal}}}{e.Player.Name}{{FFFFFF}}, Parece que quieres iniciar sesión en una cuenta existente\nPor favor, ingresa el nombre de usuario de una cuenta existente a continuación para continuar.", false, "Continuar", "Atras");
                    loginUserNameConfirmationDialog.Response += LoginUserNameConfirmationDialogEvent;
                    loginUserNameConfirmationDialog.Show(e.Player);
                    e.Player.PlaySound(1058);
                } 
            }
        }

        private void DialogButtonExitEvent(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                ShowPlayerTextDraw(e.Player);
                e.Player.PlaySound(1085);
            }
            else if (e.DialogButton == DialogButton.Left)
            {
                DisposePlayerTextDraw(e.Player);
                ChatClean.Clean(e.Player);
                e.Player.PlaySound(1058);
            }
        }

        private void UserPasswordConfirmationEvent(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                ShowPlayerTextDraw(e.Player);
                e.Player.PlaySound(1085);
            }
            else if (e.DialogButton == DialogButton.Left)
            {
                if (!string.IsNullOrEmpty(e.InputText))
                {
                    if (e.InputText.Length >= 8 && e.InputText.Length <= 32)
                    {
                        registerAccountPassword = e.InputText;
                        playerTextDraw[10].Text = null;

                        for (int i = 0; i < e.InputText.Length; i++)
                            playerTextDraw[10].Text += ".";

                        showRegisterAccountUserPassword = false;

                        ShowPlayerTextDraw(e.Player);
                        e.Player.PlaySound(1058);
                    }
                    else
                    {
                        e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Debes ingresar una contraseña válida antes de continuar.");
                        e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Recuerda que estas deben tener una longitud entre 8 y 32 caracteres. Por favor vuelve a intentarlo.");

                        userPasswordConfirmationDialog.Show(e.Player);
                        e.Player.PlaySound(1085);
                    }
                }
                else
                {
                    e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Parece que no haz ingresado una contraseña para tu cuenta. Por favor vuelve a intentarlo.");
                    userPasswordConfirmationDialog.Show(e.Player);
                    e.Player.PlaySound(1085);
                }
            }
        }

        private void UserEmailConfirmationDialogEvent(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                ShowPlayerTextDraw(e.Player);
                e.Player.PlaySound(1085);
            }
            else if (e.DialogButton == DialogButton.Left)
            {
                if (!string.IsNullOrEmpty(e.InputText))
                {
                    if (e.InputText.Length >= 4 && e.InputText.Length <= 319)
                    {
                        if (CheckEmail.Check(e.InputText))
                        {
                            registerAccountEmail = e.InputText;
                            playerTextDraw[15].Text = CorrectTextStrings.Correct(e.InputText);
                            ShowPlayerTextDraw(e.Player);
                            e.Player.PlaySound(1058);
                        }
                        else
                        {
                            e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Debes ingresar un correo electrónico válido antes de continuar..");
                            e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Y estos deben tener un formato parecido a juanca24691@gmail.com. Por favor vuelve a intentarlo.");
                            userEmailConfirmationDialog.Show(e.Player);
                            e.Player.PlaySound(1085);
                        }
                    }
                    else
                    {
                        e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Debes ingresar un correo electrónico válido antes de continuar.");
                        e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Recuerda que los correo electrónicos deben tener una longitud entre 4 y 319 caracteres.");
                        e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Y estos deben tener un formato parecido a juanca24691@gmail.com. Por favor vuelve a intentarlo.");

                        userEmailConfirmationDialog.Show(e.Player);
                        e.Player.PlaySound(1085);
                    }
                }
                else
                {
                    e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Parece que no haz ingresado un correo electrónico para cuenta. Por favor vuelve a intentarlo.");
                    userEmailConfirmationDialog.Show(e.Player);
                    e.Player.PlaySound(1085);
                }
            }
        }

        private void DialogConfirmationFromRegisterEvent(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                ShowPlayerTextDraw(e.Player);
                e.Player.PlaySound(1085);
            }
            else if (e.DialogButton == DialogButton.Left)
            {
                _ = Task.Run(() => account.CreateNewAccountAsync(e.Player.Name, registerAccountPassword, registerAccountEmail, 0));
                DisposePlayerTextDraw(e.Player);
                _ = new SelectCharacter(e.Player);
                e.Player.PlaySound(1058);
            }
        }

        private void LoginUserNameConfirmationDialogEvent(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                ShowPlayerTextDraw(e.Player);
                e.Player.PlaySound(1085);
            }
            else if (e.DialogButton == DialogButton.Left)
            {
                if (!string.IsNullOrEmpty(e.InputText))
                {
                    if (!UserIsLoggedIn.LoggedIn(e.InputText))
                    {
                        ICheckUserName.Check(e.Player, e.InputText, true).ContinueWith(t =>
                        {
                            if (t.IsCompletedSuccessfully && t.Result)
                            {
                                /* if (account.Exists(e.InputText))
                                {
                                    e.Player.Name = e.InputText;
                                    DisposePlayerTextDraw(e.Player);
                                    _ = new Login(e.Player);
                                    e.Player.PlaySound(1058);
                                }
                                else
                                {
                                    e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}El nombre de usuario que ingresaste no coincide con ninguna cuenta existente. Por favor vuelve a intentarlo.");
                                    loginUserNameConfirmationDialog.Show(e.Player);
                                    e.Player.PlaySound(1085);
                                } 
                            }
                            else
                                loginUserNameConfirmationDialog.Show(e.Player);
                        });
                    }
                    else
                    {
                        e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}El nombre de usuario que has proporcionado ya se encuentra conectado en el servidor. Por favor vuelve a intentarlo.");
                        loginUserNameConfirmationDialog.Show(e.Player);
                        e.Player.PlaySound(1085);
                    }

                }
                else
                {
                    e.Player.SendClientMessage($"{{{Colors.PrimaryRedColorHexadecimal}}}Parece que no haz ingresado un nombre de usuario para iniciar sesión. Por favor vuelve a intentarlo.");
                    loginUserNameConfirmationDialog.Show(e.Player);
                    e.Player.PlaySound(1085);
                }
            }
        }

        private void LoginExistingUserNameConfirmationDialogEvent(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                loginUserNameConfirmationDialog = new($"{{{Colors.PrimaryColorHexadecimal}}}Iniciar sesión", $"{{FFFFFF}}¡Ey! {{{Colors.PrimaryColorHexadecimal}}}{e.Player.Name}{{FFFFFF}}, Parece que quieres iniciar sesión en una cuenta existente\nPor favor, ingresa el nombre de usuario de una cuenta existente a continuación para continuar.", false, "Continuar", "Atras");
                loginUserNameConfirmationDialog.Response += LoginUserNameConfirmationDialogEvent;
                loginUserNameConfirmationDialog.Show(e.Player);
                e.Player.PlaySound(1058);
            }
            else if (e.DialogButton == DialogButton.Left)
            {
                DisposePlayerTextDraw(e.Player);
                _ = new Login(e.Player);

                e.Player.PlaySound(1058);
            }
        } 
    } */
}
