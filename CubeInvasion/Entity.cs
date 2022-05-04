using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class Entity
    {
        public Entity(string ID, Game game, EntityType eType, GFCoordinates coords, GGraphics graphics)
        {
            this.ID = ID;
            this.game = game;
            this.EntType = eType;
            this.Coords = coords;
            this.graphics = graphics;
        }

        [NonSerialized]
        public Game game;

        public string ID { get; }
        public EntityType EntType { get; }

        public int GfLayer => Coords.Z;
        public GFCoordinates Coords { get; set; }
        protected GGraphics graphics;
        
        public bool Despawnig { get; protected set; }
        public bool Destroyed { get; protected set; }

        public bool DespawningOrDestroyed => Despawnig || Destroyed;

        public virtual void Restore(Game game)
        {
            this.game = game;
            Despawnig = Destroyed = false;
        }

        public virtual void Dispose()
        {
            if (!DespawningOrDestroyed)
            {
                game.AddToDespawnEntity(this);
                Despawnig = true;
            }
        }
        public virtual void Term()
        {
            Destroyed = true;
            game = null;
        }
        public virtual void Draw(Graphics g)
        {
            graphics.RenderInGF(g, Coords.X, Coords.Y);
        }

        public virtual void IntersectWith(Entity e) { e.IntersectBy(this); }
        protected virtual void IntersectBy(Entity e) { }

        public static int _ID = 0;
        public static string NewID { get { return "ent"+_ID++; } }
    }
}
