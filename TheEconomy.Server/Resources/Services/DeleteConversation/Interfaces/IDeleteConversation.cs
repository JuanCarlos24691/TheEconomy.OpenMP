using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Services.DeleteConversation.Interfaces;

public interface IDeleteConversation
{
    void DeletePlayerConversation(Player player, string adminName = null);
    void DeleteTheGlobalConversation(string adminName = null);
}