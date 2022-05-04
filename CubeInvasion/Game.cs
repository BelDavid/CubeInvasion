using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubeInvasion
{
    [Serializable]
    abstract class Game
    {
        public Game(Engine engine, string gameName)
        {
            this.engine = engine;
            this.GameName = gameName;
        }

        #region Game control
        public virtual void Init()
        {
            entities = new Dictionary<string, Entity>();
            entitiesToSpawn = new List<Entity>();
            entitiesToDespawn = new List<Entity>();

            mobs = new List<IMobTick>();
            projectiles = new List<IProjectileTick>();
            actions = new List<Action>();

            gameField = new GameField(new Entity[Util.gameField_Width, Util.gameField_Height, Util.gfLayersCount]);

            Load();

            player = new Player(this, new GFCoordinates(Util.gameField_Width / 3, Util.gameField_Height / 2), Player_initialHP, Player_DMG);

            for (int i = 0; i < Player_initialWeapons.Length; i++)
                if (Player_initialWeapons[i])
                    player.GiveWeapon((WeaponType)i);

            for (int i = 0; i < Player_initialAmmos.Length; i++)
                player.AddAmmo((WeaponType)i, Player_initialAmmos[i]);

            SpawnEntity(player);

            Inited = true;
        }
        protected virtual void Load()
        {

        }
        public virtual void Restore(Engine engine)
        {
            this.engine = engine;

            // Restore Entities
            foreach (var e in entities.Values)
                e.Restore(this);
            // Restore Actions
            foreach (var a in actions)
                a.Restore(this);
        }

        protected void Terminate(bool gameWon_NotLost)
        {
            GameTerminated = true;

            GameWon = gameWon_NotLost;
            GameOver = !gameWon_NotLost;

            engine.Pause();
        }
        public virtual void Reset()
        {
            for (int x = 0; x < Util.gameField_Tile_Width; x++)
                for (int y = 0; y < Util.gameField_Tile_Height; y++)
                    for (int z = 0; z < Util.gfLayersCount; z++)
                        gameField[x, y, z] = null;

            mobs.Clear();
            projectiles.Clear();

            UpdateSpawnAndDespawn();

            foreach(Entity e in entities.Values)
            {
                if (e is MultiMob mb)
                {
                    for (int y = 0; y < mb.OccupiedSpace.GetLength(1); y++)
                        for (int x = 0; x < mb.OccupiedSpace.GetLength(0); x++)
                            if (mb.OccupiedSpace[x, y] == 1)
                                gameField[mb.Coords.X - mb.CenterOffset.X + x, mb.Coords.Y - mb.CenterOffset.Y + y, mb.GfLayer] = e;
                }
                else
                    gameField[e.Coords.X, e.Coords.Y, e.GfLayer] = e;


                if (e is IMobTick)
                    mobs.Add((IMobTick)e);
                if (e is IProjectileTick)
                    projectiles.Add((IProjectileTick)e);
            }
        }
        #endregion

        #region TICKS
        public virtual void Tick()
        {
            if (GameTerminated)
                return;

            if (Util.cheats) // Cheats
            {
                if (engine.input.isKeyDisposablePressed(Keys.NumPad1))
                    player.Heal(10);
                if (engine.input.isKeyDisposablePressed(Keys.NumPad2))
                    for (int i = 0; i < (int)WeaponType.COUNT; i++)
                        player.AddAmmo((WeaponType)i, 50);
                if (engine.input.isKeyDisposablePressed(Keys.NumPad3))
                    for (int i = 0; i < (int)WeaponType.COUNT; i++)
                        player.GiveWeapon((WeaponType)i);
                if (engine.input.isKeyDisposablePressed((Keys)0x6B))
                    Counter += 1;
                if (engine.input.isKeyDisposablePressed((Keys)0x6D))
                    Counter -= 1;
            }

            UpdateSpawnAndDespawn();

            if (player.HP <= 0)
                Terminate(false);

            // checkLAG
            if (engine.Check_LAG > 1 && backgroundImage != null)
            {
                backgroundImage = null;
                WriteToDebugLog("Background image removed due to low performance", Color.Red, true);
            }
        }
        public virtual void Tick_Mobs()
        {
            if (GameTerminated)
                return;

            // Mob tick
            foreach (var mob in mobs)
                mob.Tick_Mob();
        }
        public virtual void Tick_Projectiles()
        {
            if (GameTerminated)
                return;

            // Projectiles tick
            foreach (var projectile in projectiles)
            {
                projectile.Tick_Projectile();
            }
        }
        public virtual void Tick_Actions()
        {
            if (GameTerminated)
                return;

            // Actions
            for (int i = 0; i < actions.Count; i++)
            {
                if (!actions[i].Finalized)
                {
                    actions[i].Tick_Action();
                }
                else
                {
                    actions[i].Term();
                    actions.RemoveAt(i);
                    i--;
                }
            }
        }
        public virtual void Tick_Player()
        {
            if (GameTerminated)
                return;

            // Player control
            if (engine.input.isKeyDisposablePressedOrJustPressed(Keys.W))
                MoveEntity(player, 0, -1);
            if (engine.input.isKeyDisposablePressedOrJustPressed(Keys.A))
                MoveEntity(player, -1, 0);
            if (engine.input.isKeyDisposablePressedOrJustPressed(Keys.S))
                MoveEntity(player, 0, 1);
            if (engine.input.isKeyDisposablePressedOrJustPressed(Keys.D))
                MoveEntity(player, 1, 0);

            if (engine.input.isKeyDisposablePressedOrJustPressed(Keys.Up))
                player.Fire(Direction.UP);
            if (engine.input.isKeyDisposablePressedOrJustPressed(Keys.Right))
                player.Fire(Direction.RIGHT);
            if (engine.input.isKeyDisposablePressedOrJustPressed(Keys.Down))
                player.Fire(Direction.DOWN);
            if (engine.input.isKeyDisposablePressedOrJustPressed(Keys.Left))
                player.Fire(Direction.LEFT);

            if (engine.input.isKeyDisposablePressed(Keys.Q))
                player.SwitchWeapon(false);
            if (engine.input.isKeyDisposablePressed(Keys.E))
                player.SwitchWeapon(true);

            if (engine.input.isKeyDisposablePressed(Keys.D1))
                player.SwitchWeapon((WeaponType)0);
            if (engine.input.isKeyDisposablePressed(Keys.D2))
                player.SwitchWeapon((WeaponType)1);
            if (engine.input.isKeyDisposablePressed(Keys.D3))
                player.SwitchWeapon((WeaponType)2);
            if (engine.input.isKeyDisposablePressed(Keys.D4))
                player.SwitchWeapon((WeaponType)3);
            if (engine.input.isKeyDisposablePressed(Keys.D5))
                player.SwitchWeapon((WeaponType)4);
            if (engine.input.isKeyDisposablePressed(Keys.D6))
                player.SwitchWeapon((WeaponType)5);
            if (engine.input.isKeyDisposablePressed(Keys.D7))
                player.SwitchWeapon((WeaponType)6);
        }
        #endregion
        
        #region ENTITIES
        private void UpdateSpawnAndDespawn()
        {
            //Spawn entities
            for (int i = 0; i < entitiesToSpawn.Count; i++)
                if (SpawnEntity(entitiesToSpawn[i]))
                {
                    entitiesToSpawn.RemoveAt(i);
                    i--;
                }
            //Despawn entities
            for (int i = 0; i < entitiesToDespawn.Count; i++)
                if (DespawnEntity(entitiesToDespawn[i]))
                {
                    entitiesToDespawn.RemoveAt(i);
                    i--;
                }
        }

        protected bool SpawnEntity(Entity e)
        {
            if (e == null || entities.ContainsKey(e.ID) || e.DespawningOrDestroyed)
                return true;

            if (e is IHostileMob && e.Coords.GeneratedRandomly && player != null)
                for (int i = 0; i < Util.numberOfSpawnAttemptsPerCycle; i++)
                {
                    if (Math.Sqrt(Math.Pow(player.Coords.X - e.Coords.X, 2) + Math.Pow(player.Coords.Y - e.Coords.Y, 2)) < Util.saveSafeSpawnRing)
                        e.Coords = Util.GetRandCoordsInGF();
                    else
                        break;

                    if (i == Util.numberOfSpawnAttemptsPerCycle)
                        return false;
                }

            if (e is MultiMob mb)
            {
                if (mb.Coords.X - mb.CenterOffset.X < 0)
                    mb.Coords.X = mb.CenterOffset.X;
                else if (mb.Coords.X - mb.CenterOffset.X + mb.OccupiedSpace.GetLength(0) >= Util.gameField_Width)
                    mb.Coords.X = Util.gameField_Width - mb.OccupiedSpace.GetLength(0) + mb.CenterOffset.X;
                if (mb.Coords.Y - mb.CenterOffset.Y < 0)
                    mb.Coords.Y = mb.CenterOffset.Y;
                else if (mb.Coords.Y - mb.CenterOffset.Y + mb.OccupiedSpace.GetLength(1) >= Util.gameField_Height)
                    mb.Coords.Y = Util.gameField_Height - mb.OccupiedSpace.GetLength(1) + mb.CenterOffset.Y;

                bool ok = Fits(mb, 0, 0);

                //for (int y = 0; y < mb.OccupiedSpace.GetLength(1); y++)
                //    for (int x = 0; x < mb.OccupiedSpace.GetLength(0); x++)
                //        if (gameField[mb.Coords.X - mb.CenterOffset.X + x, mb.Coords.Y - mb.CenterOffset.Y + y, e.GfLayer] != null)
                //            ok = false;

                if (!ok)
                {
                    int dX = 0, dY = 0;

                    for (int i = 0; i < Util.spawnCircleRadius && !ok; i++)
                        for (int n = 0; n < Util.rings[i].GetLength(0) && !ok; n++)
                        {
                            dX = Util.rings[i][n, 0];
                            dY = Util.rings[i][n, 1];

                            if (Fits(mb, dX, dY))
                            {
                                ok = true;

                                e.Coords.X += dX;
                                e.Coords.Y += dY;
                            }
                        }

                    if (!ok)
                        return false;
                }

                for (int y = 0; y < mb.OccupiedSpace.GetLength(1); y++)
                    for (int x = 0; x < mb.OccupiedSpace.GetLength(0); x++)
                        if (mb.OccupiedSpace[x, y] > 0)
                        {
                            Entity e0;
                            for (int i = 0; i < Util.gfLayersCount; i++)
                                if ((e0 = gameField[e.Coords.X, e.Coords.Y, i]) != null)
                                    e.IntersectWith(e0);

                            gameField[mb.Coords.X - mb.CenterOffset.X + x, mb.Coords.Y - mb.CenterOffset.Y + y, e.GfLayer] = mb;
                        }
            }
            else
            {
                if (e.Coords.X < 0)
                    e.Coords.X = 0;
                else if (e.Coords.X >= Util.gameField_Width)
                    e.Coords.X = Util.gameField_Width - 1;
                if (e.Coords.Y < 0)
                    e.Coords.Y = 0;
                else if (e.Coords.Y >= Util.gameField_Height)
                    e.Coords.Y = Util.gameField_Height - 1;

                if (gameField[e.Coords.X, e.Coords.Y, e.GfLayer] != null)
                {
                    bool ok = false;
                    int dX = 0, dY = 0;

                    for (int i = 0; i < Util.spawnCircleRadius && !ok; i++)
                        for (int n = 0; n < Util.rings[i].GetLength(0) && !ok; n++)
                        {
                            dX = Util.rings[i][n, 0];
                            dY = Util.rings[i][n, 1];

                            if (Fits(e, dX, dY))
                            {
                                ok = true;

                                e.Coords.X += dX;
                                e.Coords.Y += dY;
                            }
                        }
                    if (!ok)
                        return false;
                }

                Entity e0;
                for (int i = 0; i < Util.gfLayersCount; i++)
                    if ((e0 = gameField[e.Coords.X, e.Coords.Y, i]) != null)
                        e.IntersectWith(e0);

                if (e.GfLayer >= 0)
                    gameField[e.Coords.X, e.Coords.Y, e.GfLayer] = e;
            }

            if (e is IMobTick)
                mobs.Add((IMobTick)e);
            if (e is IProjectileTick)
                projectiles.Add((IProjectileTick)e);

            if (e is IHostileMob)
                Counter_HostileMobs++;

            entities.Add(e.ID, e);

            return true;
        }
        protected bool DespawnEntity(Entity e)
        {
            if (e == null || e.Destroyed)
                return true;

            if (entities.ContainsKey(e.ID))
            {
                entities.Remove(e.ID);

                if (e is IMobTick)
                {
                    mobs.Remove((IMobTick)e);
                }
                else if (e is IProjectileTick)
                {
                    projectiles.Remove((IProjectileTick)e);
                }
                if (e is MultiMob mb)
                {
                    for (int y = 0; y < mb.OccupiedSpace.GetLength(1); y++)
                        for (int x = 0; x < mb.OccupiedSpace.GetLength(0); x++)
                            if (mb.OccupiedSpace[x, y] > 0)
                            {
                                gameField[mb.Coords.X - mb.CenterOffset.X + x, mb.Coords.Y - mb.CenterOffset.Y + y, mb.GfLayer] = null;
                            }
                }
                else
                {
                    gameField[e.Coords.X, e.Coords.Y, e.GfLayer] = null;
                }
            }
            e.Term();

            if (e is IHostileMob)
                Counter_HostileMobs--;

            EntityDespawned(e);

            return true;
        }
        public void AddToSpawnEntity(Entity e) => entitiesToSpawn.Add(e);
        public void AddToDespawnEntity(Entity e) => entitiesToDespawn.Add(e);
        public void CreateAndAddToSpawnHostileMob(string mobType, string data)
        {
            try
            {
                string[] mobData = data.Split(';');

                if (mobData.Length != 8)
                    throw new ArgumentException("There shall be 8 arguments");

                // HP & DMG
                if (!int.TryParse(mobData[2], out int hp) || hp <= 0)
                    throw new FormatException(mobData[2] + " is not valid number for HP");
                if (!int.TryParse(mobData[3], out int dmg) || dmg < 0)
                    throw new FormatException(mobData[3] + " is not valid number for DMG");

                // Loot chance
                if (!double.TryParse(mobData[4], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out double chance_HealingItem)
                    || chance_HealingItem < 0)
                    throw new FormatException(mobData[4] + " is not valid number for chance_HealingItem");
                if (!double.TryParse(mobData[5], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out double chance_AmmoPack)
                    || chance_AmmoPack < 0)
                    throw new FormatException(mobData[5] + " is not valid number for chance_AmmoPack");

                // drop weapon chances
                string[] weapons_Chances_STR = mobData[6].Split(',');
                int[] weapons_Chances = new int[Math.Min(weapons_Chances_STR.Length, (int)WeaponType.COUNT)];
                for (int j = 0; j < weapons_Chances.Length; j++)
                    if (!int.TryParse(weapons_Chances_STR[j], out weapons_Chances[j]) || weapons_Chances[j] < 0)
                        throw new FormatException(weapons_Chances_STR[j] + " is not valid number for weapon(" + (WeaponType)j + ") chance");

                // drop ammo chances
                string[] ammo_chances_STR = mobData[7].Split(',');
                int[,] ammo_chances = new int[Math.Min(ammo_chances_STR.Length, (int)WeaponType.COUNT), 2];
                for (int j = 0; j < ammo_chances.GetLength(0); j++)
                {
                    string[] values = ammo_chances_STR[j].Split('-');

                    if (values.Length > 2)
                        throw new FormatException(mobData[7] + " is not valid range for ammo(" + (WeaponType)j + ") chance");

                    if (!int.TryParse(values[0], out ammo_chances[j, 0]) || ammo_chances[j, 0] < 0)
                        throw new FormatException(values[0] + " is not valid number for ammo(" + (WeaponType)j + ") chance");

                    if (values.Length > 1)
                    {
                        if (!int.TryParse(values[1], out int upperLimit))
                            throw new FormatException(values[1] + " is not valid number for ammo(" + (WeaponType)j + ") chance");
                        else if (upperLimit < ammo_chances[j, 0])
                            throw new FormatException(ammo_chances[j, 1] + " can not be smaller than " + ammo_chances[j, 0] + " in range (" + ammo_chances_STR[j] + ')');

                        ammo_chances[j, 1] = upperLimit - ammo_chances[j, 0];
                    }
                    else
                        ammo_chances[j, 1] = 0;
                }

                GGraphics gGraphics;

                if (mobType == "multimob")
                {
                    switch (mobData[1])
                    {
                        case "brown":
                            gGraphics = GGraphics.multimob_brown;
                            break;
                        case "red":
                            gGraphics = GGraphics.multimob_red;
                            break;
                        case "orange":
                            gGraphics = GGraphics.multimob_orange;
                            break;
                        case "green":
                            gGraphics = GGraphics.multimob_green;
                            break;
                        case "blue":
                            gGraphics = GGraphics.multimob_blue;
                            break;
                        case "purple":
                            gGraphics = GGraphics.multimob_purple;
                            break;
                        default:
                            throw new ArgumentException(mobData[1] + " is not valid color");
                    }

                    byte[,] occupiedSpace;
                    switch (mobData[0])
                    {
                        case "4t":
                            occupiedSpace = Game_NotesCollector.occupiedSpace_4T;
                            break;
                        default:
                            string[] values = mobData[0].Split('x');
                            if (values.Length != 2 || !byte.TryParse(values[0], out byte width) || !byte.TryParse(values[1], out byte height) || width <= 0 || height <= 0)
                                throw new FormatException(mobData[0] + " is not valid size");

                            occupiedSpace = new byte[width, height];
                            for (int x = 0; x < width; x++)
                                for (int y = 0; y < height; y++)
                                    occupiedSpace[x, y] = 1;
                            break;
                    }

                    LootSetup lootSetup = new LootSetup(Item_Heart_2, chance_HealingItem, chance_AmmoPack,
                        weapons_Chances.Length, weapons_Chances, ammo_chances);

                    AddToSpawnEntity(new MultiMob(Entity.NewID, this, Util.GetRandCoordsInGF(), gGraphics, hp, dmg, occupiedSpace, lootSetup));
                }
                else
                {
                    // mob color
                    switch (mobData[1])
                    {
                        case "brown":
                            gGraphics = GGraphics.mob_brown;
                            break;
                        case "red":
                            gGraphics = GGraphics.mob_red;
                            break;
                        case "orange":
                            gGraphics = GGraphics.mob_orange;
                            break;
                        case "green":
                            gGraphics = GGraphics.mob_green;
                            break;
                        case "blue":
                            gGraphics = GGraphics.mob_blue;
                            break;
                        case "purple":
                            gGraphics = GGraphics.mob_purple;
                            break;
                        default:
                            throw new ArgumentException(mobData[1] + " is not valid color");
                    }

                    LootSetup lootSetup = new LootSetup(Item_Heart, chance_HealingItem, chance_AmmoPack,
                        weapons_Chances.Length, weapons_Chances, ammo_chances);

                    if (!int.TryParse(mobData[0], out int count_orAmmoCount))
                        throw new FormatException(mobData[0] + " is not valid number for count");

                    if (mobType == "mob")
                        for (int j = 0; j < count_orAmmoCount; j++)
                            AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), gGraphics, hp, dmg, lootSetup));

                    else if (mobType == "bowshooter")
                        AddToSpawnEntity(new HostileMob_BowShooter(Entity.NewID, this, Util.GetRandCoordsInGF(), gGraphics, hp, dmg, count_orAmmoCount, lootSetup));
                    else if (mobType == "mineplanter")
                        AddToSpawnEntity(new HostileMob_MinePlanter(Entity.NewID, this, Util.GetRandCoordsInGF(), gGraphics, hp, dmg, count_orAmmoCount, lootSetup));
                }
            }
            catch (Exception ex)
            {
                engine.WriteToDebugLog("Error creating mob \"" + data + "\" - [" + ex.Message + "]", Color.Red, true);
            }
        }

        protected virtual void EntityDespawned(Entity e)
        {

        }

        public bool Fits(Entity e, int dX, int dY)
        {
            if (e is MultiMob mb)
            {
                if (gameField.IsInside(e.Coords.X - mb.CenterOffset.X + dX, mb.OccupiedSpace.GetLength(0),
                    e.Coords.Y - mb.CenterOffset.Y + dY, mb.OccupiedSpace.GetLength(1), e.GfLayer))
                {
                    int ddX = mb.Coords.X - mb.CenterOffset.X + dX,
                        ddY = mb.Coords.Y - mb.CenterOffset.Y + dY;

                    for (int y = 0; y < mb.OccupiedSpace.GetLength(1); y++)
                        for (int x = 0; x < mb.OccupiedSpace.GetLength(0); x++)
                            if (mb.OccupiedSpace[x, y] > 0
                                && gameField[ddX + x, ddY + y, e.GfLayer] != null
                                && gameField[ddX + x, ddY + y, e.GfLayer] != mb)
                                return false;

                    return true;
                }
            }
            else
            {
                if (gameField.IsInside(e.Coords.X + dX, e.Coords.Y + dY, e.GfLayer))
                    if (gameField[e.Coords.X + dX, e.Coords.Y + dY, e.GfLayer] == null)
                        return true;
            }
            return false;
        }
        public bool MoveEntity(Entity e, int dX, int dY)
        {
            Entity e0 = null;
            if (e is MultiMob mb)
            {
                if (mb.Coords.X - mb.CenterOffset.X + dX < 0)
                    dX = -(mb.Coords.X - mb.CenterOffset.X);
                else if (mb.Coords.X - mb.CenterOffset.X + mb.OccupiedSpace.GetLength(0) + dX > Util.gameField_Width)
                    dX = Util.gameField_Width - (mb.Coords.X - mb.CenterOffset.X + mb.OccupiedSpace.GetLength(0));
                if (mb.Coords.Y - mb.CenterOffset.Y + dY < 0)
                    dY = -(mb.Coords.Y - mb.CenterOffset.Y);
                else if (mb.Coords.Y - mb.CenterOffset.Y + mb.OccupiedSpace.GetLength(1) + dY > Util.gameField_Height)
                    dY = Util.gameField_Height - (mb.Coords.Y - mb.CenterOffset.Y + mb.OccupiedSpace.GetLength(1));

                if (dX == 0 && dY == 0)
                    return false;

                bool free = true;

                int ddX = mb.Coords.X - mb.CenterOffset.X + dX,
                    ddY = mb.Coords.Y - mb.CenterOffset.Y + dY;

                for (int y = 0; y < mb.OccupiedSpace.GetLength(1); y++)
                    for (int x = 0; x < mb.OccupiedSpace.GetLength(0); x++)
                        if (mb.OccupiedSpace[x, y] > 0)
                            if ((e0 = gameField[ddX + x, ddY + y, e.GfLayer]) != null && e0 != mb)
                            {
                                free = false;
                                mb.IntersectWith(e0);
                            }

                if (!free)
                    return false;

                for (int y = 0; y < mb.OccupiedSpace.GetLength(1); y++)
                    for (int x = 0; x < mb.OccupiedSpace.GetLength(0); x++)
                        if (mb.OccupiedSpace[x, y] > 0)
                        {
                            gameField[mb.Coords.X - mb.CenterOffset.X + x, mb.Coords.Y - mb.CenterOffset.Y + y, e.GfLayer] = null;
                        }

                mb.Coords.X += dX;
                mb.Coords.Y += dY;

                for (int y = 0; y < mb.OccupiedSpace.GetLength(1); y++)
                    for (int x = 0; x < mb.OccupiedSpace.GetLength(0); x++)
                        if (mb.OccupiedSpace[x, y] > 0)
                        {
                            gameField[mb.Coords.X - mb.CenterOffset.X + x, mb.Coords.Y - mb.CenterOffset.Y + y, e.GfLayer] = mb;

                            for (int j = 0; j < Util.gfLayersCount; j++)
                                if (((e0 = gameField[mb.Coords.X - mb.CenterOffset.X + x, mb.Coords.Y - mb.CenterOffset.Y + y, j]) != null) && j != mb.GfLayer)
                                    mb.IntersectWith(e0);
                        }

                return true;
            }
            else
            {
                if (e.Coords.X + dX < 0)
                    dX = -e.Coords.X;
                else if (e.Coords.X + dX >= Util.gameField_Width)
                    dX = Util.gameField_Width - 1 - e.Coords.X;
                if (e.Coords.Y + dY < 0)
                    dY = -e.Coords.Y;
                else if (e.Coords.Y + dY >= Util.gameField_Height)
                    dY = Util.gameField_Height - 1 - e.Coords.Y;

                if (dX == 0 && dY == 0)
                    return false;


                if (gameField[e.Coords.X + dX, e.Coords.Y + dY, e.GfLayer] == null)
                {
                    gameField[e.Coords.X + dX, e.Coords.Y + dY, e.GfLayer] = e;
                    gameField[e.Coords.X, e.Coords.Y, e.GfLayer] = null;

                    for (int j = 0; j < Util.gfLayersCount; j++)
                        if (((e0 = gameField[e.Coords.X + dX, e.Coords.Y + dY, j]) != null) && j != e.GfLayer)
                            e.IntersectWith(e0);

                    e.Coords.X += dX;
                    e.Coords.Y += dY;
                }
                else
                    e.IntersectWith(gameField[e.Coords.X + dX, e.Coords.Y + dY, e.GfLayer]);

                return true;
            }
        }
        public bool SwapPlaces(Entity e0, Entity e1)
        {
            if (e0 == null || e1 == null)
                return true;

            if (e0 is MultiMob || e1 is MultiMob)
                return false;

            GFCoordinates temp = e0.Coords;
            e0.Coords = e1.Coords;
            e1.Coords = temp;

            gameField[e0.Coords] = e1;
            gameField[e1.Coords] = e0;

            return true;
        }
        public bool CanSeeEachOther(Entity e0, Entity e1)
        {
            if (e0.Coords.X == e1.Coords.X)
            {
                int start = Math.Min(e0.Coords.Y, e1.Coords.Y);
                int finish = Math.Max(e0.Coords.Y, e1.Coords.Y);

                for (int y = start + 1; y < finish; y++)
                {
                    if (gameField[e0.Coords.X, y, e0.GfLayer] != null || gameField[e1.Coords.X, y, e1.GfLayer] != null)
                        return false;
                }

                return true;
            }
            else if (e0.Coords.Y == e1.Coords.Y)
            {
                int start = Math.Min(e0.Coords.X, e1.Coords.X);
                int finish = Math.Max(e0.Coords.X, e1.Coords.X);

                for (int x = start + 1; x < finish; x++)
                {
                    if (gameField[x, e0.Coords.Y, e0.GfLayer] != null || gameField[x, e1.Coords.Y, e1.GfLayer] != null)
                        return false;
                }

                return true;
            }

            return false;
        }

        public string GetInfoAboutEntitiesOnPos(int x, int y)
        {
            if (!engine.IsDebugMode 
                || x < 0 || x >= Util.gameField_Width
                || y < 0 || y >= Util.gameField_Height)
                return null;

            StringBuilder text = new StringBuilder();

            for (int z = 0; z < Util.gfLayersCount; z++)
            {
                Entity ent;
                if ((ent = gameField[x, y, z]) != null)
                {
                    if (text.Length > 0)
                        text.Append("\n");

                    text.Append($"{ent.GetType().Name} (ID: {ent.ID})\nLayer: { ent.GfLayer}\nType: {ent.EntType}\n");

                    if (ent is Mob m)
                    {
                        text.Append($"HP: {m.HP}/{m.InitialHP}, DMG: {m.DMG}\n");

                        if (m is ArmoredMob am)
                        {
                            text.Append("Weapons(ammo): ");
                            for (int w = 0; w < am.owningWeapons.Length; w++)
                                if (am.owningWeapons[w])
                                    text.AppendFormat($"{(WeaponType)w}({am.GetAmmo((WeaponType)w)}), ");
                        }
                    }
                    if (ent is HealingItem hI)
                        text.Append($"Heal: {hI.Amount}");
                    if (ent is AmmoPack aP)
                        text.AppendFormat($"Weapon: {aP.Wt}, amount: {aP.Amount}");
                }
            }

            if (text.Length > 0)
                return text.ToString();

            return null;
        }
        #endregion

        #region ACTIONS
        public void SpawnAction(Action a)
        {
            if (a != null)
                actions.Add(a);
        }
        public void Strike(StrikeAction sa, int x, int y)
        {
            if (x < 0 || x >= Util.gameField_Width || y < 0 || y >= Util.gameField_Height)
                return;

            if (gameField[x, y, Util.gfLayerMob] != null)
            {
                Mob m = (Mob)gameField[x, y, Util.gfLayerMob];

                if (!sa.hitedMobs.Contains(m.ID))
                {
                    m.Hit(sa.damage);
                    sa.hitedMobs.Add(m.ID);
                }
            }
            if (gameField[x, y, Util.gfLayerBullet] != null)
            {
                gameField[x, y, Util.gfLayerBullet].Dispose();
            }
            if (gameField[x, y, Util.gfLayerExplosive] != null)
            {
                ((Explosive)gameField[x, y, Util.gfLayerExplosive]).Dispose();
            }
            if (gameField[x, y, Util.gfLayerItem] != null)
            {
                ((Item)gameField[x, y, Util.gfLayerItem]).Strike();
            }
        }
        #endregion

        #region GRAPHICS -- Should be only called from Invoked method!!!
        public virtual void InitGraphics(FormWindow formWindow)
        {

        }
        public virtual void Render(Graphics g)
        {
            // draw background image
            if (backgroundImage != null)
                g.DrawImage(backgroundImage, 0, 0, g.ClipBounds.Width, g.ClipBounds.Height);

            // draw all drawable background actions
            for (int i = 0; i < actions.Count; i++)
                if (actions[i].FGPosition == FGPosition.Background)
                    actions[i].Draw(g);

            // draw all entities
            Entity e;
            for (int y = 0; y < Util.gameField_Height; y++)
                for (int x = 0; x < Util.gameField_Width; x++)
                    for (int z = Util.gfLayersCount - 1; z >= 0; z--)
                        if ((e = gameField[x, y, z]) != null)
                        {
                            if (e is MultiMob mb)
                                mb.DrawTile(g, x, y);
                            else
                                gameField[x, y, z].Draw(g);
                        }

            // draw all drawable foreground actions
            for (int i = 0; i < actions.Count; i++)
                if (actions[i].FGPosition == FGPosition.Foreground)
                    actions[i].Draw(g);

            // update graphics
            if (!engine.IsPaused)
                GGraphics.UpdateGraphics();

            if (GameTerminated)
            {
                if (GameOver)
                    g.DrawString("GAME OVER", font_gamefinished, Brushes.Red, 180, 350);

                if (GameWon)
                    g.DrawString("GAME WON", font_gamefinished, Brushes.Green, 200, 350);
            }
        }
        public virtual void UpdateScreen(FormWindow formWindow)
        {
            formWindow.textBox_playerHP.Text = player.HP.ToString();
            formWindow.textBox_score.Text = Counter.ToString();

            formWindow.textBox_activeWeapon.Text = player.ActiveWeapon.ToString();
            formWindow.textBox_ammo.Text = player.GetAmmo(player.ActiveWeapon).ToString();
        }
        #endregion

        #region Variables
        [NonSerialized]
        protected Engine engine;

        public Player player;
        public GFCoordinates PlayerPos => player.Coords;
        
        public int Counter { get; protected set; }
        public int Counter_HostileMobs { get; protected set; }
        public bool Inited { get; private set; }
        public bool GameOver { get; private set; }
        public bool GameWon { get; private set; }
        public bool GameTerminated { get; private set; }
        public readonly string GameName;

        public Dictionary<string, Entity> entities;
        public List<Entity> entitiesToSpawn, entitiesToDespawn;
        public List<Action> actions;
        private List<IMobTick> mobs;
        private List<IProjectileTick> projectiles;

        public GameField gameField; //x, y, z (z: 0-mobs, 1-projectiles, 2-explosives, 3-items)
        
        protected Image backgroundImage = null;

        public int Player_initialHP { get; protected set; } = 10;
        public int Player_DMG { get; protected set; } = 1;
        public bool[] Player_initialWeapons { get; protected set; } = new bool[] { true };
        public int[] Player_initialAmmos { get; protected set; } = new int[] { 100 };
        #endregion
        
        public void WriteToLog(string text, Color col) => engine.WriteToLog(text, col);
        public void WriteToDebugLog(string text, Color col, bool error = false) => engine.WriteToDebugLog(text, col, error);

        protected static HealingItem Item_Heart(Game game, GFCoordinates coords) => new HealingItem(Entity.NewID, game, coords, GGraphics.item_heart, 1);
        protected static HealingItem Item_Heart_2(Game game, GFCoordinates coords) => new HealingItem(Entity.NewID, game, coords, GGraphics.item_heart_2, 5);

        public static readonly Font font_gamefinished = new Font("Comics Sans MS", 72, FontStyle.Bold);
        public const string filePath_checkPoint = "checkpoint_xxx.cid";
    }
}
