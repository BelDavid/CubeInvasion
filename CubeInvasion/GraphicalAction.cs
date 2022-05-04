using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class GraphicalAction : Action
    {
        public GraphicalAction(string ID, Game game, GFCoordinates coords, FGPosition fgPosition) : base(ID, game, coords, fgPosition)
        {

        }
    }
}
