using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class Arrow : Projectile
    {
        public Arrow(string ID, Game game, EntityType entType, GFCoordinates coords, Direction dir) : base(ID, game, entType, coords, GGraphics.projectile_arrow, dir, Util.arrow_DMG)
        {
            
        }
    }
}
