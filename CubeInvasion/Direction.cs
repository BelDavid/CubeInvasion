using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    enum Direction : int
    {
        NONE = -1,
        CENTER = 0,

        UP = 1,
        RIGHT = 2,
        DOWN = 4,
        LEFT = 8,

        UpRight = UP + RIGHT,
        UpLeft = UP + LEFT,
        DownRight = DOWN + RIGHT,
        DownLeft = DOWN + LEFT
    }
}
