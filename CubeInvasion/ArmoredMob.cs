using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    abstract class ArmoredMob : Mob, IProjectileTick
    {
        public ArmoredMob(string ID, Game game, EntityType entType, GFCoordinates coords, GGraphics graphics, int hp, int dmg, LootSetup lootSetup): 
            base(ID, game, entType, coords, graphics, hp, dmg, lootSetup)
        {
            
        }

        public WeaponType ActiveWeapon { get; protected set; } = WeaponType.NONE;
        public bool[] owningWeapons = new bool[(int)WeaponType.COUNT];
        protected int[] weaponAmmo = new int[(int)WeaponType.COUNT];

        public void Fire(Direction dir)
        {
            if (sDProgress == 0 && dir != Direction.NONE && ActiveWeapon != WeaponType.NONE)
            {
                Util.SetDXDYByDir(dir, out int dX, out int dY);

                GFCoordinates coords = new GFCoordinates(this.Coords.X +dX, this.Coords.Y + dY);
                
                if (weaponAmmo[(int)ActiveWeapon] > 0)
                {
                    switch (ActiveWeapon)
                    {
                        case WeaponType.Bow:
                            if (dir != Direction.CENTER)
                                game.AddToSpawnEntity(new Arrow(Entity.NewID, game, EntType, coords, dir));
                            break;
                        case WeaponType.Mine:
                            game.AddToSpawnEntity(new Mine(Entity.NewID, game, EntType, coords));
                            break;
                        case WeaponType.ExploBow:
                            if (dir != Direction.CENTER)
                                game.AddToSpawnEntity(new ExploArrow(Entity.NewID, game, EntType, coords, dir));
                            break;
                        case WeaponType.Bomb:
                            game.AddToSpawnEntity(new Bomb(Entity.NewID, game, EntType, coords));
                            break;
                        case WeaponType.Laser:
                            if (dir != Direction.CENTER)
                                game.SpawnAction(new LaserBeam(Action.NewID, game, coords, dir));
                            break;
                    }
                    weaponAmmo[(int)ActiveWeapon] -= 1;
                    sDProgress = Util.shootingDelay;
                }
            }
        }

        public void GiveWeapon(WeaponType wt)
        {
            if (Util.IsValidWeapon(wt))
            {
                owningWeapons[(int)wt] = true;
                if (ActiveWeapon == WeaponType.NONE)
                    SwitchWeapon(wt);
            }
        }
        public void SwitchWeapon(bool up)
        {
            bool hasAnyWeapons = false;
            for (int i = 0; i < owningWeapons.Length; i++)
            {
                if (owningWeapons[i])
                {
                    hasAnyWeapons = true;
                    break;
                }
            }

            if (hasAnyWeapons)
            {
                if (up)
                    do
                    {
                        ActiveWeapon++;

                        if (ActiveWeapon >= WeaponType.COUNT)
                            ActiveWeapon = 0;

                    } while (!owningWeapons[(int)ActiveWeapon]);
                else
                    do
                    {
                        ActiveWeapon--;

                        if (ActiveWeapon < 0)
                            ActiveWeapon = WeaponType.COUNT - 1;

                    } while (!owningWeapons[(int)ActiveWeapon]);
            }
            else
                ActiveWeapon = WeaponType.NONE;
        }
        public void SwitchWeapon(WeaponType wt)
        {
            if (Util.IsValidWeapon(wt))
                if (owningWeapons[(int)wt])
                    ActiveWeapon = wt;
        }

        public void AddAmmo(WeaponType wt, int amount)
        {
            if (Util.IsValidWeapon(wt))
            {
                weaponAmmo[(int)wt] += amount;
                if (weaponAmmo[(int)wt] > Util.maxAmmo)
                    weaponAmmo[(int)wt] = Util.maxAmmo;
            }
        }
        public int GetAmmo(WeaponType wt)
        {
            if (Util.IsValidWeapon(wt))
                return weaponAmmo[(int)wt];
            return 0;
        }


        protected int sDProgress = 0;

        public void Tick_Projectile()
        {
            if (sDProgress > 0)
                sDProgress -= 1;
        }

        public override void IntersectWith(Entity e)
        {
            if (e is AmmoPack ap && this is Player)
            {
                game.WriteToLog(string.Format("You've collected {0} of {1} ammo", ap.Amount, ap.Wt.ToString()), Color.Blue);

                AddAmmo(ap.Wt, ap.Amount);
                ap.Dispose();
            }
            base.IntersectWith(e);
        }
    }
}
