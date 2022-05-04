using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class HealingItem : Item
    {
        public HealingItem(string ID, Game game, GFCoordinates coords, GGraphics graphics, int heartAmout) :base(ID, game, coords, graphics, heartAmout)
        {

        }

        protected override void IntersectBy(Entity e)
        {
            if (e is Player p)
            {
                p.Heal(Amount);
                Dispose();
            }
            base.IntersectBy(e);
        }
    }
}
