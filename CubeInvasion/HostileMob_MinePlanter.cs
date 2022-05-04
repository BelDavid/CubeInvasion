using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class HostileMob_MinePlanter : HostileArmoredMob
    {
        public HostileMob_MinePlanter(string ID, Game game, GFCoordinates coords, GGraphics graphics, int hp, int dmg, int initialAmmo_Mine, LootSetup lootSetup) : 
            base(ID, game, coords, graphics, hp, dmg, lootSetup)
        {
            GiveWeapon(WeaponType.Mine);
            AddAmmo(WeaponType.Mine, initialAmmo_Mine);
        }

        protected int plantFrequencyCounter = 10;

        public override void Tick_Mob()
        {
            if (!DespawningOrDestroyed)
            {
                if (plantFrequencyCounter == 0)
                {
                    plantFrequencyCounter = Util.rand.Next(10, 15);

                    Fire(Direction.CENTER);
                }
                else
                    plantFrequencyCounter--;


                AI.Tick_HostileMob_Basic(game, this);
            }
            base.Tick_Mob();
        }
    }
}
