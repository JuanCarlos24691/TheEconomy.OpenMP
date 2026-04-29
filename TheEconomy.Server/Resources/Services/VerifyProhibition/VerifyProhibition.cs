using System;
using System.Threading.Tasks;
using TheEconomy.Database;
using TheEconomy.Database.Entity.Prohibitions;
using TheEconomy.Database.Entity.Account;
using Microsoft.EntityFrameworkCore;
using TheEconomy.Server.Resources.Components.AccountInformation;
using TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;

namespace TheEconomy.Server.Resources.Services.VerifyProhibition;

public class VerifyProhibition(DatabaseContext databaseContext) : IVerifyProhibition
{
    public async Task<AccountInformation> Verify(string name, string IP)
    {
        Task<ProhibitionEntity> prohibition = databaseContext.Prohibitions.FirstOrDefaultAsync(p => p.IP == IP);
        Task<AccountEntity> account = databaseContext.Accounts.FirstOrDefaultAsync(a => a.Name == name && a.ProhibitedAccount > 0);
        await Task.WhenAll(prohibition, account);

        return new AccountInformation { Prohibition = prohibition.Result, Account = account.Result };
    }
}