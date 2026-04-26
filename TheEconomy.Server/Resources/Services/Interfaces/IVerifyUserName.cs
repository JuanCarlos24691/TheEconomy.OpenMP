namespace TheEconomy.Server.Resources.Services.Interfaces
{
    public interface IVerifyUserName
    {
        public bool ObtainVerification(string userName);
    }
}