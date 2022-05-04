using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class StrikeAction :Action
    {
        public StrikeAction(string ID, Game game, GFCoordinates coords, FGPosition fgPosition, int damage) : base(ID, game, coords, fgPosition)
        {
            this.damage = damage;
        }

        public int damage { get; protected set; }
        public List<string> hitedMobs = new List<string>();

        public override void Term()
        {
            hitedMobs.Clear();
            hitedMobs = null;
            base.Term();
        }
    }
}
