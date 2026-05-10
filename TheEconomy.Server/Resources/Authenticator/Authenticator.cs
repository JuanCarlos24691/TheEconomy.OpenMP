using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Database;
using TheEconomy.Database.Entity.Account;
using TheEconomy.Database.Entity.Prohibitions;
using TheEconomy.Server.Resources.Services.DeleteConversation.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Interfaces;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;
using TheEconomy.Server.Resources.KnowledgeTest.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Interfaces;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;

#nullable enable

namespace TheEconomy.Server.Resources.Authenticator;

public class Authenticator(DatabaseContext databaseContext, IDeleteConversation deleteConversation, IColors colors, IVerifyUserName verifyUserName, IVerifyUserNameLayout verifyUserNameLayout, IVerifyProhibition verifyProhibition, IVerifyProhibitionLayout verifyProhibitionLayout, IBlackBackgroundLayout blackBackgroundLayout, ILoginLayout loginLayout, IRegisterAccountLayout registerAccountLayout, IKnowledgeTest knowledgeTest) : ISystem
{
    [Event]
    public async Task OnPlayerConnect(Player player)
    {
        await Task.Delay(500);
        await Authenticate(player);
    }

    public async Task Authenticate(Player player)
    {
        deleteConversation.DeleteTheGlobalConversation();
        blackBackgroundLayout.Show(player);

        if (verifyUserName.Verify(player.Name) is false || player.Name.Length < 10 || player.Name.Length > 24)
        {
            verifyUserNameLayout.Create(player);
            verifyUserNameLayout.Show(player);
            return;
        }

        (ProhibitionEntity? prohibition, AccountEntity? account) = await verifyProhibition.Verify(player.Name, player.Ip);

        if (account is not null || prohibition is not null)
        {
            verifyProhibitionLayout.Create(player, prohibition, account);
            verifyProhibitionLayout.Show(player);
            return;
        }

        if (await databaseContext.Accounts.AnyAsync(a => a.Name == player.Name))
        {
            loginLayout.Create(player);
            loginLayout.Show(player);
        }
        else
        {
            if (await knowledgeTest.Start(player) is true)
            {
                registerAccountLayout.Create(player);
                registerAccountLayout.Show(player);

                player.SendClientMessage($"{colors.GetHexadecimal("primaryGreen")}Superaste el test de rol.");
            }
            else
            {
                _ = Authenticate(player);
                player.SendClientMessage($"{colors.GetHexadecimal("primaryRed")}No superaste el test de rol. Por favor, vuelve a intentarlo.");
            }
        }
    }
}