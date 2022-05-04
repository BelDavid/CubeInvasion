using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubeInvasion
{
    class Engine
    {
        #region MainMethods
        public Engine(FormWindow fw, InputControl input)
        {
            formWindow = fw;
            this.input = input;

            thread_engine = new Thread(new ThreadStart(Run)) { Name = "EngineThread" };
        }

        private bool Init()
        {
            if (!Load())
            {
                //game = new Game_NotesCollector(this);
                //game.Init();

                WriteToDebugLog("Is this blue?", Color.Blue);
                WriteToDebugLog("New game created", Color.Green);
            }
            else
                WriteToDebugLog("Game loaded", Color.Green);

            WriteToDebugLog(string.Format("TileWidth: {0}, TileHeight: {1}", Util.gameField_Tile_Width, Util.gameField_Tile_Height), Color.LimeGreen);
            
            return IsInited = true;
        }
        private void Term()
        {
            WriteToDebugLog("Game terminated", Color.Green);

            if (game != null)
            {
                game.Tick();
                Save();
            }
        }

        public void Start()
        {
            thread_engine.Start();
        }
        public void Stop()
        {
            IsRunning = false;
        }
        public void RestoreGame(Game game)
        {
            if (game == null)
                return;

            this.game = game;

            if (!game.Inited)
                game.Init();
            else
                game.Restore(this);
            
            InitGraphics();
        }

        public void PauseAndShowMenu()
        {
            IsMenuShown = true;
        }
        public void Pause()
        {
            IsPaused = true;
        }
        public void Resume()
        {
            IsMenuShown = false;
        }

        private void Run()
        {
            Stopwatch sw = new Stopwatch();

            Util.delegate_returnVoid_NoParameters[] methods_Ticks = {    Tick,
                                                                    Tick_Mobs,
                                                                    Tick_Projectiles,
                                                                    Tick_Actions,
                                                                    Tick_Player };
            double[] msPerTicks = { 1000D / Util.ticksPerSecond,
                                    1000D / Util.ticksPerSecond_mobs,
                                    1000D / Util.ticksPerSecond_bullets,
                                    1000D / Util.ticksPerSecond_actions,
                                    1000D / Util.ticksPerSecond_player }; 

            long[] lastTimes = new long[methods_Ticks.Length];
            double[] deltas = new double[methods_Ticks.Length];
            int[] ticks = new int[methods_Ticks.Length];

            tick_count = new int[methods_Ticks.Length];

            long lastSecTimer = 0,
                now = 0;

            int ticklessRuns = 0;
                
            seconds = 0;

            bool ticklessRun;

            if (!Init())
                return;

            sw.Start();

            while (IsRunning)
            {
                ticklessRun = true;

                // Ticks
                for (int i = 0; i < methods_Ticks.Length; i++)
                {
                    now = sw.ElapsedMilliseconds;
                    deltas[i] += (now - lastTimes[i]) / msPerTicks[i];
                    lastTimes[i] = now;
                    if (deltas[i] > Util.ticksSkipingLimit)
                        deltas[i] -= (int)(deltas[i] - Util.ticksSkipingLimit);
                    while (deltas[i] >= 1)
                    {
                        ticklessRun = false;
                        ticks[i] += 1;
                        methods_Ticks[i]();
                        deltas[i] -= 1;
                    }
                }

                //ticklessRun check
                if (ticklessRun)
                    ticklessRuns += 1;

                //perSecond check
                if (sw.ElapsedMilliseconds - lastSecTimer >= 1000)
                {
                    seconds += 1;

                    lastSecTimer += 1000;

                    if (seconds % 60 == 0) // per minute check
                        Save();

                    for (int i = 0; i < methods_Ticks.Length; i++)
                    {
                        tick_count[i] = ticks[i];
                        ticks[i] = 0;
                    }
                    this.ticklessRuns = ticklessRuns;
                    ticklessRuns = 0;

                    // check LAG
                    if (this.ticklessRuns < 1000)
                        Check_LAG++;
                    else
                        Check_LAG = 0;
                }
            }
            sw.Stop();

            Term();
        }
        #endregion

        #region Methods
        private void Tick()
        {
            // CheckPoint restoring
            if (gameToRestore != null) 
            {
                RestoreGame(game = gameToRestore);

                gameToRestore = null;
                IsPaused = true;
            }

            // Input handle
            if (input.isKeyDisposablePressed(Keys.Escape))
                IsMenuShown = !IsMenuShown;
            if (input.isKeyDisposablePressed(Keys.Oemtilde))
            {
                if (!IsMenuShown)
                    IsPaused = !IsPaused;
                else
                {
                    IsMenuShown = false;
                    IsPaused = true;
                }
            }
            if (input.isKeyDisposablePressed(Keys.F1))
                Save();
            if (input.isKeyDisposablePressed(Keys.F3))
                IsDebugMode = !IsDebugMode;


            // Game tick
            if (!IsPaused)
                game?.Tick();

            // Stats and messages
            UpdateWindow();
        }
        private void Tick_Mobs()
        {
            // paused?
            if (IsPaused) 
                return;

            game?.Tick_Mobs();
        }
        private void Tick_Projectiles()
        {
            // paused?
            if (IsPaused) { return; }

            game?.Tick_Projectiles();
        }
        private void Tick_Actions()
        {
            // paused?
            if (IsPaused) { return; }

            game?.Tick_Actions();
        }
        private void Tick_Player()
        {
            if (IsPaused)
                return;

            game?.Tick_Player();
        }
        
        public void Render(Graphics g)
        {
            if (!IsInited)
                return;

            game?.Render(g);

            if (IsPaused)
                g.DrawString("Paused", font_gamePaused, Brushes.Red, 10, 10);
        }
        private void InitGraphics()
        {
            if (formWindow.InvokeRequired)
                formWindow.Invoke(new Util.delegate_returnVoid_NoParameters(InitGraphics));
            else
            {
                // Reset
                formWindow.label_data1.Visible = formWindow.textBox_value1.Visible =
                    formWindow.label_data2.Visible = formWindow.textBox_value2.Visible =
                    formWindow.label_data3.Visible = formWindow.textBox_value3.Visible = false;

                formWindow.richTextBox_log.Clear();
                formWindow.richTextBox_debugLog.Clear();

                formWindow.richTextBox_log.SelectionColor =
                    formWindow.richTextBox_debugLog.SelectionColor = Color.DarkGray;
                formWindow.richTextBox_log.AppendText("<Log>");
                formWindow.richTextBox_debugLog.AppendText("<Debug log>");

                // Init
                game?.InitGraphics(formWindow);
            }
        }
        private void UpdateWindow()
        {
            if (formWindow.IsDisposed || formWindow.Disposing)
                return;

            try
            {
                if (formWindow.InvokeRequired)
                    formWindow.Invoke(new Util.delegate_returnVoid_NoParameters(UpdateWindow));
                else
                {
                    if (!IsRunning)
                    {
                        formWindow.Close();
                        return;
                    }

                    game?.UpdateScreen(formWindow);
                    
                    formWindow.textBox_lastHitedMob.Text = (Mob.lastMobTakingDamage != null ? Mob.lastMobTakingDamage.HP + "/" + Mob.lastMobTakingDamage.DMG : "0/0");

                    for (int i = offsetLog; i < log.Count; i++)
                    {
                        formWindow.richTextBox_log.SelectionColor = logColors[i];
                        formWindow.richTextBox_log.AppendText(Environment.NewLine + log[i]);
                    }
                    offsetLog = log.Count;

                    for (int i = offsetDebugLog; i < debugLog.Count; i++)
                    {
                        formWindow.richTextBox_debugLog.SelectionColor = debugLogColors[i];
                        formWindow.richTextBox_debugLog.AppendText(Environment.NewLine + debugLog[i]);
                    }
                    offsetDebugLog = debugLog.Count;
                    
                    if (formWindow.groupBox_debug.Visible = IsDebugMode)
                    {
                        formWindow.textBox_tickCount.Text = string.Format("T:{0:#0} | M:{1:#0} | Pr:{2:#0} | A:{3:#0} | Pl:{4:#0}", tick_count[0], tick_count[1], tick_count[2], tick_count[3], tick_count[4]);
                        formWindow.textBox_ticklessRunCount.Text = string.Format("{0:# ### ### ##0}", ticklessRuns);
                        
                        formWindow.textBox_entityCount.Text = string.Format("{0:# ##0}", (game?.entities.Count ?? 0));
                        formWindow.textBox_actionCount.Text = string.Format("{0:# ##0}", (game?.actions.Count ?? 0));

                        formWindow.textBox_time.Text = Time_current;
                    }

                    if (!IsMenuShown)
                    {
                        formWindow.ActiveControl = null;
                        formWindow.Focus();
                    }
                    else
                        formWindow.button_Continue.Enabled
                                = formWindow.button_Save.Enabled = !GameIsNullOrTerminated;

                    formWindow.pictureBox_Screen.Refresh();
                    
                    if (formWindow.groupBox_Menu.Visible != IsMenuShown)
                        formWindow.groupBox_Menu.Visible = formWindow.groupBox_Menu.Enabled = IsMenuShown;
                }
            }
            catch (ObjectDisposedException ex)
            {
                WriteToDebugLog(ex.Message, Color.DarkRed, true);
            }
        }
      
        public bool Save()
        {
            bool saved = false;
            Stream stream = null;
            
            try
            {
                stream = new FileStream(Util.path_file_savedData, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                IFormatter formatter = new BinaryFormatter();
                
                formatter.Serialize(stream, !GameIsNullOrTerminated);

                if (!GameIsNullOrTerminated)
                {
                    //GameData
                    formatter.Serialize(stream, game);

                    formatter.Serialize(stream, Entity._ID);
                    formatter.Serialize(stream, Action._ID);
                    
                    //Logs
                    formatter.Serialize(stream, log);
                    formatter.Serialize(stream, logColors);

                    formatter.Serialize(stream, debugLog);
                    formatter.Serialize(stream, debugLogColors);

                    //Time
                    formatter.Serialize(stream, seconds);
                }

                saved = true;
                WriteToDebugLog("Saved", Color.Blue);
            }
            catch (Exception ex)
            {
                WriteToDebugLog("Failed to save data (ex: " + ex.Message + ")", Color.Red, true);
            }
            finally
            {
                stream?.Close();
            }

            return saved;
        }
        private bool Load()
        {
            if (!File.Exists(Util.path_file_savedData))
                return false;

            FileStream stream = null;
            bool loaded = false;

            try
            {
                stream = new FileStream(Util.path_file_savedData, FileMode.Open, FileAccess.Read, FileShare.Read);
                IFormatter formatter = new BinaryFormatter();

                if ((bool)formatter.Deserialize(stream)) // saved
                {
                    //GameData
                    game = (Game)formatter.Deserialize(stream);
                    RestoreGame(game);

                    //IDs
                    Entity._ID = (int)formatter.Deserialize(stream);
                    Action._ID = (int)formatter.Deserialize(stream);
                    
                    //Logs
                    log = (List<string>)formatter.Deserialize(stream);
                    logColors = (List<Color>)formatter.Deserialize(stream);

                    debugLog = (List<string>)formatter.Deserialize(stream);
                    debugLogColors = (List<Color>)formatter.Deserialize(stream);

                    //Time
                    seconds = (int)formatter.Deserialize(stream);
                    
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                WriteToDebugLog("Failed to load data (ex: " + ex.Message + ")", Color.Red, true);
            }
            finally
            {
                stream?.Close();
            }
            
            return loaded;
        }
        
        public bool SaveCheckPoint(string filePath)
        {
            bool saved = false;
            Stream stream = null;

            try
            {
                stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, !GameIsNullOrTerminated);

                if (!GameIsNullOrTerminated)
                {
                    //GameData
                    formatter.Serialize(stream, game);

                    formatter.Serialize(stream, Entity._ID);
                    formatter.Serialize(stream, Action._ID);
                }
                
                saved = true;
                WriteToDebugLog("Checkpoint Saved", Color.Blue);
            }
            catch (Exception ex)
            {
                WriteToDebugLog("Failed to save checkpoint (ex: " + ex.Message + ")", Color.Red, true);
            }
            finally
            {
                stream?.Close();
            }

            return saved;
        }
        public bool LoadCheckpoint(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            FileStream stream = null;

            bool loaded = false;

            try
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                IFormatter formatter = new BinaryFormatter();

                if ((bool)formatter.Deserialize(stream))
                {
                    //GameData
                    gameToRestore = (Game)formatter.Deserialize(stream);

                    //IDs
                    Entity._ID = (int)formatter.Deserialize(stream);
                    Action._ID = (int)formatter.Deserialize(stream);
                }
                loaded = true;
            }
            catch (Exception ex)
            {
                WriteToDebugLog("Failed to load checkpoint (ex: " + ex.Message + ")", Color.Red, true);
            }
            finally
            {
                stream?.Close();
            }

            return loaded;
        }

        public void WriteToLog(string text, Color col)
        {
            log.Add(text);
            logColors.Add(col);
        }
        public void WriteToDebugLog(string text, Color color, bool error = false)
        {
            if (error)
                IsDebugMode = true;

            debugLogColors.Add(color);
            debugLog.Add(text + "   < " + Time_current + " >");
        }
        #endregion

        #region VARIABLES
        private readonly Thread thread_engine;
        private readonly FormWindow formWindow;
        public readonly InputControl input;

        private Game game, gameToRestore;

        private bool _paused = true, _menuShown = true;

        public bool IsRunning { get; private set; } = true;
        public bool IsInited { get; private set; } = false;
        public bool IsPaused { get => _paused; private set { _paused = value; input.resetAllDisposableKeys(); } }
        private bool IsMenuShown { get => _menuShown; set { _menuShown = IsPaused = value; } }
        public bool IsDebugMode { get; private set; }
        public string Time_current => (seconds > 3600 ? string.Format("{0}h ", seconds / 3600) : "") +
                        (seconds > 60 && seconds % 3600 != 0 ? string.Format("{0}m ", (seconds / 60) % 60) : "") +
                        string.Format("{0}s", seconds % 60);

        private int[] tick_count;
        private int ticklessRuns, seconds;

        private int offsetLog = 0, offsetDebugLog = 0;
        private List<string> log = new List<string>(), debugLog = new List<string>();
        private List<Color> logColors = new List<Color>(), debugLogColors = new List<Color>();

        public string GetInfoAboutPosition(int x, int y) => game?.GetInfoAboutEntitiesOnPos(x, y);
        public bool GameIsNullOrTerminated => game == null || game.GameTerminated;
        public int Check_LAG { get; private set; } = 0;

        public static readonly Font font_gamePaused = new Font("Comics Sans MS", 24, FontStyle.Bold);
        #endregion

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormWindow());
        }
    }
}
