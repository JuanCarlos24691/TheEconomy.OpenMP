using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Interfaces;

namespace TheEconomy.Server.Resources.DatabaseEntities.Account;

public class Account(ISaveAccountRecord saveAccountRecord) : ISystem
{
    [Event]
    public async Task OnPlayerDisconnect(Player player)
    {
        await saveAccountRecord.Save(player);
    }

    [Timer(5000)]
    private async void TimerSaveAccountRecord() => await saveAccountRecord.Save();
}