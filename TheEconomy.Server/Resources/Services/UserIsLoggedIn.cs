using System;
using System.Linq;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Services
{
    public class UserIsLoggedIn(IEntityManager entityManager)
    {
        public bool LoggedIn(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("El nombre de usuario no puede estar vacío, nulo ni contener espacios en blanco.", nameof(userName));

            Player[] player = entityManager.GetComponents<Player>();
            bool userIsLoggedIn = player.Any(player => player != null && string.Equals(player.Name, userName, StringComparison.OrdinalIgnoreCase));

            return userIsLoggedIn;
        }
    }
}