using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class HostileArmoredMob : ArmoredMob, IHostileMob
    {
        public HostileArmoredMob(string ID, Game game, GFCoordinates coords, GGraphics graphics, int hp, int dmg, LootSetup lootSetup) :
            base(ID, game, EntityType.HOSTILE, coords, graphics, hp, dmg, lootSetup)
        {

        }
    }
}
