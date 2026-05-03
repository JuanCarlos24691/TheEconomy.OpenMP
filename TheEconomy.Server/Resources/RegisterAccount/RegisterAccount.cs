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

namespace TheEconomy.Server.Resources.RegisterAccount;

public class RegisterAccount(DatabaseContext databaseContext, IDialogService dialogService, IVerifyMail verifyMail, ICorrectTextStrings correctTextStrings, IColors colors, IBlackBackgroundLayout blackBackgroundLayout, IRegisterAccountLayout registerAccountLayout) : ISystem, IRegisterAccount
{
    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        RegisterAccountLayoutComponent registerAccountLayoutComponent = registerAccountLayout.GetRegisterAccountLayoutComponent(player);

        if (registerAccountLayoutComponent.IsComponentAlive)
        {
            if (playerTextDraw == registerAccountLayoutComponent.PlayerTextDrawings[1])
            {
                registerAccountLayout.Hide(player);
                player.PlaySound(1085);

                MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryRed")}Cancelar registro de cuenta", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente desees cancelar el registro de una nueva cuenta?", "Sí", "No");
                MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                if (messageDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                if (messageDialogResponse.Response == DialogResponse.LeftButton)
                {
                    blackBackgroundLayout.Hide(player);

                    registerAccountLayout.Destroy(player);
                    registerAccountLayoutComponent.Destroy();

                    player.PlaySound(1085);
                }
                else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                {
                    registerAccountLayout.Show(player);
                    player.PlaySound(1058);
                }
            }
            else if (playerTextDraw == registerAccountLayoutComponent.PlayerTextDrawings[7])
            {
                registerAccountLayout.Hide(player);
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
                    player.PlaySound(1058);
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
                    player.PlaySound(1058);
                }
            }
            else if (playerTextDraw == registerAccountLayoutComponent.PlayerTextDrawings[15])
            {
                registerAccountLayout.Hide(player);
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

                MessageDialog messageDialog = new($"Completar registro", $"{colors.GetHexadecimal("primaryRed")}Estás a punto de registrar una nueva cuenta\n¿Deseas continuar?", "Continuar", "Cancelar");
                MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                if (messageDialogResponse.Response == DialogResponse.Disconnected)
                    return;

                if (messageDialogResponse.Response == DialogResponse.LeftButton)
                {
                    databaseContext.Accounts.Add(registerAccountComponent.Account);

                    if (await databaseContext.SaveChangesAsync() == 0)
                    {
                        player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que no se pudo crear tu cuenta; por favor, vuelve a intentarlo.");
                        player.Kick();
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
                    player.PlaySound(1058);
                }
            }
        }
    }
}