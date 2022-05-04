using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class Bomb: Explosive, IMobTick
    {
        public Bomb(string ID, Game game, EntityType entType, GFCoordinates coords) : base(ID, game, entType, coords, GGraphics.explosive_bomb, Util.bomb_DMG, Util.bomb_Intensity)
        {
            this.CountDown = CDprogress = Util.bomb_Countdown;
        }

        public int CountDown { get; private set; }
        public int CDprogress { get; private set; }
        

        public override void Draw(System.Drawing.Graphics g)
        {
            base.Draw(g);
            g.DrawString(CDprogress.ToString(), SystemFonts.DefaultFont, Brushes.Blue, (Coords.X + 3f / 4) * Util.gameField_Tile_Width, (Coords.Y + 3f / 4) * Util.gameField_Tile_Height);
        }

        public void Tick_Mob()
        {
            if (CDprogress > 0)
                CDprogress--;
            else
                Dispose();
        }
    }
}
