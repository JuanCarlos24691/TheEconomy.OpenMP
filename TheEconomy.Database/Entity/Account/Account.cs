namespace TheEconomy.Database.Entity.Account
{
    public class Account
    {
        public Guid UUID { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Mail { get; set; }
        public int? AdministrativeLevel { get; set; }
        public int? SelectedCharacter { get; set; }
        public int? ProhibitedAccount { get; set; }
        public string? AccountProhibitedBy { get; set; }
        public string? ReasonForProhibition { get; set; }
        public DateTime? DateOfProhibition { get; set; }
    }
}