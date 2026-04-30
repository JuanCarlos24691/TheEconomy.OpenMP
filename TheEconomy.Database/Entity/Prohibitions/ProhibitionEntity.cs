namespace TheEconomy.Database.Entity.Prohibitions
{
    public class ProhibitionEntity
    {
        public Guid UUID { get; set; }
        public string? IP { get; set; }
        public string? ProhibitedBy { get; set; }
        public string? Reason { get; set; }
        public DateTime DateOfProhibition { get; set; }
    }
}