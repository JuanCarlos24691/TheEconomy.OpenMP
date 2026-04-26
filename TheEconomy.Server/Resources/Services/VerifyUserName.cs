using System.Linq;
using TheEconomy.Server.Resources.Services.Interfaces;

namespace TheEconomy.Server.Resources.Services
{
    public class VerifyUserName : IVerifyUserName
    {
        public bool ObtainVerification(string userName)
        {
            if (!userName.All(c => c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c >= '0' && c <= '9'))
                return false;

            return true;
        }
    }
}