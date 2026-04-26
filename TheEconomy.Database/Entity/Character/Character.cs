namespace TheEconomy.Database.Entity.Character
{
    public class Character
    {
        public Guid UUID { get; set; }
        public bool Online { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required int Gender { get; set; }
        public required DateTime BirthDate { get; set; }
        public required int Appearance { get; set; }
        public required int Height { get; set; }
        public required string EyeColor { get; set; }
        public required string HairColor { get; set; }
        public required string SkinColor { get; set; }
        public required float SpawnX { get; set; }
        public required float SpawnY { get; set; }
        public required float SpawnZ { get; set; }
        public required float Angle { get; set; }
        public required DateTime FirstConnection { get; set; }
        public required DateTime LastConnection { get; set; }
        public int? StabbingGun { get; set; }
        public int? NumberOfBladedGun { get; set; }
        public int? ShortGun { get; set; }
        public int? ShortGunAmmunition { get; set; }
        public int? LongGun { get; set; }
        public int? LongGunAmmunition { get; set; }
    }
}