using System.Threading.Tasks;
using TheEconomy.Database;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.DeleteConversation.Interfaces;
using TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces;
using Microsoft.EntityFrameworkCore;
using TheEconomy.Server.Resources.Components.AccountInformation;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Interfaces;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;
using TheEconomy.Server.Resources.Authenticator.RegisterAccount.Components;
using TheEconomy.Server.Resources.KnowledgeTest.Interfaces;
using TheEconomy.Server.Resources.Authenticator.Login.Interfaces;

namespace TheEconomy.Server.Resources.Authenticator;

public class Authenticator(DatabaseContext databaseContext, IDeleteConversation deleteConversation, IVerifyUserName verifyUserName, IVerifyUserNameLayout verifyUserNameLayout, IVerifyProhibition verifyProhibition, IVerifyProhibitionLayout verifyProhibitionLayout, IBlackBackgroundLayout blackBackgroundLayout, ILoginLayout loginLayout, IRegisterAccountLayout registerAccountLayout, IKnowledgeTest knowledgeTest) : ISystem
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

        if (verifyUserName.Verify(player.Name) is false)
        {
            verifyUserNameLayout.Create(player);
            verifyUserNameLayout.Show(player);
            return;
        }

        AccountInformation accountInformation = await verifyProhibition.Verify(player.Name, player.Ip);

        if (accountInformation.Account is not null || accountInformation.Prohibition is not null)
        {
            verifyProhibitionLayout.Create(player, accountInformation);
            verifyProhibitionLayout.Show(player);
            return;
        }

        accountInformation = player.GetComponent<AccountInformation>() ?? player.AddComponent<AccountInformation>();
        accountInformation.Account = await databaseContext.Accounts.FirstOrDefaultAsync(a => a.Name == player.Name);

        if (accountInformation.IsComponentAlive && accountInformation.Account is not null)
        {
            loginLayout.Create(player);
            loginLayout.Show(player);
        }
        else
        {
            if (await knowledgeTest.Start(player) is true)
            {
                RegisterAccountComponent registerAccountComponent = player.GetComponent<RegisterAccountComponent>() ?? player.AddComponent<RegisterAccountComponent>();
                registerAccountComponent.ShowResgiterCharacterLayout = true;

                registerAccountLayout.Create(player);
                registerAccountLayout.Show(player);
            }
            else
            {
                _ = Authenticate(player);
            }
        }
    }
}