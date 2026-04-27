using System;
using System.Text;
using System.Threading;
using TheEconomy.Database;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;
using TheEconomy.Server.Resources.ServerInitialization.Data;

namespace TheEconomy.Server.Resources.ServerInitialization;

public class ServerInitialization(IServerInformation serverInformation, DatabaseContext databaseContext) : ISystem
{
    private int reconnectionAttempts = 0;

    [Event]
    public void OnGameModeInit(IServerService serverService)
    {
        while (reconnectionAttempts < ServerInitializationData.MaxReconnectionAttempts)
        {
            if (databaseContext.Database.CanConnect())
            {
                ApplyServerSettings(serverService);

                StringBuilder builder = new();
                builder.AppendLine("\n---------------------------------------------------------------------------");
                builder.AppendLine("\tConfiguración del servidor cargada exitosamente");
                builder.AppendLine($"\tNombre: {serverInformation.Name}");
                builder.AppendLine($"\tModo: {serverInformation.Mode}");
                builder.AppendLine($"\tVersión: v{serverInformation.Version}");
                builder.AppendLine($"\tWeb: {serverInformation.WebSite}");
                builder.AppendLine("\tBase de Datos: Online");
                builder.AppendLine("---------------------------------------------------------------------------\n");

                Console.Write(builder.ToString());
                break;
            }

            HandleConnectionFailure(serverService);
        }
    }

    private void ApplyServerSettings(IServerService serverService)
    {
        string hostname = string.Format(ServerInitializationData.HostnameFormat, serverInformation.Name, serverInformation.Mode, serverInformation.Version.ToString());

        serverService.SendRconCommand($"name {hostname}");
        serverService.SetGameModeText(serverInformation.Mode);
        serverService.SendRconCommand($"language {serverInformation.Language}");
        serverService.SendRconCommand($"website {serverInformation.WebSite}");
    }

    private void HandleConnectionFailure(IServerService serverService)
    {
        reconnectionAttempts++;

        if (reconnectionAttempts < ServerInitializationData.MaxReconnectionAttempts)
        {
            Console.WriteLine($"La conexión a la base de datos ha fallado. Reintento {reconnectionAttempts}/{ServerInitializationData.MaxReconnectionAttempts} en 5s...");
            Thread.Sleep(ServerInitializationData.MillisecondsBetweenAttempts);
        }
        else
        {
            Console.WriteLine("CRITICAL: No se pudo conectar a la base de datos. Cerrando servidor...");
            serverService.SendRconCommand("exit");
            Environment.Exit(0);
        }
    }
}