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
        try
        {
            while (reconnectionAttempts < ServerInitializationData.MaxReconnectionAttempts)
            {
                if (databaseContext.Database.CanConnect())
                {
                    serverInformation.Initialize();
                    ApplyServerSettings(serverService);

                    string hostName = string.Format(ServerInitializationData.HostNameFormat, serverInformation.Name, serverInformation.Mode, serverInformation.Version.ToString());

                    StringBuilder builder = new();

                    builder.AppendLine("\n---------------------------------------------------------------------------");
                    builder.AppendLine();
                    builder.AppendLine("\tConfiguración del servidor:");
                    builder.AppendLine();
                    builder.AppendLine($"\tNombre del servidor: {serverInformation.Name}");
                    builder.AppendLine($"\tModo de juego: {serverInformation.Mode}");
                    builder.AppendLine($"\tVersión del servidor: v{serverInformation.Version}");
                    builder.AppendLine($"\tNombre de dominio: {hostName}");
                    builder.AppendLine($"\tIdioma(s): {serverInformation.Language}");
                    builder.AppendLine($"\tPágina Web: {serverInformation.WebSite}");
                    builder.AppendLine($"\tForo: {serverInformation.Forum}");
                    builder.AppendLine($"\tDiscord: {serverInformation.Discord}");
                    builder.AppendLine();
                    builder.AppendLine("\tBase de datos: La conexión a la base de datos se realizó correctamente");
                    builder.AppendLine();
                    builder.AppendLine("\t+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    builder.AppendLine("\t+++++ Desarrollador: Juan Carlos | JuanCarlo24691 ++++");
                    builder.AppendLine("\t+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    builder.AppendLine();
                    builder.AppendLine("---------------------------------------------------------------------------\n");

                    Console.Write(builder.ToString());
                    break;
                }

                HandleConnectionFailure(serverService);
            }
        }
        catch (Exception ex)
        {
            Console.Write($"La conexión a la base de datos ha fallado. Se intentara una reconexión en 5 segundos...\n{ex.Message}");
            HandleConnectionFailure(serverService);
        }
    }

    private void ApplyServerSettings(IServerService serverService)
    {
        string hostName = string.Format(ServerInitializationData.HostNameFormat, serverInformation.Name, serverInformation.Mode, serverInformation.Version.ToString());

        serverService.SendRconCommand($"name {hostName}");
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
            Console.WriteLine("No se pudo conectar a la base de datos. Cerrando servidor...");
            serverService.SendRconCommand("exit");
            Environment.Exit(0);
        }
    }
}