using System.Threading.Tasks;
using TheEconomy.Database.Entity.Account;
using TheEconomy.Database.Entity.Prohibitions;

#nullable enable

namespace TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;

public interface IVerifyProhibition
{
    Task<(ProhibitionEntity? Prohibition, AccountEntity? Account)> Verify(string name, string IP);
}