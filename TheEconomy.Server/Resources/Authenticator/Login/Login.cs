using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database;
using TheEconomy.Database.Entity.Account;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Components;
using TheEconomy.Server.Resources.Authenticator.Login.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using TheEconomy.Server.Resources.Services.IsPlayerConnect.Interfaces;
using TheEconomy.Server.Resources.Components.AccountInformation;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Characters.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.Login;

public class Login(DatabaseContext databaseContext, IDialogService dialogService, ICorrectTextStrings correctTextStrings, IIsPlayerConnect isPlayerConnect, IVerifyUserName verifyUserName, IColors colors, ILoginLayout loginLayout, IRegisterAccountLayout registerAccountLayout, ICharactersLayout charactersLayout) : ISystem
{
    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        LoginLayoutComponent loginLayoutComponent = player.GetComponent<LoginLayoutComponent>();

        if (loginLayoutComponent is not null && loginLayoutComponent.IsComponentAlive && loginLayoutComponent.PlayerTextDrawings is not null)
        {
            LoginComponent loginComponent = player.GetComponent<LoginComponent>() ?? player.AddComponent<LoginComponent>();

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
                            DestroyLoginComponents(player);

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
                            else if (inputDialogResponse.InputText == player.Name)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Ya estas Iniciando sesión en esa cuenta; por favor, vuelve a intentarlo.");
                                return;
                            }
                            else if (isPlayerConnect.Verify(inputDialogResponse.InputText))
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}El nombre de usuario ingresado ya se encuentra conectado; por favor, vuelve a intentarlo.");
                                return;
                            }
                            else if (await databaseContext.Accounts.AnyAsync(a => a.Name == inputDialogResponse.InputText) is false)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}El nombre de usuario que ingresaste no coincide con ninguna cuenta existente. Por favor vuelve a intentarlo.");
                                return;
                            }

                            player.Name = inputDialogResponse.InputText;

                            loginLayoutComponent.PlayerTextDrawings[6].Text = correctTextStrings.Correct(inputDialogResponse.InputText);
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}El Nombre del Usuario se establecio correctamente para su Inicio de sesión.");
                        }
                        else if (inputDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite al Inicio de sesión.");
                        }
                        break;
                    }
                case 7:
                    {
                        loginLayout.Hide(player);
                        player.PlaySound(1085);

                        InputDialog inputDialog = new()
                        {
                            Caption = $"{colors.GetHexadecimal("primaryColor")}Contraseña de la Cuenta",
                            Content = $"{colors.GetHexadecimal("primaryWhite")}Para ingresar a tu cuenta escribe tu Contraseña a continuación:\n\nFecha de ingreso: {colors.GetHexadecimal("primaryColor")}{DateTime.Now}{colors.GetHexadecimal("primaryWhite")}",
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
                            if (string.IsNullOrEmpty(inputDialogResponse.InputText) || inputDialogResponse.InputText.Length < 8 || inputDialogResponse.InputText.Length > 128)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}La contraseña de la cuenta es inválida");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Recuerda que esta debe tener una longitud de 8 a 128 caracteres");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Por favor, vuelve a intentarlo.");
                                return;
                            }

                            loginComponent.Password = inputDialogResponse.InputText;
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}La contraseña de la cuenta se ha establecio correctamente para su Inicio de sesión.");
                        }
                        else if (inputDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite al Inicio de sesión.");
                        }
                        break;
                    }
                case 10:
                    {
                        if (string.IsNullOrEmpty(loginComponent.Password))
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}No has ingresado una contraseña para tu cuenta. Por favor vuelve a intentarlo.");
                            return;
                        }

                        if (loginLayoutComponent.ShowPassword)
                        {
                            loginLayoutComponent.PlayerTextDrawings[9].Text = new string('.', loginComponent.Password.Length);
                            loginLayoutComponent.ShowPassword = false;

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Ocultaste la contraseña.");
                        }
                        else
                        {
                            loginLayoutComponent.ShowPassword = true;

                            loginLayoutComponent.PlayerTextDrawings[9].Text = correctTextStrings.Correct(loginComponent.Password);
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Mostraste la contraseña.");
                        }
                        break;
                    }
                case 12:
                    {
                        player.PlaySound(1085);

                        if (string.IsNullOrEmpty(loginComponent.Password))
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no tienes asignado una contraseña para Iniciar Sesión; por favor, vuelve a intentarlo.");
                            return;
                        }
                        else if (loginComponent.Password.Length < 8 || loginComponent.Password.Length > 128)
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}La contraseña de la cuenta es inválida");
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Recuerda que esta debe tener una longitud de 8 a 128 caracteres");
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Por favor, vuelve a intentarlo.");
                            return;
                        }

                        loginLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Iniciar Sesión", $"{colors.GetHexadecimal("primaryWhite")}Estás a punto de Iniciar Sesión\n¿Deseas continuar?", "Continuar", "Cancelar");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        player.PlaySound(1085);

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            string password = await databaseContext.Accounts.Where(a => a.Name == player.Name).Select(a => a.Password).FirstOrDefaultAsync();

                            if (string.IsNullOrEmpty(password))
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}No se ha podido encontrar ninguna cuenta que coincida con la contraseña ingresada.");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Por favor, vuelve a intentarlo.");
                                return;
                            }

                            PasswordHasher<object> hasher = new();
                            bool isValidPassword = hasher.VerifyHashedPassword(null, password, loginComponent.Password) == PasswordVerificationResult.Success;

                            if (isValidPassword)
                            {
                                DestroyLoginComponents(player);

                                AccountEntity account = await databaseContext.Accounts.Include(a => a.Characters).FirstOrDefaultAsync(a => a.Name == player.Name);

                                if (account is not null)
                                {
                                    player.AddComponent(new AccountInformation { Account = account });
                                    charactersLayout.Create(player);

                                    player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Has iniciado sesión correctamente en tu cuenta.");
                                }
                                else
                                {
                                    player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}No se ha podido encontrar tu cuenta en la base de datos; por favor, vuelve a intentarlo.");
                                }
                            }
                            else
                            {
                                loginLayout.Show(player);
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}La contraseña ingresada es incorrecta; por favor, vuelve a intentarlo.");
                            }
                        }
                        break;
                    }
                case 14:
                    {
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Esta función aún está en desarrollo.");
                        break;
                    }
                case 16:
                    {
                        loginLayout.Hide(player);
                        player.PlaySound(1085);

                        InputDialog inputDialog = new()
                        {
                            Caption = $"{colors.GetHexadecimal("primaryColor")}Registrar nueva cuenta",
                            Content = $"{colors.GetHexadecimal("primaryWhite")}¡Ey! {colors.GetHexadecimal("primaryColor")}{player.Name}{colors.GetHexadecimal("primaryWhite")}, Parece que quieres crear una neuva cuenta\nPor favor, ingresa el nombre de usuario de la nueva cuenta continuación para continuar.",
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
                            else if (inputDialogResponse.InputText == player.Name)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Ya estas Iniciando sesión en esa cuenta; por favor, vuelve a intentarlo.");
                                return;
                            }
                            else if (isPlayerConnect.Verify(inputDialogResponse.InputText))
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}El nombre de usuario ingresado ya se encuentra conectado; por favor, vuelve a intentarlo.");
                                return;
                            }
                            else if (await databaseContext.Accounts.AnyAsync(a => a.Name == inputDialogResponse.InputText) is true)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Ya existe una cuenta con ese nombre de usuario; Por favor vuelve a intentarlo.");
                                return;
                            }

                            DestroyLoginComponents(player);
                            player.Name = inputDialogResponse.InputText;
                            registerAccountLayout.Create(player);

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
        player.DestroyComponents<LoginComponent>();
    }
}