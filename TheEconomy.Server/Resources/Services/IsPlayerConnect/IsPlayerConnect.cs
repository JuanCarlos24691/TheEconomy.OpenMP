using System;
using System.Linq;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.IsPlayerConnect.Interfaces;

namespace TheEconomy.Server.Resources.Services.IsPlayerConnect;

public class IsPlayerConnect(IEntityManager entityManager) : IIsPlayerConnect
{
    public bool Verify(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("La fecha no puede ser nula, vacía ni contener espacios en blanco.", nameof(name));

        return entityManager.GetComponents<Player>()
            .Any(p => p.Name == name);
    }
}