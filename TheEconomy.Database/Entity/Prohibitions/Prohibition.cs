namespace TheEconomy.Database.Entity.Prohibitions
{
    public class Prohibition
    {
        public Guid UUID { get; set; }
        public required string IP { get; set; }
        public required string ProhibitedBy { get; set; }
        public required string Reason { get; set; }
        public required DateTime DateOfProhibition { get; set; }
    }
}