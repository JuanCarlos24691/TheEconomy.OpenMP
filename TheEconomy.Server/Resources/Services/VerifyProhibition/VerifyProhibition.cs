using System.Threading.Tasks;
using System.Linq;
using TheEconomy.Database;
using TheEconomy.Database.Entity.Prohibitions;
using TheEconomy.Database.Entity.Account;
using Microsoft.EntityFrameworkCore;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;

#nullable enable

namespace TheEconomy.Server.Resources.Services.VerifyProhibition;

public class VerifyProhibition(DatabaseContext databaseContext) : IVerifyProhibition
{
    public async Task<(ProhibitionEntity? Prohibition, AccountEntity? Account)> Verify(string name, string IP)
    {
        ProhibitionEntity? prohibition = await databaseContext.Prohibitions
            .FirstOrDefaultAsync(p => p.IP == IP);

        AccountEntity? account = await databaseContext.Accounts
            .Where(a => a.Name == name && (a.ProhibitedAccount > 0 || a.ProhibitedAccount == -1))
            .Select(a => new AccountEntity
            {
                AccountProhibitedBy = a.AccountProhibitedBy,
                ReasonForProhibition = a.ReasonForProhibition,
                DateOfProhibition = a.DateOfProhibition,
                ProhibitedAccount = a.ProhibitedAccount
            })
            .FirstOrDefaultAsync();
            
        return (prohibition, account);
    }
}