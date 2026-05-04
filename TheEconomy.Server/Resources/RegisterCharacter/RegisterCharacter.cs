using System.Text;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.RegisterAccount.Interfaces;
using TheEconomy.Server.Resources.RegisterAccount.Components;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyMail.Interfaces;
using TheEconomy.Database;
using TheEconomy.Server.Resources.RegisterCharacter.Interfaces;
using TheEconomy.Server.Resources.RegisterCharacter.Components;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyDate.Interfaces;
using System;

namespace TheEconomy.Server.Resources.RegisterAccount;

public class RegisterCharacter(DatabaseContext databaseContext, IDialogService dialogService, IVerifyMail verifyMail, IVerifyDate verifyDate, IVerifyUserName verifyUserName, ICorrectTextStrings correctTextStrings, IColors colors, IRegisterCharacterLayout registerCharacterLayout) : ISystem, IRegisterAccount
{
    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        RegisterCharacterLayoutComponent registerCharacterLayoutComponent = registerCharacterLayout.GetRegisterCharacterLayoutComponent(player);

        if (registerCharacterLayoutComponent.IsComponentAlive)
        {
            RegisterCharacterComponent registerCharacterComponent = player.GetComponent<RegisterCharacterComponent>() ?? player.AddComponent<RegisterCharacterComponent>();

            if (playerTextDraw == registerCharacterLayoutComponent.PlayerTextDrawings[1])
            {
                registerCharacterLayout.Hide(player);
                player.PlaySound(1085);

                MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryRed")}Cancelar registro de Personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente desees cancelar el registro de un nuev Personaje?", "Sí", "No");
                MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                if (messageDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                if (messageDialogResponse.Response == DialogResponse.LeftButton)
                {
                    registerCharacterLayout.Destroy(player);

                    player.AddComponent<RegisterCharacterComponent>()?.Destroy();
                    registerCharacterLayoutComponent.Destroy();

                    player.PlaySound(1085);
                }
                else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                {
                    registerCharacterLayout.Show(player);
                    player.PlaySound(1085);
                }
            }
            else if (playerTextDraw == registerCharacterLayoutComponent.PlayerTextDrawings[6])
            {
                registerCharacterLayout.Hide(player);
                player.PlaySound(1085);

                InputDialog inputDialog = new()
                {
                    Caption = $"{colors.GetHexadecimal("primaryColor")}Nombre del Personaje",
                    Content = $"{colors.GetHexadecimal("primaryWhite")}La longitud de caracteres permitidos son de {colors.GetHexadecimal("primaryRed")}4{colors.GetHexadecimal("primaryWhite")} a {colors.GetHexadecimal("primaryGreen")}10{colors.GetHexadecimal("primaryWhite")} caracteres\n¿Como quieres nombrar a tu personaje?",
                    Button1 = "Siguiente",
                    Button2 = "Atras"
                };

                InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                if (inputDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                registerCharacterLayout.Show(player);
                player.PlaySound(1085);

                if (inputDialogResponse.Response == DialogResponse.LeftButton)
                {
                    if (string.IsNullOrEmpty(inputDialogResponse.InputText) || !verifyUserName.Verify(inputDialogResponse.InputText))
                    {
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes ingresar un nombre válido para tu personaje. Por favor vuelve a intentarlo");
                        return;
                    }
                    else if (inputDialogResponse.InputText.Length < 4 || inputDialogResponse.InputText.Length > 10)
                    {
                        registerCharacterLayout.Show(player);
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}La longitud de caracteres permitidos son de 4 a 10 caracteres. Por favor vuelve a intentarlo.");
                        return;
                    }

                    registerCharacterComponent.Character.Name = inputDialogResponse.InputText;

                    registerCharacterLayoutComponent.PlayerTextDrawings[7].Text = registerCharacterComponent.Character.Name;
                    player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}El nombre del personaje se establecio correctamente.");
                }
                else if (inputDialogResponse.Response == DialogResponse.LeftButton)
                {
                    player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste la asignación del nombre de tu personaje.");
                }
            }
            else if (playerTextDraw == registerCharacterLayoutComponent.PlayerTextDrawings[8])
            {
                registerCharacterLayout.Hide(player);
                player.PlaySound(1085);

                InputDialog inputDialog = new()
                {
                    Caption = $"{colors.GetHexadecimal("primaryColor")}Apellido del Personaje",
                    Content = $"{colors.GetHexadecimal("primaryWhite")}La longitud de caracteres permitidos son de {colors.GetHexadecimal("primaryRed")}4{colors.GetHexadecimal("primaryWhite")} a {colors.GetHexadecimal("primaryGreen")}10{colors.GetHexadecimal("primaryWhite")} caracteres\n¿Que apellido quieres para tu personaje?",
                    Button1 = "Siguiente",
                    Button2 = "Atras"
                };

                InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                if (inputDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                registerCharacterLayout.Show(player);
                player.PlaySound(1085);

                if (inputDialogResponse.Response == DialogResponse.LeftButton)
                {
                    if (string.IsNullOrEmpty(inputDialogResponse.InputText) || !verifyUserName.Verify(inputDialogResponse.InputText))
                    {
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes ingresar un nombre válido para tu personaje. Por favor vuelve a intentarlo");
                        return;
                    }
                    else if (inputDialogResponse.InputText.Length < 4 || inputDialogResponse.InputText.Length > 10)
                    {
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}La longitud de caracteres permitidos son de 4 a 10 caracteres. Por favor vuelve a intentarlo.");
                        return;
                    }

                    registerCharacterComponent.Character.LastName = inputDialogResponse.InputText;

                    registerCharacterLayoutComponent.PlayerTextDrawings[9].Text = registerCharacterComponent.Character.LastName;
                    player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}El apellido del personaje se establecio correctamente.");
                }
                else if (inputDialogResponse.Response == DialogResponse.LeftButton)
                {
                    player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste la asignación del apellido de tu personaje.");
                }
            }
            else if (playerTextDraw == registerCharacterLayoutComponent.PlayerTextDrawings[10])
            {
                registerCharacterLayout.Hide(player);
                player.PlaySound(1085);

                MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryRed")}Género del Personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Que genero deseas que tenga tu personaje?", "Hombre", "Mujer");
                MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                if (messageDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                registerCharacterLayout.Show(player);
                player.PlaySound(1085);

                if (messageDialogResponse.Response == DialogResponse.LeftButton)
                {
                    registerCharacterComponent.Character.Gender = 0;
                    registerCharacterLayoutComponent.PlayerTextDrawings[12].Text = "Hombre";
                }
                else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                {
                    registerCharacterComponent.Character.Gender = 1;
                    registerCharacterLayoutComponent.PlayerTextDrawings[12].Text = "Mujer";
                }

                player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}El género del personaje se establecio correctamente.");
            }
            else if (playerTextDraw == registerCharacterLayoutComponent.PlayerTextDrawings[13])
            {
                registerCharacterLayout.Hide(player);
                player.PlaySound(1085);

                InputDialog inputDialog = new()
                {
                    Caption = $"{colors.GetHexadecimal("primaryColor")}Fecha de nacimiento del Personaje",
                    Content = $"{colors.GetHexadecimal("primaryWhite")}Debes usar un formato de año/mes/día. Por ejemplo: {colors.GetHexadecimal("primaryGreen")}02/02/2002\n¿Cuando nacio tu personaje?",
                    Button1 = "Siguiente",
                    Button2 = "Atras"
                };

                InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                if (inputDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                registerCharacterLayout.Show(player);
                player.PlaySound(1085);

                if (inputDialogResponse.Response == DialogResponse.LeftButton)
                {
                    if (string.IsNullOrEmpty(inputDialogResponse.InputText) || !verifyDate.ObtainVerification(inputDialogResponse.InputText) || !DateTime.TryParse(inputDialogResponse.InputText, out _))
                    {
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryColor")}La fecha de nacimiento de tu personaje no es válida, el formato correcto es día/mes/año. Por favor vuelve a intentarlo");
                        return;
                    }

                    registerCharacterComponent.Character.BirthDate = DateTime.Parse(inputDialogResponse.InputText);

                    registerCharacterLayoutComponent.PlayerTextDrawings[12].Text = registerCharacterComponent.Character.BirthDate.ToString("dd/MM/yyyy");
                    player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}La fecha de nacimiento del personaje se establecio correctamente.");
                }
                else if (inputDialogResponse.Response == DialogResponse.LeftButton)
                {
                    player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste la asignación de la fecha de nacimiento de tu personaje.");
                }
            }

            /* else if (playerTextDraw == registerCharacterLayoutComponent.PlayerTextDrawings[7])
            {
                registerCharacterLayout.Hide(player);
                player.PlaySound(1085);

                RegisterAccountComponent registerAccountComponent = player.GetComponent<RegisterAccountComponent>() ?? player.AddComponent<RegisterAccountComponent>();

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
                    Caption = "Ingresar contraseña",
                    Content = stringBuilder.ToString(),
                    Button1 = "Siguiente",
                    Button2 = "Atras"
                };

                InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                if (inputDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                registerAccountLayout.Show(player);

                if (inputDialogResponse.Response == DialogResponse.LeftButton)
                {
                    if (string.IsNullOrEmpty(inputDialogResponse.InputText) && inputDialogResponse.InputText.Length < 8 && inputDialogResponse.InputText.Length > 32)
                    {
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes ingresar una contraseña válida antes de continuar.");
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Recuerda que estas deben tener una longitud entre 8 y 32 caracteres. Por favor vuelve a intentarlo.");
                        return;
                    }

                    registerAccountComponent.Account.Password = inputDialogResponse.InputText;
                    player.PlaySound(1085);
                }
                else if (inputDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                {
                    player.PlaySound(1085);
                }
            }
            else if (playerTextDraw == registerAccountLayoutComponent.PlayerTextDrawings[10])
            {
                RegisterAccountComponent registerAccountComponent = player.GetComponent<RegisterAccountComponent>() ?? player.AddComponent<RegisterAccountComponent>();

                if (string.IsNullOrEmpty(registerAccountComponent.Account.Password))
                {
                    player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}No has ingresado una contraseña para tu cuenta. Por favor vuelve a intentarlo.");
                    return;
                }

                if (registerAccountComponent.ShowPassword)
                {
                    registerAccountLayoutComponent.PlayerTextDrawings[9].Text = new string('.', registerAccountComponent.Account.Password.Length);
                    registerAccountComponent.ShowPassword = false;
                }
                else
                {
                    registerAccountLayoutComponent.PlayerTextDrawings[9].Text = correctTextStrings.ObtainCorrection(registerAccountComponent.Account.Password);
                    registerAccountComponent.ShowPassword = true;
                }
            }
            else if (playerTextDraw == registerAccountLayoutComponent.PlayerTextDrawings[12])
            {
                registerAccountLayout.Hide(player);
                player.PlaySound(1085);

                RegisterAccountComponent registerAccountComponent = player.GetComponent<RegisterAccountComponent>() ?? player.AddComponent<RegisterAccountComponent>();

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
                    Caption = "Ingresar Mail",
                    Content = stringBuilder.ToString(),
                    Button1 = "Siguiente",
                    Button2 = "Atras"
                };

                InputDialogResponse inputDialogResponse = await dialogService.ShowAsync(player, inputDialog);

                if (inputDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                registerAccountLayout.Show(player);

                if (inputDialogResponse.Response == DialogResponse.LeftButton)
                {
                    if (string.IsNullOrEmpty(inputDialogResponse.InputText) && inputDialogResponse.InputText.Length < 4 && inputDialogResponse.InputText.Length > 319)
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
                    player.PlaySound(1085);
                }
                else if (inputDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                {
                    player.PlaySound(1085);
                }
            }
            else if (playerTextDraw == registerAccountLayoutComponent.PlayerTextDrawings[15])
            {
                player.PlaySound(1085);

                RegisterAccountComponent registerAccountComponent = player.GetComponent<RegisterAccountComponent>() ?? player.AddComponent<RegisterAccountComponent>();

                switch (true)
                {
                    case bool _ when string.IsNullOrEmpty(registerAccountComponent.Account.Name):
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no tienes asignado un nombre de usuario para registrar tu cuenta; por favor, vuelve a intentarlo.");
                        return;

                    case bool _ when string.IsNullOrEmpty(registerAccountComponent.Account.Password):
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no tienes asignado una contraseña para registrar tu cuenta; por favor, vuelve a intentarlo.");
                        return;

                    case bool _ when string.IsNullOrEmpty(registerAccountComponent.Account.Mail):
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no tienes asignado un correo electronico para registrar tu cuenta; por favor, vuelve a intentarlo.");
                        return;
                }

                registerAccountLayout.Hide(player);

                MessageDialog messageDialog = new($"Completar registro", $"{colors.GetHexadecimal("primaryRed")}Estás a punto de registrar una nueva cuenta\n¿Deseas continuar?", "Continuar", "Cancelar");
                MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                if (messageDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                if (messageDialogResponse.Response == DialogResponse.LeftButton)
                {
                    databaseContext.Accounts.Add(registerAccountComponent.Account);

                    if (await databaseContext.SaveChangesAsync() == 0)
                    {
                        registerAccountLayout.Show(player);
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no se pudo crear tu cuenta; por favor, vuelve a intentarlo.");
                        return;
                    }

                    player.DestroyComponents<RegisterAccountLayoutComponent>();
                    player.DestroyComponents<RegisterAccountComponent>();

                    player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Tu cuenta fue creada con éxito.");
                    player.PlaySound(1085);
                }
                else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                {
                    registerAccountLayout.Show(player);
                    player.PlaySound(1085);
                }
            } */
        }
    }
}