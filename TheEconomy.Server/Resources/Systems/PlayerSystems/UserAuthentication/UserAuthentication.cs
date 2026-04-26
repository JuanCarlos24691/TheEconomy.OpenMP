using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheEconomy.Database;
using TheEconomy.Database.Entity.Account;
using TheEconomy.Database.Entity.Prohibitions;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Components;
using TheEconomy.Server.Resources.Services;

namespace TheEconomy.Server.Resources.Systems.PlayerSystems.UserAuthentication
{
    public class UserAuthentication(DatabaseContext databaseContext, DeleteConversation deleteConversation, VerifyUserName verifyUserName, VerifyProhibition verifyProhibition, KnowledgeTest knowledgeTest) : ISystem
    {
        [Event]
        public async Task OnPlayerConnect(Player player)
        {
            await Task.Delay(1);
            deleteConversation.DeleteTheGlobalConversation();

            if (verifyUserName.ObtainVerification(player) is true)
            {
                Task<Prohibition> prohibition = databaseContext.Prohibitions.FirstOrDefaultAsync(p => p.IP == player.Ip);
                Task<Account> account = databaseContext.Accounts.FirstOrDefaultAsync(a => EF.Functions.Like(a.Name, player.Name));
                await Task.WhenAll(prohibition, account);

                if (player.GetComponent<PlayerData>() is null)
                    player.AddComponent<PlayerData>();

                PlayerData playerData = player.GetComponent<PlayerData>();

                playerData.Prohibition = prohibition.Result;
                playerData.Account = account.Result;

                if (verifyProhibition.ObtainVerification(player) is false)
                {

                    if (playerData.Account is not null)
                    {
                    }
                    else
                        _ = knowledgeTest.StartRoleTest(player);
                }
            }
        }
    }
}