namespace TheEconomy.Server.Resources.Services.Interfaces
{
    public interface IVerifyEmail
    {
        public bool ObtainVerification(string email);
    }
}