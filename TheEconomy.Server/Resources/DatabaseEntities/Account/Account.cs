using System;
using System.Linq;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Components;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Interfaces;

namespace TheEconomy.Server.Resources.DatabaseEntities.Account;

public class Account(ISaveAccountRecord saveAccountRecord) : ISystem
{
    [Event]
    public async Task OnPlayerDisconnect(Player player)
    {
        AccountComponent accountComponent = player.GetComponent<AccountComponent>();

        if (accountComponent == null || !accountComponent.IsComponentAlive || accountComponent?.Account is null || !accountComponent.IsLoggedIn)
            return;

        accountComponent.Account.Characters.ElementAt(accountComponent.Account.SelectedCharacter).LastConnection = DateTime.UtcNow;
        accountComponent.Account.Characters.ToList().ForEach(c => c.Online = false);
        accountComponent.Account.SelectedCharacter = -1;

        await saveAccountRecord.Save(player);
    }

    [Timer(2500)]
    private async void TimerSaveAccountRecord() => await saveAccountRecord.Save();
}