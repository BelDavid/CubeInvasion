using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class Mob : Entity, IMobTick
    {
        public Mob(string ID, Game game, EntityType entType, GFCoordinates coords, GGraphics graphics, int hp, int dmg, LootSetup lootSetup): 
            base(ID, game, entType, coords.SetZ(Util.gfLayerMob), graphics)
        {
            this.HP = this.InitialHP = hp;
            this.DMG = dmg;
            this.lootSetup = lootSetup;
        }

        public int HP { get; protected set; }
        public int InitialHP { get; protected set; }
        public int DMG { get; protected set; }

        public LootSetup lootSetup { get; protected set; }

        public void Hit(int dmg)
        {
            if (dmg < 0)
            {
                Heal(-dmg);
                return;
            }
            HP -= dmg;
            if (!(this is Player))
                lastMobTakingDamage = this;
        }
        public void Heal(int heal)
        {
            if (heal < 0)
            {
                Hit(-heal);
                return;
            }
            HP += heal;
        }
        public virtual float GetHPRatio()
        {
            return (float)HP / InitialHP;
        }
        
        public override void Draw(Graphics g)
        {
            base.Draw(g);
            graphics.DrawHealthBar(g, this);
        }

        public virtual void Tick_Mob()
        {
            if (!DespawningOrDestroyed)
            {
                if (HP <= 0)
                    Dispose();
            }
        }

        public override void Term()
        {
            if (lastMobTakingDamage == this)
                lastMobTakingDamage = null;

            base.Term();
        }
        public override void Dispose()
        {
            lootSetup?.GenerateLoot(game, Coords.X, Coords.Y);

            base.Dispose();
        }

        public static Mob lastMobTakingDamage;

        public override void IntersectWith(Entity e)
        {
            if (e is Mob m && this.EntType != m.EntType)
            {
                m.Hit(this.DMG);
            }

            base.IntersectWith(e);
        }
    }
}
