using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubeInvasion
{
    public partial class FormWindow : Form
    {
        public FormWindow()
        {
            InitializeComponent();

            inputControl = new InputControl();
            engine = new Engine(this, inputControl);

            timer_minute = new Timer { Interval = 1000 };
            timer_minute.Tick += Timer_minute_Tick;
            timer_minute.Start();

            //set Tile dimensions
            Util.gameField_Tile_Width = pictureBox_Screen.Width / Util.gameField_Width;
            Util.gameField_Tile_Height = pictureBox_Screen.Height / Util.gameField_Height;
        }

        private Engine engine;
        private InputControl inputControl;
        private Timer timer_minute;
        
        private void FormWindow_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            engine?.Start();
        }
        private void FormWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (engine.IsRunning)
                engine.Stop();
        }
        private void FormWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer_minute.Stop();
        }
    
        private void PictureBox_Screen_Paint(object sender, PaintEventArgs e)
        {
            engine.Render(e.Graphics);
        }
        private void PictureBox_Screen_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / Util.gameField_Tile_Width, 
                y = e.Y / Util.gameField_Tile_Height;

            string info = engine.GetInfoAboutPosition(x, y);

            if (info != null)
                MessageBox.Show(info, string.Format("x={0} | y={1}", x, y));
        }

        private void FormWindow_KeyDown(object sender, KeyEventArgs e)
        {
            inputControl.setKeyState(e.KeyCode, true);
        }
        private void FormWindow_KeyUp(object sender, KeyEventArgs e)
        {
            inputControl.setKeyState(e.KeyCode, false);
        }

        private void Timer_minute_Tick(object sender, EventArgs e)
        {
            if (!engine.IsRunning )
                Close();
        }

 
        private void Button_Continue_Click(object sender, EventArgs e)
        {
            engine.Resume();
        }
        private void Button_NewGame_Click(object sender, EventArgs e)
        {
            groupBox_Menu.Enabled = false;
            groupBox_chooseGameMode.Enabled = groupBox_chooseGameMode.Visible = true;
            label_warning_gamelost.Visible = !engine.GameIsNullOrTerminated;

            listBox_GameModes.ClearSelected();
        }
        private void Button_Options_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Have you tried our pumpkin pie? It's delicious ;)");
        }
        private void Button_Save_Click(object sender, EventArgs e)
        {
            engine.Save();
        }
        private void Button_SaveAndExit_Click(object sender, EventArgs e)
        {
            if (engine.IsRunning)
                engine.Stop();
            else
                Close();
        }

        private void Button_Choose_Click(object sender, EventArgs e)
        {
            switch (listBox_GameModes.SelectedIndex)
            {
                case 0:
                    engine.RestoreGame(new Game_NotesCollector(engine));
                    break;
                case 1:
                    engine.RestoreGame(new Game_PortalRun(engine));
                    break;
            }
            groupBox_chooseGameMode.Enabled = groupBox_chooseGameMode.Visible = false;
            groupBox_Menu.Enabled = true;
            engine?.Resume();
        }
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            groupBox_chooseGameMode.Enabled = groupBox_chooseGameMode.Visible = false;
            groupBox_Menu.Enabled = true;
        }
}
}
