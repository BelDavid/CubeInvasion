using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class Action
    {
        public Action(string ID, Game game, GFCoordinates coords, FGPosition fgPosition)
        {
            this.ID = ID;
            this.game = game;
            this.Coords = coords;
            this.FGPosition = fgPosition;
        }

        [NonSerialized]
        protected Game game;

        public string ID { get; protected set; }
        public GFCoordinates Coords { get; protected set; }
        public FGPosition FGPosition { get; protected set; }
        public bool Finalized { get; protected set; }
        protected int step = 0;

        public abstract void Tick_Action();
        public abstract void Draw(Graphics g);

        public virtual void Restore(Game game)
        {
            this.game = game;
            Finalized = false;
        }
        public virtual void Stop()
        {
            Finalized = true;
        }
        public virtual void Term()
        {
            game = null;
            step = 0;
        }

        public static int _ID = 0;
        public static string NewID { get { return "act" + _ID++; } }
    }
}
