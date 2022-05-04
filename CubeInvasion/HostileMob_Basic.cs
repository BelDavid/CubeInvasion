using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{ 
    [Serializable]
    class HostileMob_Basic : Mob, IHostileMob
    {
        public HostileMob_Basic(string ID, Game game, GFCoordinates coords, GGraphics graphics, int hp, int dmg, LootSetup lootSetup): 
            base(ID, game, EntityType.HOSTILE, coords, graphics, hp, dmg, lootSetup)
        {

        }

        public override void Tick_Mob()
        {
            if (!DespawningOrDestroyed)
            {
                AI.Tick_HostileMob_Basic(game, this);
            }
            base.Tick_Mob();
        }
    }
}
