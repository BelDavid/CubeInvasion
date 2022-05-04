using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class Explosion : StrikeAction
    {
        public Explosion(string ID, Game game, GFCoordinates coords, int damage, int intensity):base(ID, game, coords, FGPosition.Foreground, damage)
        {
            this.Intensity = intensity;
        }
        
        public int Intensity { get; protected set; }
        
        public override void Draw(Graphics g)
        {
            GGraphics.action_explosion_center.RenderInGF(g, Coords.X, Coords.Y);

            int from = step - Intensity >= 0 ? step - Intensity : 0, to = step > Intensity ? Intensity : step;
            for (int i = from; i < to; i++)
            {
                for (int j = 0; j < Util.rings[i].GetLength(0); j++)
                {
                    GGraphics.action_explosion.RenderInGF(g, Coords.X + Util.rings[i][j, 0], Coords.Y + Util.rings[i][j, 1]);
                }
            }
        }
        public override void Tick_Action()
        {
            if (!Finalized)
            {
                if (step == 0)
                    game.Strike(this, Coords.X, Coords.Y);
                if (step < Intensity)
                {
                    for (int i = 0; i < Util.rings[step].GetLength(0); i++)
                        game.Strike(this, Coords.X + Util.rings[step][i, 0], Coords.Y + Util.rings[step][i, 1]);
                }
                else if (step > Intensity * 2)
                    Finalized = true;

                step++;
            }
        }
    }
}
