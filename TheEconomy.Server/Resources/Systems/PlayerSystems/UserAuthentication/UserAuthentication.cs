using System.Threading.Tasks;
using TheEconomy.Database;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.DeleteConversation.Interfaces;

namespace TheEconomy.Server.Resources.Systems.PlayerSystems.UserAuthentication
{
    public class UserAuthentication(DatabaseContext databaseContext, IDeleteConversation deleteConversation, VerifyUserName verifyUserName, VerifyProhibition verifyProhibition, KnowledgeTest.KnowledgeTest knowledgeTest) : ISystem
    {
        [Event]
        public async Task OnPlayerConnect(Player player)
        {
            await Task.Delay(1);
            bool resultado = await knowledgeTest.Start(player);

            if (resultado)
            {
                player.SendClientMessage($"¡Has aprobado el test de conocimiento! Bienvenido al servidor.");
            }
            else
            {
                player.SendClientMessage($"No has aprobado el test de conocimiento. No podrás acceder al servidor.");
            }

            /* deleteConversation.DeleteTheGlobalConversation();

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
            } */
        }
    }
}