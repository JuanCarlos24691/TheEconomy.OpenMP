namespace TheEconomy.Database.Entity.ServerInformation
{
    public class ServerInformationEntity
    {
        public Guid UUID { get; set; }
        public string? Name { get; set; }
        public string? Mode { get; set; }
        public string? Language { get; set; }
        public string? Version { get; set; }
        public string? WebSite { get; set; }
        public string? Forum { get; set; }
        public string? Discord { get; set; }
    }
}