using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class Explosive : Entity
    {
        public Explosive(string ID, Game game, EntityType entType, GFCoordinates coords, GGraphics graphics, int damage, int intensity) :
            base(ID, game, entType, coords.SetZ(Util.gfLayerExplosive), graphics)
        {
            this.Damage = damage;
            this.Intensity = intensity;
        }

        public int Damage { get; protected set; }
        public int Intensity { get; protected set; }


        public override void Dispose()
        {
            if (!DespawningOrDestroyed)
            {
                game.SpawnAction(new Explosion(Action.NewID, game, Coords, Damage, Intensity));
            }
            base.Dispose();
        }
    }
}
