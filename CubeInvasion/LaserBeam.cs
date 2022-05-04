using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class LaserBeam : StrikeAction
    {
        public LaserBeam(string ID, Game game, GFCoordinates coords, Direction dir):
            base(ID, game, coords, FGPosition.Foreground, Util.laser_Beam_DMG)
        {
            this.Dir = dir;
            Coords1 = coords.Copy();

            dX = ((dir & Direction.RIGHT) != 0 ? 1 : 0) - ((dir & Direction.LEFT) != 0 ? 1 : 0);
            dY = ((dir & Direction.DOWN) != 0 ? 1 : 0) - ((dir & Direction.UP) != 0 ? 1 : 0);

            if (Dir == Direction.LEFT || Dir == Direction.RIGHT)
                graphics = GGraphics.action_laserBeam_horizontal;
            else if (Dir == Direction.UP || Dir == Direction.DOWN)
                graphics = GGraphics.action_laserBeam_vertical;
        }

        public GFCoordinates Coords1 { get; protected set; }
        public Direction Dir { get; protected set; }
        protected int dX, dY;

        protected GGraphics graphics;

        public override void Tick_Action()
        {
            for (int i = 0; i < Util.laser_Beam_Speed; i++)
            {
                game.Strike(this, Coords.X, Coords.Y);

                Coords.X += dX;
                Coords.Y += dY;

                if (step > Util.laser_Beam_Length)
                {
                    Coords1.X += dX;
                    Coords1.Y += dY;
                }

                step++;

                if (Coords1.X < 0 || Coords1.X >= Util.gameField_Width || Coords1.Y < 0 || Coords1.Y >= Util.gameField_Height)
                {
                    Finalized = true;
                    break;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            int d = Math.Max(Math.Abs(Coords.X - Coords1.X), Math.Abs(Coords.Y - Coords1.Y)),
                ddX = 0, ddY = 0;
            for (int i = 0; i < d; i++)
            {
                graphics.RenderInGF(g, Coords1.X + ddX, Coords1.Y + ddY);

                ddX += dX;
                ddY += dY;
            }
        }
    }
}
