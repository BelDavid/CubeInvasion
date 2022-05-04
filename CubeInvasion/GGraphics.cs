using CubeInvasion.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class GGraphics
    {
        static GGraphics()
        {
            for (int i = 0; i < 256; i++)
                BrushFromHealthRatio[i] = new SolidBrush(Color.FromArgb(255, i, 0));
            for (int i = 0; i < 256; i++)
                BrushFromHealthRatio[256 + i] = new SolidBrush(Color.FromArgb(255 - i, 255, 0));
        }

        public GGraphics(int animationSpeed, params Image[] images)
        {
            this.animationSpeed = animationSpeed;
            this.images = images;

            if (animationSpeed < 1)
                animationSpeed = 1;
        }
        public GGraphics(Image image) : this(1, new Image[] { image })
        {

        }


        private int animationSpeed;
        private Image[] images;

        public void Render(Graphics g, int x, int y, int width, int height)
        {
            g.DrawImage(images[(animationState / animationSpeed) % images.Length], x, y, width, height);
        }
        public void RenderInGF(Graphics g, int posX, int posY)
        {
            g.DrawImage(images[(animationState / animationSpeed) % images.Length], posX * Util.gameField_Tile_Width, posY * Util.gameField_Tile_Height,
                Util.gameField_Tile_Width, Util.gameField_Tile_Height);
        }

        public void DrawHealthBar(Graphics g, Mob m)
        {
            float healthRatio = m.GetHPRatio();

            if (healthRatio >= 1 || healthRatio <= 0)
                return;

            int hR = (int)Math.Round(healthRatio * 512);


            if (m is MultiMob mb)
            {
                g.FillRectangle(BrushFromHealthRatio[hR], (mb.Coords.X - mb.CenterOffset.X) * Util.gameField_Tile_Width, (m.Coords.Y - mb.CenterOffset.Y) * Util.gameField_Tile_Height,
                                            mb.GetHPRatio() * mb.OccupiedSpace.GetLength(1) * Util.gameField_Tile_Width, Util.healthBar_thickness);
            }
            else
                g.FillRectangle(BrushFromHealthRatio[hR], m.Coords.X * Util.gameField_Tile_Width, m.Coords.Y * Util.gameField_Tile_Height,
                                            m.GetHPRatio() * Util.gameField_Tile_Width, Util.healthBar_thickness);
        }

        public Image GetImage(int index)
        {
            if (index >= 0)
                return images[index % images.Length];
            return null;
        }
        
        // STATIC
        private static int animationState = 0;
        public static void UpdateGraphics()
        {
            animationState++;
        }
        
        public static readonly Brush[] BrushFromHealthRatio = new Brush[512];

        // Items
        public static readonly GGraphics item_heart = new GGraphics(Resources.sprite_item_heart);
        public static readonly GGraphics item_heart_2 = new GGraphics(Resources.sprite_item_heart_2);
        public static readonly GGraphics item_ammopack = new GGraphics(Resources.sprite_item_ammopack);
        public static readonly GGraphics item_weaponpack = new GGraphics(Resources.sprite_item_weaponpack);

        public static readonly GGraphics item_note = new GGraphics(Resources.sprite_item_note);

        // Bullets
        public static readonly GGraphics[] projectile_arrow = new GGraphics[] {
                                new GGraphics(RotateImage(Resources.sprite_projectile_arrow_left, 1)),
                                new GGraphics(RotateImage(Resources.sprite_projectile_arrow_left, 2)),
                                new GGraphics(RotateImage(Resources.sprite_projectile_arrow_left, 3)),
                                new GGraphics(Resources.sprite_projectile_arrow_left) };
        public static readonly GGraphics[] projectile_exploArrow = new GGraphics[] {
                                new GGraphics(RotateImage(Resources.sprite_projectile_exploArrow_left, 1)),
                                new GGraphics(RotateImage(Resources.sprite_projectile_exploArrow_left, 2)),
                                new GGraphics(RotateImage(Resources.sprite_projectile_exploArrow_left, 3)),
                                new GGraphics(Resources.sprite_projectile_exploArrow_left) };

        // Explosives
        public static readonly GGraphics explosive_bomb = new GGraphics(Resources.sprite_explosive_bomb);
        public static readonly GGraphics explosive_mine = new GGraphics(Resources.sprite_explosive_mine);

        // Mobs
        public static readonly GGraphics mob_player = new GGraphics(Resources.sprite_player);

        public static readonly GGraphics mob_brown = new GGraphics(Resources.sprite_mob_brown);
        public static readonly GGraphics mob_red = new GGraphics(Resources.sprite_mob_red);
        public static readonly GGraphics mob_orange = new GGraphics(Resources.sprite_mob_orange);
        public static readonly GGraphics mob_blue = new GGraphics(Resources.sprite_mob_blue);
        public static readonly GGraphics mob_green = new GGraphics(Resources.sprite_mob_green);
        public static readonly GGraphics mob_purple = new GGraphics(Resources.sprite_mob_purple);

        public static readonly GGraphics multimob_brown = new GGraphics(Resources.sprite_multimob_brown);
        public static readonly GGraphics multimob_red = new GGraphics(Resources.sprite_multimob_red);
        public static readonly GGraphics multimob_orange = new GGraphics(Resources.sprite_multimob_orange);
        public static readonly GGraphics multimob_green = new GGraphics(Resources.sprite_multimob_green);
        public static readonly GGraphics multimob_blue = new GGraphics(Resources.sprite_multimob_blue);
        public static readonly GGraphics multimob_purple = new GGraphics(Resources.sprite_multimob_purple);

        // Actions
        public static readonly GGraphics action_explosion_center = new GGraphics(Resources.sprite_action_explosion_center);
        public static readonly GGraphics action_explosion = new GGraphics(5, Resources.sprite_action_explosion_0, Resources.sprite_action_explosion_1);
        
        public static readonly GGraphics action_laserBeam_horizontal = new GGraphics(Resources.sprite_action_laserBeam_horizontal);
        public static readonly GGraphics action_laserBeam_vertical = new GGraphics(Resources.sprite_action_laserBeam_vertical);

        public static readonly GGraphics graphicalAction_portal = new GGraphics(6, Resources.sprite_graphicalAction_portal_0,
                                                                                    Resources.sprite_graphicalAction_portal_1,
                                                                                    Resources.sprite_graphicalAction_portal_2,
                                                                                    Resources.sprite_graphicalAction_portal_3);


        /// <summary>
        /// Rotates image.
        /// If (rotationIndex mod 4) == 0 => returns original image
        /// </summary>
        /// <param name="img">image to rotate</param>
        /// <param name="rotationAngle">rotate by (rotationIndex * 90) degrees</param>
        /// <returns>rotated image</returns>
        public static Image RotateImage(Image img, int rotationIndex)
        {
            rotationIndex = (int)(rotationIndex - 4 * Math.Floor((decimal)rotationIndex / 4));
            if (rotationIndex == 0)
                return img;

            if (rotationIndex == 1)
            {
                Bitmap bmp_origin = (Bitmap)img;
                Bitmap bmp_target = new Bitmap(img.Height, img.Width);
                //bmp_target.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                for (int y = 0; y < bmp_target.Height; y++)
                {
                    for (int x = 0; x < bmp_target.Width; x++)
                    {
                        bmp_target.SetPixel(x, y, bmp_origin.GetPixel(y, bmp_origin.Height - x - 1));
                    }
                }

                return bmp_target;
            }
            else if (rotationIndex == 2)
            {
                Bitmap bmp_origin = (Bitmap)img;
                Bitmap bmp_target = new Bitmap(img.Width, img.Height);
                //bmp_target.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                for (int y = 0; y < bmp_target.Height; y++)
                {
                    for (int x = 0; x < bmp_target.Width; x++)
                    {
                        bmp_target.SetPixel(x, y, bmp_origin.GetPixel(bmp_origin.Width - x - 1, bmp_origin.Height - y - 1));
                    }
                }

                return bmp_target;
            }
            else if (rotationIndex == 3)
            {
                Bitmap bmp_origin = (Bitmap)img;
                Bitmap bmp_target = new Bitmap(img.Height, img.Width);
                //bmp_target.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                for (int y = 0; y < bmp_target.Height; y++)
                {
                    for (int x = 0; x < bmp_target.Width; x++)
                    {
                        bmp_target.SetPixel(x, y, bmp_origin.GetPixel(bmp_origin.Width - y - 1, x));
                    }
                }

                return bmp_target;
            }
            return img; // will not be reached (hopefully :D)
        }
    }
}
