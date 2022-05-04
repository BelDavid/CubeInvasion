using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class Projectile: Entity, IProjectileTick
    {
        public Projectile(string ID, Game game, EntityType entType, GFCoordinates coords, GGraphics[] graphics, Direction dir, int damage) 
            : base(ID, game, entType, coords.SetZ(Util.gfLayerBullet), graphics[(int)(Math.Log((int)dir)/Math.Log(2))])
        {
            this.Dir = dir;
            this.Damage = damage;
        }

        public Direction Dir { get; private set; }

        public int Damage { get; protected set; }

        public void Tick_Projectile()
        {
            if (!game.MoveEntity(this, ((Dir & Direction.RIGHT) != 0 ? 1 : 0) - ((Dir & Direction.LEFT) != 0 ? 1 : 0),
            ((Dir & Direction.DOWN) != 0 ? 1 : 0) - ((Dir & Direction.UP) != 0 ? 1 : 0)))
                Dispose();
        }

        public override void IntersectWith(Entity e)
        {
            if (e is Mob m && m.EntType != this.EntType)
            {
                m.Hit(Damage);
                Dispose();
            }
            else if (e is Explosive ex && ex.EntType != EntityType.HOSTILE)
            {
                Dispose();
                ex.Dispose();
            }
            else if (e is Projectile)
            {
                if (this.EntType == e.EntType)
                {
                    e.Dispose();
                    Dispose();
                }
                else
                {
                    game.SwapPlaces(e, this);
                    return;
                }
            }
            base.IntersectWith(e);
        }
        protected override void IntersectBy(Entity e)
        {
            if (e is Mob m && m.EntType != this.EntType)
            {
                m.Hit(Damage);
                Dispose();
            }
            base.IntersectBy(e);
        }
    }
}
