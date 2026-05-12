using System;
using System.Linq;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database.Entity.Character;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Components;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Interfaces;

namespace TheEconomy.Server.Resources.DatabaseEntities.Account;

public class Account(ISaveAccountRecord saveAccountRecord) : ISystem
{
    [Event]
    public async Task OnPlayerDisconnect(Player player, DisconnectReason reason)

    {
        AccountComponent accountComponent = player.GetComponent<AccountComponent>();

        if (accountComponent == null || !accountComponent.IsComponentAlive || accountComponent?.Account is null || !accountComponent.IsLoggedIn)
            return;

        CharacterEntity character = accountComponent.Account.Characters.ElementAt(accountComponent.Account.SelectedCharacter);

        character.LastConnection = DateTime.UtcNow;
        character.Online = false;
        character.SpawnX = player.Position.X;
        character.SpawnY = player.Position.Y;
        character.SpawnZ = player.Position.Z;
        character.Angle = player.Angle;

        accountComponent.Account.Characters.ToList().ForEach(c => c.Online = false);
        accountComponent.Account.SelectedCharacter = -1;

        await saveAccountRecord.Save(player);
    }

    [Timer(2500)]
    private async void TimerSaveAccountRecord() => await saveAccountRecord.Save();
}