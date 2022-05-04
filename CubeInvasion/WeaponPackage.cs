using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class WeaponPackage : Item
    {
        public WeaponPackage(string ID, Game game, GFCoordinates coords, WeaponType wt) : base(ID, game, coords, GGraphics.item_weaponpack, 0)
        {
            this.Wt = wt;
        }

        public WeaponType Wt { get; protected set; }

        protected override void IntersectBy(Entity e)
        {
            if (e is Player)
            {
                ((Player)e).GiveWeapon(Wt);
                game.WriteToLog(string.Format("New weapon: '{0}' found", Wt.ToString()), Color.Green);
                Dispose();
            }
            base.IntersectBy(e);
        }
    }
}
