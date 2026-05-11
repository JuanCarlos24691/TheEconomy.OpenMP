using System;
using SampSharp.Entities.SAMP;
using TheEconomy.Database.Entity.Character;
using TheEconomy.Server.Resources.BlackBackground.Interfaces;
using TheEconomy.Server.Resources.PlayerApparence.Interfaces;

namespace TheEconomy.Server.Resources.PlayerApparence;

public class SetSpawnParameters : ISetSpawnParameters
{
    public void Spawn(Player player, CharacterEntity character, bool forceSpawn = false)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(character);

        player.CancelSelectTextDraw();
        player.ToggleSpectating(false);

        player.SetSpawnInfo(0, character.Appearance, new Vector3(character.SpawnX, character.SpawnY, character.SpawnZ), character.Angle);

        if (forceSpawn)
            player.Spawn();
    }
}