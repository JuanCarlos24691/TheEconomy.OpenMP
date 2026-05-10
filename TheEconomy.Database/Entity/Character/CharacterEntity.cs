using TheEconomy.Database.Entity.Account;

namespace TheEconomy.Database.Entity.Character
{
    public class CharacterEntity
    {
        public Guid UUID { get; set; }
        public Guid AUUID { get; set; }
        public virtual AccountEntity? Account { get; set; }
        public bool Online { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int Gender { get; set; } = -1;
        public DateTime BirthDate { get; set; }
        public int Appearance { get; set; }
        public string? Height { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColor { get; set; }
        public string? SkinColor { get; set; }
        public float SpawnX { get; set; }
        public float SpawnY { get; set; }
        public float SpawnZ { get; set; }
        public float Angle { get; set; }
        public DateTime FirstConnection { get; set; }
        public DateTime LastConnection { get; set; }
        public int? StabbingGun { get; set; }
        public int? NumberOfBladedGun { get; set; }
        public int? ShortGun { get; set; }
        public int? ShortGunAmmunition { get; set; }
        public int? LongGun { get; set; }
        public int? LongGunAmmunition { get; set; }
    }
}