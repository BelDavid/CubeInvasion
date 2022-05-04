using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CubeInvasion
{
    [Serializable]
    class Game_PortalRun : Game
    {
        public Game_PortalRun(Engine engine) : base(engine, "Portal Run")
        {

        }

        protected Portal portal;
        public override void Init()
        {
            base.Init();

            portal = new Portal(this);
            SpawnAction(portal);
            
            Counter = 1;
        }
        protected override void Load()
        {
            base.Load();

            try
            {
                using (StreamReader streamReader = new StreamReader(new FileStream(path_file_gameData, FileMode.Open, FileAccess.Read)))
                {
                    string line;
                    bool level = false, wave = false;
                    int levelNum = 0, waveNum = 0, lineNum = 0;
                    bool playerDataProcessed = false;

                    gameData = new List<List<List<string>>>();
                    Dictionary<string, string> templates = new Dictionary<string, string>(),
                        substitutions = new Dictionary<string, string>();


                    while ((line = streamReader.ReadLine()) != null)
                    {
                        line = line.Trim().ToLower();
                        lineNum++;

                        foreach (var substKey in substitutions.Keys)
                            while (line.Contains(substKey))
                                line = line.Replace(substKey, substitutions[substKey]);

                        if (line == "<level>")
                        {
                            if (level || wave)
                                throw new FormatException("Wrong operator " + line + " on " + lineNum + ". line");
                            else
                            {
                                level = true;
                                levelNum++;
                                gameData.Add(new List<List<string>>());
                            }
                        }
                        else if (line == "</level>")
                        {
                            if (!level || wave)
                                throw new FormatException("Wrong operator " + line + " on " + lineNum + ". line");
                            else
                            {
                                level = false;
                                waveNum = 0;
                            }
                        }
                        else if (line == "<wave>")
                        {
                            if (!level || wave)
                                throw new FormatException("Wrong operator " + line + " on " + lineNum + ". line");
                            else
                            {
                                wave = true;
                                waveNum++;
                                gameData[levelNum - 1].Add(new List<string>());
                            }
                        }
                        else if (line == "</wave>")
                        {
                            if (!wave || !level)
                                throw new FormatException("Wrong operator " + line + " on " + lineNum + ". line");
                            else
                                wave = false;
                        }
                        else if (line.StartsWith("<--"))
                        {
                            // just comentary
                        }
                        else if (line.StartsWith("<++")) // Template
                        {
                            string[] data = line.Replace("<++", "").Split('+');

                            if (data.Length != 2)
                                throw new ArgumentException("Invalid template format");

                            templates.Add(data[0], data[1]);
                        }
                        else if (line.StartsWith("<??")) // Substitution
                        {
                            string[] data = line.Replace("<??", "").Replace(">", "").Split('?');

                            if (data.Length != 2)
                                throw new ArgumentException("Invalid substitution format");

                            substitutions.Add(data[0], data[1]);
                        }
                        else if (level && wave && (line.StartsWith("<") && line.EndsWith(">")))
                        {
                            string[] data = line.Replace("<", "").Replace(">", "").Split(' ');

                            if (templates.TryGetValue(data[0], out string template))
                            {
                                if (!int.TryParse(data[1], out int count))
                                    throw new FormatException(data[1] + " is invalind number for count");

                                for (int i = 0; i < count; i++)
                                    gameData[levelNum - 1][waveNum - 1].Add(template);
                            }
                            else
                                gameData[levelNum - 1][waveNum - 1].Add(line);
                        }
                        else if (!level && !wave && line.StartsWith("<player ") && line.EndsWith(">"))
                        {
                            if (playerDataProcessed)
                                throw new FormatException("Can not modify Player data twice");

                            playerDataProcessed = true;

                            try // Process PLayer data
                            {
                                string[] data = line.Replace("<player ", "").Replace(">", "").Split(';');

                                if (data.Length != 4)
                                    throw new ArgumentException("There shall be 4 arguments");

                                if (!int.TryParse(data[0], out int init_hp) || init_hp <= 0)
                                    throw new FormatException(data[0] + " is not valid number for HP");

                                if (!int.TryParse(data[1], out int dmg) || dmg < 0)
                                    throw new FormatException(data[1] + " is not valid number for DMG");

                                string[] initialWeapons_STR = data[2].Split(',');
                                bool[] initialWeapons = new bool[Math.Min(initialWeapons_STR.Length, (int)WeaponType.COUNT)];
                                for (int i = 0; i < initialWeapons_STR.Length; i++)
                                {
                                    if (initialWeapons_STR[i] == "1")
                                        initialWeapons[i] = true;
                                    else if (initialWeapons_STR[i] != "0")
                                        throw new FormatException(initialWeapons_STR[i] + " is not valid token for initialWeapon(" + (WeaponType)i + ") state");
                                }

                                string[] initialAmmos_STR = data[3].Split(',');
                                int[] initialAmmos = new int[Math.Min(initialAmmos_STR.Length, (int)WeaponType.COUNT)];
                                for (int i = 0; i < initialAmmos.Length; i++)
                                    if (!int.TryParse(initialAmmos_STR[i], out initialAmmos[i]) || initialAmmos[i] < 0)
                                        throw new FormatException(initialAmmos_STR[i] + " is not valid number for initialAmmo(" + (WeaponType)i + ") state");


                                this.Player_initialHP = init_hp;
                                this.Player_DMG = dmg;
                                this.Player_initialWeapons = initialWeapons;
                                this.Player_initialAmmos = initialAmmos;
                            }
                            catch (Exception ex)
                            {
                                WriteToDebugLog("Error parsing player data\n" + ex.Message, Color.Red, true);
                            }
                        }
                    }
                    if (wave || level)
                        throw new FormatException("All <level> and <wave> has to be closed properly. With </...>");
                }
            }
            catch (Exception ex)
            {
                WriteToDebugLog("Error reading data\n" + ex.Message, Color.Red, true);
            }
        }
        public override void InitGraphics(FormWindow formWindow)
        {
            base.InitGraphics(formWindow);

            // Level Counter
            formWindow.label_GameCounter.Text = "Level:";

            // Wave counter
            formWindow.label_data1.Visible = formWindow.textBox_value1.Visible = true;
            formWindow.label_data1.Text = "Wave:";
            formWindow.textBox_value1.Text = "1";
        }

        public override void Tick_Actions()
        {
            base.Tick_Actions();
            
            if (portal.Activated)
            {
                portal.Hide();

                Counter_Wave++;
                if (gameData == null)
                    switch (Counter_Wave)
                    {
                        case 1:
                            for (int i = 0; i < 5; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_brown, 2, 1,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 10 } })));
                            AddToSpawnEntity(new HostileMob_BowShooter(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_purple, 20, 1, 100,
                                new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 10 } })));
                            break;

                        case 2:
                            for (int i = 0; i < 10; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_brown, 2, 1,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 10 } })));
                            break;


                        case 3:
                            for (int i = 0; i < 10; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_brown, 2, 1,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 10 } })));
                            for (int i = 0; i < 2; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_red, 4, 3,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 20 } })));
                            break;

                        case 4:
                            for (int i = 0; i < 10; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_red, 2, 1,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 10 } })));
                            for (int i = 0; i < 3; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_orange, 10, 3,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 20 } })));
                            for (int i = 0; i < 2; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_blue, 20, 6,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 30 } })));
                            break;

                        case 5:
                            for (int i = 0; i < 5; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_red, 2, 1,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 10 } })));
                            for (int i = 0; i < 5; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_orange, 10, 3,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 20 } })));
                            for (int i = 0; i < 5; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_blue, 20, 6,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 30 } })));
                            break;

                        case 6:
                            for (int i = 0; i < 10; i++)
                                AddToSpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_orange, 10, 3,
                                    new LootSetup(Item_Heart, .1, .3, 1, new int[] { 1 }, new int[,] { { 20, 10 } })));
                            AddToSpawnEntity(new MultiMob(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.multimob_orange, 200, 20, Game_NotesCollector.occupiedSpace_4T,
                                    new LootSetup(Item_Heart_2, .2, .2, 1, new int[] { 1 }, new int[,] { { 30, 30 } })));
                            break;

                        default:
                            Terminate(true);
                            break;
                    }

                else
                {
                    if (Counter_Wave - 1 >= gameData[Counter - 1].Count)
                    {
                        Counter++;
                        Counter_Wave = 1;

                        if (Counter - 1 >= gameData.Count)
                            Terminate(true);
                    }

                    if (Counter - 1 < gameData.Count && Counter_Wave - 1 < gameData[Counter - 1].Count)
                    {
                        List<string> waveData = gameData[Counter - 1][Counter_Wave - 1];
                        for (int i = 0; i < waveData.Count; i++)
                        {
                            try
                            {
                                string[] data = waveData[i].Replace("<", "").Replace(">", "").Split(' ');

                                if (data.Length != 2)
                                    throw new ArgumentException("Pattern shall be: <type data>");

                                if (data[0] == "mob" || data[0] == "multimob" || data[0] == "bowshooter" || data[0] == "mineplanter")
                                    CreateAndAddToSpawnHostileMob(data[0], data[1]);

                                else if (data[0] == "weapon")
                                {
                                    if (!int.TryParse(data[1], out int weapon) || weapon < 0)
                                        throw new FormatException(data[1] + " is not valid numer for weapon");

                                    weaponToSpawn = (WeaponType)weapon;
                                }
                                else if (data[0] == "background")
                                {
                                    string path = "data/img/" + data[1];
                                    if (!File.Exists(path))
                                        throw new FileNotFoundException("File (" + path + ") not found");

                                    backgroundImage = new Bitmap(path);
                                }
                                else
                                    throw new FormatException("Unknown command");
                            }
                            catch (Exception ex)
                            {
                                engine.WriteToDebugLog("Error parsing \"" + waveData[i] + "\" - [" + ex.Message + "]", Color.Red, true);
                            }
                        }
                    }
                }
            }
        }
        
        protected override void EntityDespawned(Entity e)
        {
            base.EntityDespawned(e);

            if (Counter_HostileMobs == 0)
            {
                portal.Show();

                if ((int)weaponToSpawn >= (int)WeaponType.COUNT)
                    weaponToSpawn = WeaponType.NONE;
                else if (weaponToSpawn != WeaponType.NONE)
                {
                    AddToSpawnEntity(new WeaponPackage(Entity.NewID, this, e.Coords.Copy(), weaponToSpawn));

                    weaponToSpawn = WeaponType.NONE;
                }
            }
        }
        
        public override void UpdateScreen(FormWindow formWindow)
        {
            base.UpdateScreen(formWindow);
            formWindow.textBox_value1.Text = Counter_Wave.ToString();
        }

        public int Counter_Wave { get; protected set; } = 0;
        protected List<List<List<string>>> gameData;
        protected WeaponType weaponToSpawn = WeaponType.NONE;

        public new const string filePath_checkPoint = "checkpoint_PortalRun.cid";
        public const string path_file_gameData = Util.path_folder_data + "PortalRun" + Util.file_extension;
    }
}
