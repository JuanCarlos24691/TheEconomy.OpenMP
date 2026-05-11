using SampSharp.Entities.SAMP;
using TheEconomy.Database.Entity.Character;

namespace TheEconomy.Server.Resources.PlayerApparence.Interfaces;

public interface ISetSpawnParameters
{
    public void Spawn(Player player, CharacterEntity character, bool forceSpawn = false);
}