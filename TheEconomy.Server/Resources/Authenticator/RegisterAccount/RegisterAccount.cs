using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Interfaces;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Components;
using TheEconomy.Server.Resources.Services.VerifyMail.Interfaces;
using TheEconomy.Server.Resources.Components.AccountInformation;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using TheEconomy.Server.Resources.Services.IsPlayerConnect.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Characters.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.RegisterAccount;

public class RegisterAccount(DatabaseContext databaseContext, IDialogService dialogService, IVerifyMail verifyMail, ICorrectTextStrings correctTextStrings, IColors colors, IRegisterAccountLayout registerAccountLayout, ICharactersLayout charactersLayout, IVerifyUserName verifyUserName, IIsPlayerConnect isPlayerConnect, ILoginLayout loginLayout) : ISystem
{
    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        RegisterAccountLayoutComponent registerAccountLayoutComponent = player.GetComponent<RegisterAccountLayoutComponent>();

        if (registerAccountLayoutComponent is not null && registerAccountLayoutComponent.IsComponentAlive && registerAccountLayoutComponent.PlayerTextDrawings is not null)
        {
            RegisterAccountComponent registerAccountComponent = player.GetComponent<RegisterAccountComponent>() ?? player.AddComponent<RegisterAccountComponent>();

            switch (registerAccountLayoutComponent.PlayerTextDrawings.IndexOf(playerTextDraw))
            {
                case 1:
                    {
                        registerAccountLayout.Hide(player);
                        player.PlaySound(1085);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Cancelar registro de cuenta", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente desees cancelar el registro de una nueva cuenta?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            player.GetComponent<RegisterAccountComponent>()?.Destroy();
                            player.GetComponent<RegisterAccountLayoutComponent>()?.Destroy();

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste la creacion de la cuenta.");
                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            registerAccountLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la creacion de la cuenta.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
                case 7:
                    {
                        registerAccountLayout.Hide(player);
                        player.PlaySound(1085);

                        StringBuilder stringBuilder = new();

                        stringBuilder.AppendLine($"{colors.GetHexadecimal("primaryWhite")}Hola, {colors.GetHexadecimal("primaryColor")}{player.Name}{colors.GetHexadecimal("primaryWhite")}, ingresa una contraseña para continuar con el registro.");
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine($"\t{(string.IsNullOrEmpty(registerAccountComponent.Account.Password) ? colors.GetHexadecimal("primaryGray") : colors.GetHexadecimal("primaryGreen"))}1): Contraseña");
                        stringBuilder.AppendLine($"\t{(string.IsNullOrEmpty(registerAccountComponent.Account.Mail) ? colors.GetHexadecimal("primaryGray") : colors.GetHexadecimal("primaryGreen"))}2): Correo Electrónico");
                        stringBuilder.AppendLine($"\t{((!string.IsNullOrEmpty(registerAccountComponent.Account?.Password) && !string.IsNullOrEmpty(registerAccountComponent.Account?.Mail)) ? colors.GetHexadecimal("primaryGreen") : colors.GetHexadecimal("primaryGray"))}3): ¡Listo!");
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine($"{colors.GetHexadecimal("secondaryColor")}La contraseña debe estar entre 8 y 16 caracteres.");

                        InputDialog inputDialog = new()
                        {
                            Caption = $"{colors.GetHexadecimal("primaryColor")}Ingresar contraseña",
                            Content = stringBuilder.ToString(),
                            Button1 = "Siguiente",
                            Button2 = "Atras"
                        };

                        InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                        if (inputDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        registerAccountLayout.Show(player);
                        player.PlaySound(1085);

                        if (inputDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            if (string.IsNullOrEmpty(inputDialogResponse.InputText) || inputDialogResponse.InputText.Length < 8 || inputDialogResponse.InputText.Length > 128)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes ingresar una contraseña válida antes de continuar.");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Recuerda que estas deben tener una longitud entre 8 y 32 caracteres. Por favor vuelve a intentarlo.");
                                return;
                            }

                            registerAccountComponent.Account.Password = inputDialogResponse.InputText;
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}La contraseña de tu cuenta se establecio correctamente.");
                        }
                        break;
                    }
                case 10:
                    {
                        if (string.IsNullOrEmpty(registerAccountComponent.Account.Password))
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}No has ingresado una contraseña para tu cuenta. Por favor vuelve a intentarlo.");
                            return;
                        }

