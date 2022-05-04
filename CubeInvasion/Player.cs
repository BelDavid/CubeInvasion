using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class Player : ArmoredMob
    {
        public Player(Game game, GFCoordinates coords, int hp, int dmg):
            base(cID, game, EntityType.FRIENDLY, coords, GGraphics.mob_player, hp, dmg, null)
        {

        }
        
        public override void IntersectWith(Entity e)
        {
            if (e is IHostileMob)
            {
                ((Mob)e).Hit(DMG);
            }
            base.IntersectWith(e);
        }
        
        public override float GetHPRatio()
        {
            return -1;
        }

        public const string cID = "player";
    }
}
