using System;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Authenticator.EditCharacter.Interfaces;
using TheEconomy.Server.Resources.Authenticator.EditCharacter.Components;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyDate.Interfaces;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Components;
using TheEconomy.Server.Resources.Authenticator.Characters.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.EditCharacter;

public class EditCharacter(DatabaseContext databaseContext, IDialogService dialogService, IVerifyDate verifyDate, IVerifyUserName verifyUserName, ICorrectTextStrings correctTextStrings, IColors colors, ICharactersLayout charactersLayout, IEditCharacterLayout editCharacterLayout) : ISystem
{
    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        EditCharacterLayoutComponent editCharacterLayoutComponent = player.GetComponent<EditCharacterLayoutComponent>();

        if (editCharacterLayoutComponent is not null && editCharacterLayoutComponent.IsComponentAlive && editCharacterLayoutComponent.PlayerTextDrawings is not null)
        {
            EditCharacterComponent editCharacterComponent = player.GetComponent<EditCharacterComponent>() ?? player.AddComponent<EditCharacterComponent>();

            switch (editCharacterLayoutComponent.PlayerTextDrawings.IndexOf(playerTextDraw))
            {
                case 1:
                case 30:
                    {
                        editCharacterLayout.Hide(player);
                        player.PlaySound(1085);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryRed")}Cancelar edición del personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas cancelar la edición del personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            DestroyEditCharacterComponents(player);
                            charactersLayout.Create(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste la edición del personaje.");
                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            editCharacterLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la edición del personaje.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
            }
        }
    }

    private static void DestroyEditCharacterComponents(Player player)
    {
        player.DestroyComponents<EditCharacterLayoutComponent>();
        player.DestroyComponents<EditCharacterComponent>();
    }
}