using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class Item : Entity
    {
        public Item(string ID, Game game, GFCoordinates coords, GGraphics graphics, int amount) : base(ID, game, EntityType.PASSIVE, coords.SetZ(Util.gfLayerItem), graphics)
        {
            this.Amount = amount;
        }

        public int Amount { get; protected set; }

        public virtual void Strike()
        {

        }
    }
}
