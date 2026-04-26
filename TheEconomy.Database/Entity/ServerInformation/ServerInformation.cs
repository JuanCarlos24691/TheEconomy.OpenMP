namespace TheEconomy.Database.Entity.ServerInformation
{
    public class ServerInformation
    {
        public Guid UUID { get; set; }
        public required string Name { get; set; }
        public required string Mode { get; set; }
        public required string Language { get; set; }
        public required string Version { get; set; }
        public required string WebSite { get; set; }
        public required string Forum { get; set; }
        public required string Discord { get; set; }
    }
}