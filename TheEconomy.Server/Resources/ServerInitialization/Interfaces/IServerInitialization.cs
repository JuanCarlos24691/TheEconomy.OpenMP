using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.ServerInitialization.Interfaces;

public interface IServerConfiguration
{
    void Configure(IServerService serverService);
}