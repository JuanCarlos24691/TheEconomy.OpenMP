using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Components;
using TheEconomy.Server.Resources.DatabaseEntities.Account.Interfaces;

#nullable enable

namespace TheEconomy.Server.Resources.DatabaseEntities.Account;

public class SaveAccountRecord (IEntityManager entityManager, DatabaseContext databaseContext) : ISaveAccountRecord
{
    public async Task Save(Player? player = null)
    {
        Player[] players = player != null ? [player] : entityManager.GetComponents<Player>();

        foreach (Player p in players)
        {
            AccountComponent accountComponent = p.GetComponent<AccountComponent>();

            if (accountComponent == null || !accountComponent.IsComponentAlive || accountComponent?.Account is null || !accountComponent.IsLoggedIn)
                continue;

            databaseContext.Accounts.Update(accountComponent.Account);
        }

        await databaseContext.SaveChangesAsync();
    }
}