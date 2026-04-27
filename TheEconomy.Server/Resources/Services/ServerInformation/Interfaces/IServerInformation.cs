using System;

namespace TheEconomy.Server.Resources.Services.ServerInformation.Interfaces
{
    public interface IServerInformation
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public string Language { get; set; }
        public Version Version { get; set; }
        public string WebSite { get; set; }
        public string Forum { get; set; }
        public string Discord { get; set; }

        public string GetAppSetting(string key, string defaultValue);
    }
}