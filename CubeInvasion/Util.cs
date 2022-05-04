using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    static class Util
    {
        static Util()
        {
            //Create rings
            for (int i = 0; i < rings.Length; i++)
            {
                int ii = i + 1;
                rings[i] = new int[ii * 8, 2];

                int n = 0;

                for (int j = -ii; j <= ii; j++)
                {
                    rings[i][n, 0] = j;
                    rings[i][n, 1] = -ii;
                    n++;
                }
                for (int j = -i; j <= i; j++)
                {
                    rings[i][n, 0] = ii;
                    rings[i][n, 1] = j;
                    n++;
                }
                for (int j = ii; j >= -ii; j--)
                {
                    rings[i][n, 0] = j;
                    rings[i][n, 1] = ii;
                    n++;
                }
                for (int j = i; j >= -i; j--)
                {
                    rings[i][n, 0] = -ii;
                    rings[i][n, 1] = j;
                    n++;
                }
            }
        }

        // Rings
        public static int[][,] rings = new int[50][,];

        //Random
        public static readonly Random rand = new Random();
        public static GFCoordinates GetRandCoordsInGF(int gfLayer = 0) 
            => new GFCoordinates(rand.Next(gameField_Width), rand.Next(gameField_Height), gfLayer, true);

        // Dir to dX, dY
        public static void SetDXDYByDir(Direction dir, out int dX, out int dY)
        {
            dX = ((dir & Direction.RIGHT) != 0 ? 1 : 0) - ((dir & Direction.LEFT) != 0 ? 1 : 0);
            dY = ((dir & Direction.DOWN) != 0 ? 1 : 0) - ((dir & Direction.UP) != 0 ? 1 : 0);
        }

        public static bool IsValidWeapon(WeaponType weaponType)
        {
            if (weaponType != WeaponType.NONE && (int)weaponType < (int)WeaponType.COUNT)
                return true;

            return false;
        }

        //Const
        public static readonly bool
            cheats = false;

        public const double
            //Ticks
            ticksPerSecond = 30,
            ticksPerSecond_mobs = 3, 
            ticksPerSecond_bullets = 25,
            ticksPerSecond_actions = 20,
            ticksPerSecond_player = 8;


        public const int
            //Game
            ticksSkipingLimit = 5,
            spawnCircleRadius = 35,
            numberOfSpawnAttemptsPerCycle = 5,
            playerControlFrequency = 3,

            healthBar_thickness = 2,

            //GameField
            gfLayerMob = 0,
            gfLayerBullet = 1,
            gfLayerExplosive = 2,
            gfLayerItem = 3,
            gfLayersCount = gfLayerItem + 1,

            //Player
            saveSafeSpawnRing = 10,
            
            maxAmmo = 999,
            shootingDelay = 5,
            
            // Weapons
            arrow_DMG = 2,

            mine_DMG = 5,
            mine_Intensity = 3,

            exploArrow_DMG = 4,
            exploArrow_Intensity = 1,

            bomb_DMG = 7,
            bomb_Intensity = 4,
            bomb_Countdown = 9,

            laser_Beam_DMG = 10,
            laser_Beam_Speed = 6,
            laser_Beam_Length = 30,

            //GameField
            gameField_Width = 35,
            gameField_Height = 20;

        public static int
            //GameField
            gameField_Tile_Width,
            gameField_Tile_Height;

        public const string
            // Folders
            path_folder_data = "data/",
            path_folder_images = path_folder_data + "images/",
            
            //Files
            path_file_savedData = path_folder_data + "data" + file_extension,
            
            //Extension
            file_extension = ".cid";

        //Delegates
        public delegate void delegate_returnVoid_NoParameters();

        public delegate HealingItem delegate_retHealingItem_paramsGameCoordinates(Game game, GFCoordinates coords);

    }
}
