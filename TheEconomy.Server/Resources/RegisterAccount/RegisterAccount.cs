using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.RegisterAccount.Interfaces;
using TheEconomy.Server.Resources.RegisterAccount.Components;
using System.Threading.Tasks;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;

namespace TheEconomy.Server.Resources.RegisterAccount;

public class RegisterAccount(IWorldService worldService, IDialogService dialogService, IServerInformation serverInformation, ICorrectTextStrings correctTextStrings, IColors colors, IBlackBackgroundLayout blackBackgroundLayout, IRegisterAccountLayout registerAccountLayout) : ISystem, IRegisterAccount
{
    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        RegisterAccountLayoutComponent registerAccountComponent = registerAccountLayout.GetRegisterAccountLayoutComponent(player);

        if (registerAccountComponent.IsComponentAlive)
        {
            if (playerTextDraw == registerAccountComponent.PlayerTextDrawings[1])
            {
                registerAccountLayout.Hide(player);
                player.PlaySound(1085);

                MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryRed")}Cancelar registro de cuenta", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente desees cancelar el registro de una nueva cuenta?", "Sí", "No");
                MessageDialogResponse response = await dialogService.ShowAsync(player, messageDialog);

                if (response.Response == DialogResponse.LeftButton)
                {
                    blackBackgroundLayout.Hide(player);

                    registerAccountLayout.Destroy(player);
                    registerAccountComponent.Destroy();

                    player.PlaySound(1085);
                }
                else if (response.Response == DialogResponse.RightButtonOrCancel || response.Response == DialogResponse.Disconnected)
                {
                    registerAccountLayout.Show(player);
                    player.PlaySound(1058);
                }
            }
        }
    }
}

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
