using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class GameField
    {
        public GameField(Entity[,,] gf)
        {
            this.gameField = gf;
        }

        private readonly Entity[,,] gameField;

        public int Width => gameField.GetLength(0);
        public int Height => gameField.GetLength(1);
        public int LayersCount => gameField.GetLength(2);

        public Entity this[GFCoordinates coords]
        {
            get => this[coords.X, coords.Y, coords.Z];
            set { this[coords.X, coords.Y, coords.Z] = value; }
        }
        public Entity this[int x, int y, int z]
        {
            get
            {
                if (IsInside(x, y, z))
                    return gameField[x, y, z];
                else
                    return null;
            }
            set
            {
                if (IsInside(x, y, z))
                    gameField[x, y, z] = value;
            }
        }

        public bool IsInside(int x, int y, int z)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height && z >= 0 && z < LayersCount;
        }
        public bool IsInside(int x, int width, int y, int height, int z)
        {
            return x >= 0 && x + width < Width && y >= 0 && y + height < Height && z >= 0 && z < LayersCount;
        }
    }
}
