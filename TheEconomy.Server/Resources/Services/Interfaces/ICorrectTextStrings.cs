namespace TheEconomy.Server.Resources.Services.Interfaces
{
    public interface ICorrectTextStrings
    {
        public string ObtainCorrection(string message, bool replaceSpaces = true);
    }
}