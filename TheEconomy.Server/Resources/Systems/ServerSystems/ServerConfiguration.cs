using System;
using System.Globalization;
using System.Text;
using System.Threading;
using TheEconomy.Database;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services;

namespace TheEconomy.Server.Resources.Systems.ServerSystems
{
    public class ServerConfiguration(ServerInformation serverInformation, DatabaseContext databaseContext) : ISystem
    {
        private readonly int numberOfReconnectionAttempts = 3;
        private int reconnectionAttempts = 0;
        private readonly int millisecondsToWaitForReconnection = 5000;

        [Event]
        public void OnGameModeInit(IServerService serverService)
        {
            while (reconnectionAttempts != numberOfReconnectionAttempts)
            {
                if (databaseContext.Database.CanConnect())
                {
                    string name = serverInformation.Name;
                    string mode = serverInformation.Mode;
                    Version version = serverInformation.Version;
                    string serverHostname = $"(Android y Windows) {name} {mode} | v{Convert.ToString(version, CultureInfo.InvariantCulture)}";
                    string language = serverInformation.Language;
                    string webSite = serverInformation.WebSite;
                    string forum = serverInformation.Forum;
                    string discord = serverInformation.Discord;

                    serverService.SendRconCommand($"name {serverHostname}");
                    serverService.SetGameModeText(mode);
                    serverService.SendRconCommand($"language {language}");
                    serverService.SendRconCommand($"website {webSite}");

                    StringBuilder builder = new();

                    builder.AppendLine("\n---------------------------------------------------------------------------");
                    builder.AppendLine();
                    builder.AppendLine("\tConfiguración del servidor:");
                    builder.AppendLine();
                    builder.AppendLine($"\tNombre del servidor: {name}");
                    builder.AppendLine($"\tModo de juego: {mode}");
                    builder.AppendLine($"\tVersión del servidor: v{version}");
                    builder.AppendLine($"\tNombre de dominio: {serverHostname}");
                    builder.AppendLine($"\tIdioma(s): {language}");
                    builder.AppendLine($"\tPágina Web: {webSite}");
                    builder.AppendLine($"\tForo: {forum}");
                    builder.AppendLine($"\tDiscord: {discord}");
                    builder.AppendLine();
                    builder.AppendLine("\tBase de datos: La conexión a la base de datos se realizó correctamente");
                    builder.AppendLine();
                    builder.AppendLine("\t+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    builder.AppendLine("\t++++ Desarrollador: Juan Carlos | JuanCarlo24691 ++++");
                    builder.AppendLine("\t+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    builder.AppendLine();
                    builder.AppendLine("---------------------------------------------------------------------------\n");

                    Console.Write(builder.ToString());
                    break;
                }
                else
                {
                    reconnectionAttempts++;

                    if (reconnectionAttempts < numberOfReconnectionAttempts)
                    {
                        Console.Write("La conexión a la base de datos ha fallado. Se intentara una reconexión en 5 segundos...");
                        Thread.Sleep(millisecondsToWaitForReconnection);
                    }
                    else
                    {
                        Console.Write("No se pudo establecer la conexión con la base de datos. El servidor se cerrara de inmediato");
                        serverService.SendRconCommand("exit");
                        Environment.Exit(0);
                        break;
                    }
                }
            }
        }
    }
}