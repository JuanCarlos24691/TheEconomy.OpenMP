using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.DeleteConversation.Interfaces;
using TheEconomy.Server.Resources.Services.DeleteConversation.Data;
using System;

namespace TheEconomy.Server.Resources.Services.DeleteConversation;

public class DeleteConversation(IWorldService worldService, IColors color) : IDeleteConversation
{
    public void DeletePlayerConversation(Player player, string adminName = null)
    {
        ArgumentNullException.ThrowIfNull(player);

        for (int i = 0; i < DeleteConversationData.NumberOfBlankMessages; i++)
        {
            player.SendClientMessage("");
        }

        if (string.IsNullOrWhiteSpace(adminName) != true)
        {
            player.SendClientMessage($"{color.GetHexadecimal("primaryGreen")}El administrador {adminName} ha borrado tu chat.");
        }
    }

    public void DeleteTheGlobalConversation(string adminName = null)
    {
        for (int i = 0; i < DeleteConversationData.NumberOfBlankMessages; i++)
        {
            worldService.SendClientMessage(Color.White, " ");
        }

        if (!string.IsNullOrWhiteSpace(adminName))
        {
            string msg = $"El administrador {adminName} ha borrado el chat global.";
            worldService.SendClientMessage(color.ObtainRGB("primaryGreen"), msg);
        }
    }
}