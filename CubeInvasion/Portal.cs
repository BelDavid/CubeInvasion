using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class Portal : Action
    {
        public Portal(Game game) : base("portal", game, new GFCoordinates(Util.gameField_Width/2, Util.gameField_Height/2), FGPosition.Background)
        {
            steps_MAX = Math.Max(Util.gameField_Width * Util.gameField_Tile_Width, Util.gameField_Height * Util.gameField_Tile_Height) / 2;
        }

        public readonly int steps_MAX;
        [NonSerialized]
        public static readonly Pen pen_portalRing = new Pen(Color.DarkBlue, 5);
        public readonly GGraphics graphics = GGraphics.graphicalAction_portal;
        public bool Activated { get; protected set; }
        public bool Shown { get; protected set; } = true;

        public override void Draw(Graphics g)
        {
            if (!Shown)
                return;
            
            if (step < steps_MAX)
                g.DrawRectangle(pen_portalRing, (Coords.X + 0.5f) * Util.gameField_Tile_Width - step, (Coords.Y + 0.5f) * Util.gameField_Tile_Height - step, 2 * step, 2 * step);
            else
                g.DrawRectangle(pen_portalRing, (Coords.X + 0.5f) * Util.gameField_Tile_Width - 2*steps_MAX + step, (Coords.Y + 0.5f) * Util.gameField_Tile_Height - 2*steps_MAX + step, (2 * steps_MAX - step) * 2, (2 * steps_MAX - step) * 2);

            graphics.RenderInGF(g, Coords.X, Coords.Y);
        }
        public void Hide()
        {
            Activated = false;
            Shown = false;
            step = 0;
        }
        public void Show()
        {
            Shown = true;
        }
        public override void Stop()
        {
            base.Stop();
            Hide();
        }
        public override void Tick_Action()
        {
            if (!Shown)
                return;

            if (game.player.Coords == this.Coords)
            {
                if (step < steps_MAX)
                    step += 15;
                else
                    step += 25;

                if (step > steps_MAX * 2)
                {
                    Activated = true;
                    Shown = false;
                }
            }
            else
                step = 0;
        }

    }
}
