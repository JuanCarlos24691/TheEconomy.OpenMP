using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Services.Interfaces
{
    public interface IDeleteConversation
    {
        public void DeleteTheGlobalConversation(string whoDeletedTheGlobalConversation = null);
        public void DeletePlayerConversation(Player player, string whoDeletedThePlayerConversation = null);
    }
}