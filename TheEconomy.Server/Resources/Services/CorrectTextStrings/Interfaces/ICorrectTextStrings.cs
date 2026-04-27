namespace TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces
{
    public interface ICorrectTextStrings
    {
        public string ObtainCorrection(string message, bool replaceSpaces = true);
    }
}