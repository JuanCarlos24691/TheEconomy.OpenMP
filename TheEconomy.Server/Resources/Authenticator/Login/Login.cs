using System;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Components;
using TheEconomy.Server.Resources.Authenticator.Login.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using TheEconomy.Server.Resources.Services.IsPlayerConnect.Interfaces;
using TheEconomy.Server.Resources.Components.AccountInformation;
using Microsoft.EntityFrameworkCore;

namespace TheEconomy.Server.Resources.Authenticator.Login;

public class Login(DatabaseContext databaseContext, IDialogService dialogService, ICorrectTextStrings correctTextStrings, IIsPlayerConnect isPlayerConnect, IVerifyUserName verifyUserName, IColors colors, ILoginLayout loginLayout) : ISystem
{
    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        LoginLayoutComponent loginLayoutComponent = player.GetComponent<LoginLayoutComponent>();

        if (loginLayoutComponent is not null && loginLayoutComponent.IsComponentAlive && loginLayoutComponent.PlayerTextDrawings is not null)
        {
            switch (loginLayoutComponent.PlayerTextDrawings.IndexOf(playerTextDraw))
            {
                case 1:
                    {
                        loginLayout.Hide(player);
                        player.PlaySound(1085);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Cancelar Inicio de sesión", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente desees cancelar el Inicio de sesión?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            player.GetComponent<LoginLayoutComponent>()?.Destroy();

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste el Inicio de sesión.");
                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            loginLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite al Inicio de sesión.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
                case 4:
                    {
                        loginLayout.Hide(player);
                        player.PlaySound(1085);

                        InputDialog inputDialog = new()
                        {
                            Caption = $"{colors.GetHexadecimal("primaryColor")}Cambio de cuenta",
                            Content = $"{colors.GetHexadecimal("primaryWhite")}¡Ey! {colors.GetHexadecimal("primaryColor")}{player.Name}{colors.GetHexadecimal("primaryWhite")}, Parece que quieres Iniciar sesión en una cuenta diferente\nPor favor, ingresa el nombre de usuario de una cuenta existente continuación para continuar.",
                            Button1 = "Siguiente",
                            Button2 = "Atras"
                        };

                        InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                        if (inputDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        loginLayout.Show(player);
                        player.PlaySound(1085);

                        if (inputDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            if (string.IsNullOrEmpty(inputDialogResponse.InputText) || inputDialogResponse.InputText.Length < 10 || inputDialogResponse.InputText.Length > 24)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}El nombre de usuario ingresado es inválido");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Recuerda que este debe tener una longitud de 10 a 24 caracteres");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Por favor, vuelve a intentarlo.");
                                return;
                            }
                            else if (verifyUserName.Verify(inputDialogResponse.InputText) is false)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}El nombre de usuario ingresado es inválido");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Recuerda que este debe tener un formato alfanumérico a-Z 0-9");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Por favor, vuelve a intentarlo.");
                                return;
                            }
                            else if (isPlayerConnect.Verify(inputDialogResponse.InputText))
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}El nombre de usuario ingresado ya se encuentra conectado; por favor, vuelve a intentarlo.");
                                return;
                            }
                            else if (inputDialogResponse.InputText == player.Name)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Ya estas Iniciando sesión en esa cuenta; por favor, vuelve a intentarlo.");
                                return;
                            }
                            else if (await databaseContext.Accounts.AnyAsync(a => a.Name == inputDialogResponse.InputText) is false)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}El nombre de usuario que ingresaste no coincide con ninguna cuenta existente. Por favor vuelve a intentarlo.");
                                return;
                            }

                            AccountInformation accountInformation = player.GetComponent<AccountInformation>();
                            accountInformation.Account = await databaseContext.Accounts.FirstOrDefaultAsync(a => a.Name == inputDialogResponse.InputText);

                            if (accountInformation is null || accountInformation.Account is null)
                            {
                                DestroyLoginComponents(player);
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que tu entidad no cuenta con los componentes necesarios para Iniciar sesión en una nueva cuenta; por favor, vuelve a intentarlo.");
                                return;
                            }

                            player.Name = accountInformation.Account.Name;

                            loginLayoutComponent.PlayerTextDrawings[6].Text = correctTextStrings.Correct(accountInformation.Account.Name);
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}El Nombre del Usuario se establecio correctamente para su Inicio de sesión.");
                        }
                        else if (inputDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite al Inicio de sesión.");
                        }
                        break;
                    }
            }
        }
    }

    private static void DestroyLoginComponents(Player player)
    {
        player.DestroyComponents<LoginLayoutComponent>();
    }
}