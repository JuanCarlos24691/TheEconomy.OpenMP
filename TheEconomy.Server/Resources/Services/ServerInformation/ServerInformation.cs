using System;
using System.Collections.Generic;
using System.Configuration;
using TheEconomy.Database;
using TheEconomy.Database.Entity.ServerInformation;
using TheEconomy.Server.Resources.Services.ServerInformation.Interfaces;

public class ServerInformation(DatabaseContext databaseContext) : IServerInformation
{
    public string Name { get; set; }
    public string Mode { get; set; }
    public string Language { get; set; }
    public Version Version { get; set; }
    public string WebSite { get; set; }
    public string Forum { get; set; }
    public string Discord { get; set; }

    public void Initialize()
    {
        List<ServerInformationEntity> serverInformation = [.. databaseContext.ServerInformation];

        if (serverInformation.Count is not 0)
        {
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
            Name = GetAppSetting("name", "unknown");
            Mode = GetAppSetting("mode", "unknown");
            Language = GetAppSetting("language", "unknown");
            Version = new Version(GetAppSetting("version", "0.0"));
            WebSite = GetAppSetting("website", "www.unknown.com");
            Forum = GetAppSetting("forum", "forum.unknown.com");
            Discord = GetAppSetting("discord", "www.discord.gg/unknown");

            databaseContext.Add(new ServerInformationEntity
            {
                Name = Name,
                Mode = Mode,
                Language = Language,
                Version = Convert.ToString(Version),
                WebSite = WebSite,
                Forum = Forum,
                Discord = Discord
            });

            databaseContext.SaveChanges();
        }
    }

    public string GetAppSetting(string key, string defaultValue)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("La clave no puede ser nula.", nameof(key));

        if (string.IsNullOrWhiteSpace(defaultValue))
            throw new ArgumentException("El valor por defecto no puede ser nulo.", nameof(defaultValue));

        string value = ConfigurationManager.AppSettings[key];
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }
}