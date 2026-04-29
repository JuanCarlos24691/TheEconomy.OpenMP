using System.Threading.Tasks;
using TheEconomy.Database;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.DeleteConversation.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using Microsoft.EntityFrameworkCore;
using TheEconomy.Server.Resources.Components.AccountInformation;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator;

public class Authenticator(DatabaseContext databaseContext, IColors colors, IDeleteConversation deleteConversation, IVerifyUserName verifyUserName, IVerifyUserNameView verifyUserNameView, IVerifyProhibition verifyProhibition, IVerifyProhibitionView verifyProhibitionView, KnowledgeTest.KnowledgeTest knowledgeTest) : ISystem
{
    [Event]
    public async Task OnPlayerConnect(Player player)
    {
        await Task.Delay(1700);
        deleteConversation.DeleteTheGlobalConversation();

        if (verifyUserName.Verify(player.Name) is false)
        {
            verifyUserNameView.CreatePlayerTextDrawings(player);
            verifyUserNameView.Show(player);

            await Task.Delay(1700);
            player.Kick();
            return;
        }

        AccountInformation accountInformation = await verifyProhibition.Verify(player.Name, player.Ip);

        if (accountInformation is not null)
        {
            verifyProhibitionView.CreatePlayerTextDrawings(player, accountInformation);
            verifyProhibitionView.Show(player);

            await Task.Delay(1700);
            player.Kick();
            return;
        }

        accountInformation = player.GetComponent<AccountInformation>() ?? player.AddComponent<AccountInformation>();
        accountInformation.Account = await databaseContext.Accounts.FirstOrDefaultAsync(a => EF.Functions.Like(a.Name, player.Name));

        if (accountInformation.Account is not null)
        {
            player.SendClientMessage($"Ya estas registrado.");
        }
        else if (await knowledgeTest.Start(player) is false)
        {
            player.SendClientMessage($"No has aprobado el test de conocimiento. No podrás acceder al servidor.");

            await Task.Delay(1700);
            player.Kick();
        }
    }
}