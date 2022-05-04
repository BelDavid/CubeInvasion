using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class AmmoPack : Item
    {
        public AmmoPack(string ID, Game game, GFCoordinates coords, WeaponType wt, int ammoAmout) :base(ID, game, coords, GGraphics.item_ammopack, ammoAmout)
        {
            this.Wt = wt;
        }

        public WeaponType Wt { get; protected set; }
    }
}
