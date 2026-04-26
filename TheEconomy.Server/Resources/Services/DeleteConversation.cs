using System;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.Interfaces;

namespace TheEconomy.Server.Resources.Services
{
    public class DeleteConversation(IWorldService worldService, Colors color) : IDeleteConversation
    {
        private const int NumberOfBlankMessages = 99;

        public void DeletePlayerConversation(Player player, string whoDeletedThePlayerConversation = null)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "La entidad player no puede ser nula. Por favor, asegúrese de que se haya inicializado correctamente.");

            for (int i = 0; i < NumberOfBlankMessages; i++)
                player.SendClientMessage(-1, "");

            if (!string.IsNullOrWhiteSpace(whoDeletedThePlayerConversation))
                player.SendClientMessage(-1, $"{color.GetHexadecimal("primaryGreen")}El administrador {whoDeletedThePlayerConversation} ha borrado tu vista de la conversación global.");
        }

        public void DeleteTheGlobalConversation(string whoDeletedTheGlobalConversation = null)
        {
            for (int i = 0; i < NumberOfBlankMessages; i++)
                worldService.SendClientMessage(-1, "");

            if (!string.IsNullOrWhiteSpace(whoDeletedTheGlobalConversation))
                worldService.SendClientMessage(-1, $"{color.GetHexadecimal("primaryGreen")}El administrador {whoDeletedTheGlobalConversation} ha borrado la conversación global.");
        }
    }
}