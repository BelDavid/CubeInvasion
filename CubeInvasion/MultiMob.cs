using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class MultiMob : Mob, IHostileMob
    {
        public MultiMob(string ID, Game game, GFCoordinates coords, GGraphics graphics, int hp, int dmg, byte[,] occupiedSpace, LootSetup lootSetup) :
            base(ID, game, EntityType.HOSTILE, coords, graphics, hp, dmg, lootSetup)
        {
            CenterOffset = new GFCoordinates(occupiedSpace.GetLength(0) / 2, occupiedSpace.GetLength(1) / 2);
            this.OccupiedSpace = occupiedSpace;
        }



        /// <summary>
        /// 0 - Tile is not part of mob.
        /// 1 - Tile is Part of a mob.
        /// 2 - Transparent part (Part of a mob, but not visible).
        /// </summary>
        public byte[,] OccupiedSpace { get; protected set; }
        public GFCoordinates CenterOffset { get; protected set; }

        public override void Dispose()
        {
            if (lootSetup != null)
                for (int y = 0; y < OccupiedSpace.GetLength(1); y++)
                    for (int x = 0; x < OccupiedSpace.GetLength(0); x++)
                        if (OccupiedSpace[x, y] == 1 && (CenterOffset.X != x || CenterOffset.Y != y))
                            lootSetup.GenerateLoot(game, Coords.X - CenterOffset.X + x, Coords.Y - CenterOffset.Y + y);

            base.Dispose();
        }

        public void DrawTile(Graphics g, int x, int y)
        {
            if (x == Coords.X && y == Coords.Y)
                Draw(g);
        }
        public override void Draw(Graphics g)
        {
            for (int y = 0; y < OccupiedSpace.GetLength(1); y++)
                for (int x = 0; x < OccupiedSpace.GetLength(0); x++)
                    if (OccupiedSpace[x, y] == 1)
                        graphics.RenderInGF(g, Coords.X - CenterOffset.X + x, Coords.Y - CenterOffset.Y + y);
            base.Draw(g);
        }

        public override void Tick_Mob()
        {
            if (!DespawningOrDestroyed)
            {
                AI.Tick_HostileMob_Basic(game, this);
            }
            base.Tick_Mob();
        }
    }
}
