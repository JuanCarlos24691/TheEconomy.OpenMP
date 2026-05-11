using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using TheEconomy.Database;
using TheEconomy.Database.Entity.Character;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Characters.Components;
using TheEconomy.Server.Resources.Authenticator.Characters.Interfaces;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Components;
using TheEconomy.Server.Resources.Authenticator.RegisterCharacter.Interfaces;
using TheEconomy.Server.Resources.PlayerApparence.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator.Characters;

public class Characters(IWorldService worldService, DatabaseContext databaseContext, IDialogService dialogService, ICorrectTextStrings correctTextStrings, IRegisterCharacterLayout registerCharacterLayout, ICharactersLayout charactersLayout, IColors colors, ISetSpawnParameters setSpawnParameters) : ISystem
{
    [PlayerCommand("kill")]
    public void KillCommand(Player player)
    {
        player.Health = 0f;
    }

    [PlayerCommand("nrg")]
    public void OnNrgCommand(Player player)
    {
        var pos = player.Position;
        var angle = player.Angle;

        // Calcular posición frente al jugador (2 unidades adelante)
        var spawnX = pos.X + (float)(Math.Sin(angle * Math.PI / 180.0) * 2);
        var spawnY = pos.Y + (float)(Math.Cos(angle * Math.PI / 180.0) * 2);
        var spawnZ = pos.Z;

        // Crear la NRG-500 (Model ID: 522) usando IWorldService
        var vehicle = worldService.CreateVehicle(
            VehicleModelType.NRG500,  // 522
            position: new Vector3(spawnX, spawnY, spawnZ),
            rotation: angle,
            color1: -1,          // color aleatorio
            color2: -1,
            respawnDelay: 60     // segundos para auto-despawnear si está vacío
        );

        // Meter al jugador en el asiento del conductor
        player.PutInVehicle(vehicle, 0);

        player.SendClientMessage(Color.GreenYellow, "✔ NRG-500 spawneada!");
    }

    [PlayerCommand("test")]
    public void OnTestAccountCommand(Player player)
    {
        var accountComponent = player.GetComponent<AccountComponent>();

        if (accountComponent == null || !accountComponent.IsLoggedIn || accountComponent.Account == null)
        {
            player.SendClientMessage(Color.Red, "No tienes una cuenta activa.");
            return;
        }

        // Cambiar el nivel administrativo como prueba
        accountComponent.Account.AdministrativeLevel++;

        player.SendClientMessage(Color.GreenYellow, $"Nivel admin cambiado a: {accountComponent.Account.AdministrativeLevel} (se guardará en 1s)");
    }

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

            List<CharacterEntity> characters = [.. accountComponent.Account.Characters];

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
                            accountComponent.Account.SelectedCharacter = 0;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(characters[accountComponent.Account.SelectedCharacter].Name)}_{correctTextStrings.Correct(characters[accountComponent.Account.SelectedCharacter].LastName)}";
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
                            accountComponent.Account.SelectedCharacter = 1;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(characters[accountComponent.Account.SelectedCharacter].Name)}_{correctTextStrings.Correct(characters[accountComponent.Account.SelectedCharacter].LastName)}";
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
                            accountComponent.Account.SelectedCharacter = 2;
                            charactersLayoutComponent.PlayerTextDrawings[12].Text = $"{correctTextStrings.Correct(characters[accountComponent.Account.SelectedCharacter].Name)}_{correctTextStrings.Correct(characters[accountComponent.Account.SelectedCharacter].LastName)}";
                            return;
                        }

                        charactersLayout.Hide(player);

                        MessageDialog messageDialog = new($"{colors.GetHexadecimal("primaryColor")}Crear nuevo personaje", $"{colors.GetHexadecimal("primaryWhite")}¿Realmente deseas crear un nuevo personaje?", "Sí", "No");
                        MessageDialogResponse messageDialogResponse = await dialogService.ShowAsync(player, messageDialog);

                        if (messageDialogResponse.Response == DialogResponse.Disconnected)
                            return;

                        if (messageDialogResponse.Response == DialogResponse.LeftButton)
                        {
                            characters.ForEach(c => c.Online = false);
                            characters[accountComponent.Account.SelectedCharacter].Online = true;

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

                        if (accountComponent?.Account?.SelectedCharacter is null)
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
                            setSpawnParameters.Spawn(player, characters[accountComponent.Account.SelectedCharacter], true);

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