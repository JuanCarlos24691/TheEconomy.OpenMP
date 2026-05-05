namespace TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces
{
    public interface ICorrectTextStrings
    {
        public string Correct(string message, bool replaceSpaces = true);
    }
}