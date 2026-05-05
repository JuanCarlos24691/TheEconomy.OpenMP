using System.Threading.Tasks;
using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.KnowledgeTest.Interfaces;

public interface IKnowledgeTest
{
    Task<bool> Start(Player player);
}