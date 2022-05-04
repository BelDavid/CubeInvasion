using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class HostileMob_BowShooter : HostileArmoredMob
    {
        public HostileMob_BowShooter(string ID, Game game, GFCoordinates coords, GGraphics graphics, int hp, int dmg, int initialAmmo_Bow, LootSetup lootSetup) : 
            base(ID, game, coords, graphics, hp, dmg, lootSetup)
        {
            GiveWeapon(WeaponType.Bow);
            AddAmmo(WeaponType.Bow, initialAmmo_Bow);
        }

        public override void Tick_Mob()
        {
            if (!DespawningOrDestroyed)
            {
                AI.Tick_HostileMob_BowShooter(game, this);
            }
            base.Tick_Mob();
        }
    }
}
