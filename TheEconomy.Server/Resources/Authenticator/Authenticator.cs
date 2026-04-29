using System.Threading.Tasks;
using TheEconomy.Database;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.DeleteConversation.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator
{
    public class Authenticator(DatabaseContext databaseContext, IDeleteConversation deleteConversation, IVerifyUserName verifyUserName, IVerifyUserNameUI VerifyUserNameUI, /* IVerifyProhibition verifyProhibition, */ KnowledgeTest.KnowledgeTest knowledgeTest) : ISystem
    {
        [Event]
        public async Task OnPlayerConnect(Player player)
        {
            await Task.Delay(1700);
            deleteConversation.DeleteTheGlobalConversation();

            if (verifyUserName.Verify(player.Name) is false)
            {
                VerifyUserNameUI.CreatePlayerTextDrawings(player);
                VerifyUserNameUI.Show(player);
                
                await Task.Delay(1700);
                player.Kick();
                return;
            }

            bool resultKnowledgeTest = await knowledgeTest.Start(player);

            /* if (resultKnowledgeTest)
            {
                player.SendClientMessage($"¡Has aprobado el test de conocimiento! Bienvenido al servidor.");
            }
            else
            {
                player.SendClientMessage($"No has aprobado el test de conocimiento. No podrás acceder al servidor.");
            } */

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