                        if (registerAccountLayoutComponent.ShowPassword)
                        {
                            registerAccountLayoutComponent.PlayerTextDrawings[9].Text = new string('.', registerAccountComponent.Account.Password.Length);
                            registerAccountLayoutComponent.ShowPassword = false;

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Ocultaste la contraseña.");
                        }
                        else
                        {
                            registerAccountLayoutComponent.ShowPassword = true;

                            registerAccountLayoutComponent.PlayerTextDrawings[9].Text = correctTextStrings.Correct(registerAccountComponent.Account.Password);
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Mostraste la contraseña.");
                        }
                        break;
                    }
                case 12:
                    {
                        registerAccountLayout.Hide(player);
                        player.PlaySound(1085);

                        StringBuilder stringBuilder = new();

                        stringBuilder.AppendLine($"{colors.GetHexadecimal("primaryWhite")}¡Ya casi! {colors.GetHexadecimal("primaryColor")}{player.Name}{colors.GetHexadecimal("primaryWhite")}, vas por buen camino, ahora ingresa un");
                        stringBuilder.AppendLine("Correo electronico válido para continuar.");
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine($"\t{(string.IsNullOrEmpty(registerAccountComponent.Account.Password) ? colors.GetHexadecimal("primaryGray") : colors.GetHexadecimal("primaryGreen"))}1): Contraseña");
                        stringBuilder.AppendLine($"\t{(string.IsNullOrEmpty(registerAccountComponent.Account.Mail) ? colors.GetHexadecimal("primaryGray") : colors.GetHexadecimal("primaryGreen"))}2): Correo Electrónico");
                        stringBuilder.AppendLine($"\t{((!string.IsNullOrEmpty(registerAccountComponent.Account?.Password) && !string.IsNullOrEmpty(registerAccountComponent.Account?.Mail)) ? colors.GetHexadecimal("primaryGreen") : colors.GetHexadecimal("primaryGray"))}3): ¡Listo!");
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine($"{colors.GetHexadecimal("secondaryColor")}La dirección de correo electrónico debe ser parecida a juancarlos@gmail.com");
                        stringBuilder.AppendLine($"{colors.GetHexadecimal("secondaryColor")}y tener una longitud entre 4 y 319 caracteres.");

                        InputDialog inputDialog = new()
                        {
                            Caption = $"{colors.GetHexadecimal("primaryColor")}Ingresar Mail",
                            Content = stringBuilder.ToString(),
                            Button1 = "Siguiente",
                            Button2 = "Atras"
                        };

                        InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                        if (inputDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        registerAccountLayout.Show(player);
                        player.PlaySound(1085);

                        if (inputDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            if (string.IsNullOrEmpty(inputDialogResponse.InputText) || inputDialogResponse.InputText.Length < 4 || inputDialogResponse.InputText.Length > 319)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes ingresar un correo electrónico válido antes de continuar.");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Recuerda que los correo electrónicos deben tener una longitud entre 4 y 319 caracteres.");
                                return;
                            }
                            else if (!verifyMail.Verify(inputDialogResponse.InputText))
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes ingresar un correo electrónico válido antes de continuar.");
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Y estos deben tener un formato parecido a juanca24691@gmail.com. Por favor vuelve a intentarlo.");
                                return;
                            }

                            registerAccountComponent.Account.Mail = inputDialogResponse.InputText;

                            registerAccountLayoutComponent.PlayerTextDrawings[14].Text = correctTextStrings.Correct(registerAccountComponent.Account.Mail);
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}El Correo Electronico de tu cuenta se establecio correctamente.");
                        }
                        else if (inputDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste la asignación del Correo Electronico de tu Cuenta.");
                        }
                        break;
                    }
                case 15:
                    {
                        player.PlaySound(1085);
                        registerAccountComponent.Account.Name = player.Name;

                        switch (true)
                        {
                            case bool _ when string.IsNullOrEmpty(registerAccountComponent.Account.Name):
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no tienes asignado un nombre de usuario para registrar tu Cuenta; por favor, vuelve a intentarlo.");
                                return;

                            case bool _ when string.IsNullOrEmpty(registerAccountComponent.Account.Password):
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no tienes asignado una contraseña para registrar tu Cuenta; por favor, vuelve a intentarlo.");
                                return;

                            case bool _ when string.IsNullOrEmpty(registerAccountComponent.Account.Mail):
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no tienes asignado un correo electronico para registrar tu Cuenta; por favor, vuelve a intentarlo.");
                                return;

                            case bool _ when await databaseContext.Accounts.AnyAsync(a => a.Name == registerAccountComponent.Account.Name):
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que tu nombre de usuario ya se encuentra registrado; por favor, vuelve a intentarlo.");
                                return;

                            case bool _ when await databaseContext.Accounts.AnyAsync(a => a.Mail == registerAccountComponent.Account.Mail):
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que tu correo electrónico ya se encuentra registrado; por favor, vuelve a intentarlo.");
                                return;
                        }

                        registerAccountLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Completar registro", $"{colors.GetHexadecimal("primaryWhite")}Estás a punto de registrar una nueva cuenta\n¿Deseas continuar?", "Continuar", "Cancelar");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        player.PlaySound(1085);

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            if (registerAccountComponent?.Account is null)
                            {
                                DestroyRegisterAccountComponents(player);
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que tu entidad no cuenta con los componentes necesarios para finalizar la creación de la Cuenta; por favor, vuelve a intentarlo.");
                                return;
                            }

                            PasswordHasher<object> hasher = new();
                            registerAccountComponent.Account.Password = hasher.HashPassword(null, registerAccountComponent.Account.Password!);

                            databaseContext.Accounts.Add(registerAccountComponent.Account);

                            if (await databaseContext.SaveChangesAsync() > 0)
                            {
                                player.AddComponent(new AccountInformation { Account = registerAccountComponent.Account });
                                charactersLayout.Create(player);

                                player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Tu cuenta fue creada con éxito.");
                            }
                            else
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Error interno al guardar la cuenta. Reintenta.");
                            }

                            DestroyRegisterAccountComponents(player);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            registerAccountLayout.Show(player);
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste la creación de tu Cuenta.");
                        }
                        break;
                    }
                case 17:
                    {
                        registerAccountLayout.Hide(player);
                        player.PlaySound(1085);

                        InputDialog inputDialog = new()
                        {
                            Caption = $"{colors.GetHexadecimal("primaryColor")}Iniciar sesión",
                            Content = $"{colors.GetHexadecimal("primaryWhite")}¡Ey! {colors.GetHexadecimal("primaryColor")}{player.Name}{colors.GetHexadecimal("primaryWhite")}, Parece que quieres Iniciar sesión en una cuenta diferente\nPor favor, ingresa el nombre de usuario de una cuenta existente continuación para continuar.",
                            Button1 = "Siguiente",
                            Button2 = "Atras"
                        };

                        InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                        if (inputDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        registerAccountLayout.Show(player);
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
                            else if (await databaseContext.Accounts.AnyAsync(a => a.Name == inputDialogResponse.InputText) is false)
                            {
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}El nombre de usuario que ingresaste no coincide con ninguna cuenta existente. Por favor vuelve a intentarlo.");
                                return;
                            }

                            DestroyRegisterAccountComponents(player);
                            player.Name = inputDialogResponse.InputText;
                            loginLayout.Create(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}El Nombre del Usuario se establecio correctamente para su Inicio de sesión.");
                        }
                        else if (inputDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite al registro de una nueva cuenta.");
                        }
                        break;
                    }
            }
        }
    }

    private static void DestroyRegisterAccountComponents(Player player)
    {
        player.DestroyComponents<RegisterAccountLayoutComponent>();
        player.DestroyComponents<RegisterAccountComponent>();
    }
}