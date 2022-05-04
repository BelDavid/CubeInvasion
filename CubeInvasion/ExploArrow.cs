using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class ExploArrow : Projectile
    {
        public ExploArrow(string ID, Game game, EntityType entType, GFCoordinates coords, Direction dir) : base(ID, game, entType, coords, GGraphics.projectile_exploArrow, dir, Util.exploArrow_DMG)
        {

        }
        

        public override void Dispose()
        {
            if (!DespawningOrDestroyed)
            {
                game.SpawnAction(new Explosion(Action.NewID, game, Coords, Damage, Util.exploArrow_Intensity));
            }
            base.Dispose();
        }
    }
}
