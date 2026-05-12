using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database;
using TheEconomy.Database.Entity.Character;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Characters.Components;
using TheEconomy.Server.Resources.Authenticator.Characters.Interfaces;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Components;
using TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Interfaces;
using TheEconomy.Server.Resources.PlayerApparence.Interfaces;
using TheEconomy.Server.Resources.Authenticator.EditCharacter.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.Characters;

public class Characters(DatabaseContext databaseContext, IDialogService dialogService, ICorrectTextStrings correctTextStrings, IRegisterCharacterLayout registerCharacterLayout, ICharactersLayout charactersLayout, IColors colors, ISetSpawnParameters setSpawnParameters, IEditCharacterLayout editCharacterLayout) : ISystem
{
    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        CharactersLayoutComponent charactersLayoutComponent = player.GetComponent<CharactersLayoutComponent>();

        if (charactersLayoutComponent is not null && charactersLayoutComponent.IsComponentAlive && charactersLayoutComponent.PlayerTextDrawings is not null)
        {
            AccountComponent accountComponent = player.GetComponent<AccountComponent>();

            if (accountComponent?.Account?.Characters is null)
            {
                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que tu entidad no cuenta con los componentes necesarios para realizar esta acción; por favor, vuelve a intentarlo.");
                return;
            }

            List<CharacterEntity> charactersEntity = [.. accountComponent.Account.Characters];

            switch (charactersLayoutComponent.PlayerTextDrawings.IndexOf(playerTextDraw))
            {
                case 1:
                    {
                        charactersLayout.Hide(player);
                        player.PlaySound(1085);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Cancelar selección de personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas cancelar la selección de personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            DestroyCharactersComponents(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Cancelaste la selección de personaje.");
                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            charactersLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la selección de personaje.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
                case 5:
                    {
                        player.PlaySound(1085);

                        if (charactersEntity.Count > 0 && charactersEntity[0] is not null)
                        {
                            accountComponent.Account.SelectedCharacter = 0;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(charactersEntity[accountComponent.Account.SelectedCharacter].Name)}_{correctTextStrings.Correct(charactersEntity[accountComponent.Account.SelectedCharacter].LastName)}";
                            return;
                        }

                        charactersLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Crear nuevo personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas crear un nuevo personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            DestroyCharactersComponents(player);
                            registerCharacterLayout.Create(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Ahora estas creando un nuevo personaje.");
                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            charactersLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la selección de personaje.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
                case 7:
                    {
                        player.PlaySound(1085);

                        if (charactersEntity.Count > 1 && charactersEntity[1] is not null)
                        {
                            accountComponent.Account.SelectedCharacter = 1;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(charactersEntity[accountComponent.Account.SelectedCharacter].Name)}_{correctTextStrings.Correct(charactersEntity[accountComponent.Account.SelectedCharacter].LastName)}";
                            return;
                        }

                        charactersLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Crear nuevo personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas crear un nuevo personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            DestroyCharactersComponents(player);
                            registerCharacterLayout.Create(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Ahora estas creando un nuevo personaje.");
                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            charactersLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la selección de personaje.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
                case 9:
                    {
                        player.PlaySound(1085);

                        if (charactersEntity.Count > 2 && charactersEntity[2] is not null)
                        {
                            accountComponent.Account.SelectedCharacter = 2;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(charactersEntity[accountComponent.Account.SelectedCharacter].Name)}_{correctTextStrings.Correct(charactersEntity[accountComponent.Account.SelectedCharacter].LastName)}";
                            return;
                        }

                        charactersLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Crear nuevo personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas crear un nuevo personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            DestroyCharactersComponents(player);
                            registerCharacterLayout.Create(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Ahora estas creando un nuevo personaje.");
                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            charactersLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la selección de personaje.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
                case 11:
                    {
                        player.PlaySound(1085);

                        if (accountComponent?.Account?.SelectedCharacter == -1)
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes seleccionar un personaje primero para poder editarlo.");
                            return;
                        }

                        charactersLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Editar personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas editar este personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            DestroyCharactersComponents(player);
                            editCharacterLayout.Create(player, charactersEntity[accountComponent.Account.SelectedCharacter]);

                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            charactersLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la selección de personaje.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
                case 13:
                    {
                        player.PlaySound(1085);

                        if (accountComponent?.Account?.SelectedCharacter == -1)
                        {
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes seleccionar un personaje primero para poder empezar a jugar.");
                            return;
                        }

                        charactersLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Empezar a jugar", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas empezar a jugar con este personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            DestroyCharactersComponents(player);

                            charactersEntity.ForEach(c => c.Online = false);
                            charactersEntity[accountComponent.Account.SelectedCharacter].Online = true;
                            setSpawnParameters.Spawn(player, charactersEntity[accountComponent.Account.SelectedCharacter], true);

                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            charactersLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la selección de personaje.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
                case 15:
                    {
                        player.PlaySound(1085);

                        switch (true)
                        {
                            case bool _ when accountComponent?.Account?.SelectedCharacter == -1:
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Debes seleccionar un personaje primero para poder borrarlo; por favor, vuelve a intentarlo.");
                                return;
                            case bool _ when charactersEntity[accountComponent.Account.SelectedCharacter].Online == true:
                                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}No puedes eliminar un personaje que se encuentra jugando actualmente; por favor, vuelve a intentarlo.");
                                return;
                        }

                        charactersLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Eliminar personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas eliminar este personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            databaseContext.Characters.Remove(charactersEntity[accountComponent.Account.SelectedCharacter]);

                            charactersLayout.Destroy(player);
                            charactersLayout.Create(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Personaje eliminado correctamente.");
                            player.PlaySound(1085);
                        }
                        else if (messageDialogResponse.Response == DialogResponse.RightButtonOrCancel)
                        {
                            charactersLayout.Show(player);

                            player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Volvite a la selección de personaje.");
                            player.PlaySound(1085);
                        }
                        break;
                    }
            }
        }
    }

    private static void DestroyCharactersComponents(Player player)
    {
        player.DestroyComponents<CharactersLayoutComponent>();
    }
}