using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class Note : Item
    {
        public Note(Game game, GFCoordinates coords) : base(cID, game, coords, GGraphics.item_note, 0)
        {

        }

        public bool Collided { get; set; }
        public bool CollectedNotDestroyed { get; set; }
        

        public override void Strike()
        {
            CollectedNotDestroyed = false;
            Collided = true;
            base.Strike();
        }

        public override void Restore(Game game)
        {
            base.Restore(game);
            Collided = false;
        }

        protected override void IntersectBy(Entity e)
        {
            if (e is Player)
            {
                CollectedNotDestroyed = true;
                Collided = true;
            }
            else if (e is Projectile)
            {
                CollectedNotDestroyed = false;
                Collided = true;
            }
            base.IntersectBy(e);
        }

        public const string cID = "note";
    }
}
