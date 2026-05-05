using System;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Components;
using TheEconomy.Server.Resources.Authenticator.Login.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.Login;

public class Login(DatabaseContext databaseContext, IDialogService dialogService, ICorrectTextStrings correctTextStrings, IColors colors, ILoginLayout loginLayout) : ISystem
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
                            player.GetComponent<LoginComponent>()?.Destroy();
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
            }
        }
    }
}