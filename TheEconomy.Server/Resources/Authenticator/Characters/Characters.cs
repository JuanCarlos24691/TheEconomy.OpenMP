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
using TheEconomy.Server.Resources.Components.AccountInformation;
using TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Interfaces;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;
using SampSharp.Entities.SAMP.Commands;

namespace TheEconomy.Server.Resources.Authenticator.Characters;

public class Characters(DatabaseContext databaseContext, IDialogService dialogService, ICorrectTextStrings correctTextStrings, IRegisterCharacterLayout registerCharacterLayout, ICharactersLayout charactersLayout, IColors colors, IBlackBackgroundLayout blackBackgroundLayout) : ISystem
{
    [Event]
    public static bool OnPlayerRequestClass(Player player, int classId)
    {
        return false; // aquí sí puedes bloquear comportamiento del selector
    }

    [PlayerCommand("kill")]
    public void KillCommand(Player player)
    {
        player.Health = 0f;
    }

    [Event]
    public async Task OnPlayerClickPlayerTextDraw(Player player, PlayerTextDraw playerTextDraw)
    {
        CharactersLayoutComponent charactersLayoutComponent = player.GetComponent<CharactersLayoutComponent>();

        if (charactersLayoutComponent is not null && charactersLayoutComponent.IsComponentAlive && charactersLayoutComponent.PlayerTextDrawings is not null)
        {
            AccountInformation accountInformation = player.GetComponent<AccountInformation>();

            if (accountInformation?.Account?.Characters is null)
            {
                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}Parece que tu entidad no cuenta con los componentes necesarios para realizar esta acción; por favor, vuelve a intentarlo.");
                return;
            }

            List<CharacterEntity> characters = [.. accountInformation.Account.Characters];
            CharactersComponent charactersComponent = player.GetComponent<CharactersComponent>() ?? player.AddComponent<CharactersComponent>();

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

                        if (characters.Count > 0 && characters[0] is not null)
                        {
                            charactersComponent.Index = 0;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(characters[charactersComponent.Index].Name)}_{correctTextStrings.Correct(characters[charactersComponent.Index].LastName)}";
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

                        if (characters.Count > 1 && characters[1] is not null)
                        {
                            charactersComponent.Index = 1;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(characters[charactersComponent.Index].Name)}_{correctTextStrings.Correct(characters[charactersComponent.Index].LastName)}";
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

                        if (characters.Count > 2 && characters[2] is not null)
                        {
                            charactersComponent.Index = 2;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(characters[charactersComponent.Index].Name)}_{correctTextStrings.Correct(characters[charactersComponent.Index].LastName)}";
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
                case 13:
                    {
                        player.PlaySound(1085);

                        if (charactersComponent.Index == -1)
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
                            player.SendClientMessage($"{colors.GetHexadecimal("primaryWhite")}¡Enhorabuena! Empezaste a jugar como {colors.GetHexadecimal("primaryGreen")}{characters[charactersComponent.Index].Name} {colors.GetHexadecimal("primaryYellow")}{characters[charactersComponent.Index].LastName}.");
                            player.PlaySound(1085);

                            if (player.IsSelectingTextDraw is true)
                                player.CancelSelectTextDraw();

                            blackBackgroundLayout.Hide(player);

                            player.SetSpawnInfo(0, characters[charactersComponent.Index].Appearance, new Vector3(characters[charactersComponent.Index].SpawnX, characters[charactersComponent.Index].SpawnY, characters[charactersComponent.Index].SpawnZ), characters[charactersComponent.Index].Angle, Weapon.None, 0, Weapon.None, 0, Weapon.None, 0);
                            player.Spawn();

                            DestroyCharactersComponents(player);
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