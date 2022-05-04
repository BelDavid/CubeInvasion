using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class Mine : Explosive
    {
        public Mine(string ID, Game game, EntityType entType, GFCoordinates coords) :
            base(ID, game, entType, coords, GGraphics.explosive_mine, Util.mine_DMG, Util.mine_Intensity)
        {

        }

        protected override void IntersectBy(Entity e)
        {
            if (e is Mob m && !(m.EntType == this.EntType && this.EntType == EntityType.HOSTILE))
            {
                Dispose();
            }
            base.IntersectBy(e);
        }
    }
}
