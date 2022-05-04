using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    enum EntityType : int
    {
        PASSIVE = 0,
        FRIENDLY = 1,
        HOSTILE = 2
    }
}
