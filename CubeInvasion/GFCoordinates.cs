using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class GFCoordinates
    {
        private GFCoordinates()
        {

        }
        public GFCoordinates(int x, int y, bool generatedRandomly = false): this(x, y, 0, generatedRandomly) { }
        public GFCoordinates(int x, int y, int z, bool generatedRandomly = false)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.GeneratedRandomly = generatedRandomly;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool GeneratedRandomly { get; set; }

        public GFCoordinates SetZ(int z)
        {
            Z = z;
            return this;
        }


        public GFCoordinates Copy()
        {
            return new GFCoordinates(this.X, this.Y, this.Z);
        }
        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}]", X, Y, Z);
        }

        public static GFCoordinates operator +(GFCoordinates a, GFCoordinates b) => new GFCoordinates(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static GFCoordinates operator -(GFCoordinates a, GFCoordinates b) => new GFCoordinates(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static bool operator ==(GFCoordinates a, GFCoordinates b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        public static bool operator !=(GFCoordinates a, GFCoordinates b) => !(a == b);
    }
}
