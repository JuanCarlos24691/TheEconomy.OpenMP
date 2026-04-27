namespace TheEconomy.Server.Resources.Services.VerifyMail.Interfaces
{
    public interface IVerifyMail
    {
        public bool ObtainVerification(string email);
    }
}