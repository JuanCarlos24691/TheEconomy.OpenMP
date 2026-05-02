using System.Threading.Tasks;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Components.AccountInformation;

namespace TheEconomy.Server.Resources.Services.VerifyProhibition.Interfaces;

public interface IVerifyProhibition
{
    Task<AccountInformation> Verify(string name,string IP);
}