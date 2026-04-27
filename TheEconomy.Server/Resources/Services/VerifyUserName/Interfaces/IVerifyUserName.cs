namespace TheEconomy.Server.Resources.Services.VerifyUserName.Interfaces
{
    public interface IVerifyUserName
    {
        public bool ObtainVerification(string userName);
    }
}