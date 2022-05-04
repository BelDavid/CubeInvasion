using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class LootSetup
    {
        public LootSetup(Util.delegate_retHealingItem_paramsGameCoordinates del_HealingItem,
            double chance_HealingItem, double chance_AmmoPack, int weapons_avaiable, int[] weapons, int[,] chances_Ammo)
        {
            this.Del_HealingItem = del_HealingItem;

            this.chance_HealingItem = chance_HealingItem;
            this.chance_AmmoPack = chance_AmmoPack;

            this.weapons_avaiable = weapons_avaiable = Math.Min((int)WeaponType.COUNT, weapons_avaiable);

            if (weapons == null)
            {
                weapons = new int[weapons_avaiable];
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i] = 1;
            }
            this.weapons = weapons;

            for (int i = 0; i < weapons_avaiable; i++)
                this.weapons_SUM += weapons[i];
            if (this.weapons.Length > weapons_avaiable)
                for (int i = weapons_avaiable; i < weapons.Length; i++)
                    this.weapons[i] = 0;

            this.chances_Ammo = chances_Ammo;
        }

        private readonly double chance_HealingItem, chance_AmmoPack;
        private readonly int weapons_avaiable, weapons_SUM;
        private readonly int[] weapons;
        private readonly int[,] chances_Ammo;
        public Util.delegate_retHealingItem_paramsGameCoordinates Del_HealingItem { get; private set; }
        
        public bool GenerateLoot(Game game, int posX, int posY)
        {
            double n = Util.rand.NextDouble();

            if (n < chance_HealingItem)
            {
                game.AddToSpawnEntity(Del_HealingItem(game, new GFCoordinates(posX, posY)));
                return true;
            }
            else if (n < chance_HealingItem + chance_AmmoPack)
            {
                int m = Util.rand.Next(weapons_SUM);
                int o = 0;
                int weapon;
                
                for (weapon = 0; weapon < weapons_avaiable; weapon++)
                {
                    o += weapons[weapon];
                    if (o > m)
                        break;
                }

                int ammoAmmount = chances_Ammo[weapon, 0] + Util.rand.Next(chances_Ammo[weapon, 1] + 1);
                
                game.AddToSpawnEntity(new AmmoPack(Entity.NewID, game, new GFCoordinates(posX, posY), (WeaponType)weapon, ammoAmmount));
                return true;
            }

            return false;
        }
    }
}
