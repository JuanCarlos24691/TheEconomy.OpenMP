using System;
using System.Collections.Generic;
using System.Configuration;
using TheEconomy.Database;

using TheEconomy.Server.Resources.Services.Interfaces;

namespace TheEconomy.Server.Resources.Services
{
    public class ServerInformation : IServerInformation
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public string Language { get; set; }
        public Version Version { get; set; }
        public string WebSite { get; set; }
        public string Forum { get; set; }
        public string Discord { get; set; }

        // Constructor for initializing values
        public ServerInformation(DatabaseContext databaseContext)
        {
            List<TheEconomy.Database.Entity.ServerInformation.ServerInformation> serverInformation = [.. databaseContext.ServerInformation];

            if (serverInformation.Count is not 0)
            {
                // load the information from the database
                Name = serverInformation[0].Name;
                Mode = serverInformation[0].Mode;
                Language = serverInformation[0].Language;
                Version = new Version(serverInformation[0].Version);
                WebSite = serverInformation[0].WebSite;
                Forum = serverInformation[0].Forum;
                Discord = serverInformation[0].Discord;
            }
            else
            {
                // load the information from the App.config
                Name = GetAppSetting("name", "unknown");
                Mode = GetAppSetting("mode", "unknown");
                Language = GetAppSetting("language", "unknown");
                Version = new Version(GetAppSetting("version", "0.0"));
                WebSite = GetAppSetting("website", "www.unknown.com");
                Forum = GetAppSetting("forum", "forum.unknown.com");
                Discord = GetAppSetting("discord", "www.discord.gg/unknown");

                TheEconomy.Database.Entity.ServerInformation.ServerInformation setTheServerInformationInTheDatabase = new()
                {
                    Name = Name,
                    Mode = Mode,
                    Language = Language,
                    Version = Convert.ToString(Version),
                    WebSite = WebSite,
                    Forum = Forum,
                    Discord = Discord
                };

                databaseContext.Add(setTheServerInformationInTheDatabase);
                databaseContext.SaveChanges();
            }
        }

        public string GetAppSetting(string key, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("La clave no puede ser nula, estar vacía ni contener solo espacios en blanco.", nameof(key));

            if (string.IsNullOrWhiteSpace(defaultValue))
                throw new ArgumentException("El valor por defecto no puede ser nulo, estar vacío ni contener solo espacios en blanco.", nameof(defaultValue));

            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            return value;
        }
    }
}