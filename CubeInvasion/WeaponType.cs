using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    enum WeaponType : int
    {
        NONE = -1,

        Bow = 0,
        Mine = 1,
        ExploBow = 2,
        Bomb = 3,
        Laser = 4,

        COUNT = Laser + 1
    }
}
