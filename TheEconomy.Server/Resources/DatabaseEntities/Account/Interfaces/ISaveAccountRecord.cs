using System.Threading.Tasks;
using SampSharp.Entities.SAMP;

#nullable enable

namespace TheEconomy.Server.Resources.DatabaseEntities.Account.Interfaces;

public interface ISaveAccountRecord
{
    Task Save(Player? player = null);
}