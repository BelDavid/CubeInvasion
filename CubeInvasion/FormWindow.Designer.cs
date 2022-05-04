using System;

namespace CubeInvasion
{
    partial class FormWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_Screen = new System.Windows.Forms.PictureBox();
            this.label_playerHP = new System.Windows.Forms.Label();
            this.textBox_playerHP = new System.Windows.Forms.TextBox();
            this.textBox_score = new System.Windows.Forms.TextBox();
            this.label_GameCounter = new System.Windows.Forms.Label();
            this.textBox_activeWeapon = new System.Windows.Forms.TextBox();
            this.label_activeWeapon = new System.Windows.Forms.Label();
            this.textBox_ammo = new System.Windows.Forms.TextBox();
            this.label_ammo = new System.Windows.Forms.Label();
            this.textBox_lastHitedMob = new System.Windows.Forms.TextBox();
            this.label_lastHitedMob = new System.Windows.Forms.Label();
            this.groupBox_stats = new System.Windows.Forms.GroupBox();
            this.label_data3 = new System.Windows.Forms.Label();
            this.textBox_value3 = new System.Windows.Forms.TextBox();
            this.label_data2 = new System.Windows.Forms.Label();
            this.textBox_value2 = new System.Windows.Forms.TextBox();
            this.label_data1 = new System.Windows.Forms.Label();
            this.textBox_value1 = new System.Windows.Forms.TextBox();
            this.groupBox_log = new System.Windows.Forms.GroupBox();
            this.richTextBox_log = new System.Windows.Forms.RichTextBox();
            this.groupBox_debug = new System.Windows.Forms.GroupBox();
            this.textBox_time = new System.Windows.Forms.TextBox();
            this.label_time = new System.Windows.Forms.Label();
            this.richTextBox_debugLog = new System.Windows.Forms.RichTextBox();
            this.textBox_actionCount = new System.Windows.Forms.TextBox();
            this.label_actionCount = new System.Windows.Forms.Label();
            this.textBox_entityCount = new System.Windows.Forms.TextBox();
            this.label_entityCount = new System.Windows.Forms.Label();
            this.textBox_ticklessRunCount = new System.Windows.Forms.TextBox();
            this.label_ticklessRunCount = new System.Windows.Forms.Label();
            this.textBox_tickCount = new System.Windows.Forms.TextBox();
            this.groupBox_Menu = new System.Windows.Forms.GroupBox();
            this.button_SaveAndExit = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Options = new System.Windows.Forms.Button();
            this.button_NewGame = new System.Windows.Forms.Button();
            this.label_MENU = new System.Windows.Forms.Label();
            this.button_Continue = new System.Windows.Forms.Button();
            this.listBox_GameModes = new System.Windows.Forms.ListBox();
            this.button_Choose = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox_chooseGameMode = new System.Windows.Forms.GroupBox();
            this.label_warning_gamelost = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Screen)).BeginInit();
            this.groupBox_stats.SuspendLayout();
            this.groupBox_log.SuspendLayout();
            this.groupBox_debug.SuspendLayout();
            this.groupBox_Menu.SuspendLayout();
            this.groupBox_chooseGameMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Screen
            // 
            this.pictureBox_Screen.BackColor = System.Drawing.Color.Black;
            this.pictureBox_Screen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_Screen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Screen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox_Screen.Location = new System.Drawing.Point(0, 110);
            this.pictureBox_Screen.Name = "pictureBox_Screen";
            this.pictureBox_Screen.Size = new System.Drawing.Size(980, 547);
            this.pictureBox_Screen.TabIndex = 0;
            this.pictureBox_Screen.TabStop = false;
            this.pictureBox_Screen.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Screen_Paint);
            this.pictureBox_Screen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_Screen_MouseDown);
            // 
            // label_playerHP
            // 
            this.label_playerHP.AutoSize = true;
            this.label_playerHP.BackColor = System.Drawing.Color.Transparent;
            this.label_playerHP.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_playerHP.Location = new System.Drawing.Point(12, 16);
            this.label_playerHP.Name = "label_playerHP";
            this.label_playerHP.Size = new System.Drawing.Size(108, 23);
            this.label_playerHP.TabIndex = 1;
            this.label_playerHP.Text = "Player\'s HP:";
            this.label_playerHP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_playerHP
            // 
            this.textBox_playerHP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_playerHP.Enabled = false;
            this.textBox_playerHP.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_playerHP.Location = new System.Drawing.Point(126, 16);
            this.textBox_playerHP.Name = "textBox_playerHP";
            this.textBox_playerHP.ReadOnly = true;
            this.textBox_playerHP.Size = new System.Drawing.Size(45, 23);
            this.textBox_playerHP.TabIndex = 2;
            this.textBox_playerHP.Text = "0";
            this.textBox_playerHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_score
            // 
            this.textBox_score.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_score.Enabled = false;
            this.textBox_score.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_score.Location = new System.Drawing.Point(126, 44);
            this.textBox_score.Name = "textBox_score";
            this.textBox_score.ReadOnly = true;
            this.textBox_score.Size = new System.Drawing.Size(45, 23);
            this.textBox_score.TabIndex = 4;
            this.textBox_score.Text = "0";
            this.textBox_score.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_GameCounter
            // 
            this.label_GameCounter.AutoSize = true;
            this.label_GameCounter.BackColor = System.Drawing.Color.Transparent;
            this.label_GameCounter.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_GameCounter.Location = new System.Drawing.Point(12, 44);
            this.label_GameCounter.Name = "label_GameCounter";
            this.label_GameCounter.Size = new System.Drawing.Size(106, 23);
            this.label_GameCounter.TabIndex = 3;
            this.label_GameCounter.Text = "<Counter0>:";
            this.label_GameCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_activeWeapon
            // 
            this.textBox_activeWeapon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_activeWeapon.Enabled = false;
            this.textBox_activeWeapon.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_activeWeapon.Location = new System.Drawing.Point(281, 16);
            this.textBox_activeWeapon.Name = "textBox_activeWeapon";
            this.textBox_activeWeapon.ReadOnly = true;
            this.textBox_activeWeapon.Size = new System.Drawing.Size(113, 23);
            this.textBox_activeWeapon.TabIndex = 6;
            this.textBox_activeWeapon.Text = "Bow";
            // 
            // label_activeWeapon
            // 
            this.label_activeWeapon.AutoSize = true;
            this.label_activeWeapon.BackColor = System.Drawing.Color.Transparent;
            this.label_activeWeapon.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_activeWeapon.Location = new System.Drawing.Point(198, 16);
            this.label_activeWeapon.Name = "label_activeWeapon";
            this.label_activeWeapon.Size = new System.Drawing.Size(77, 23);
            this.label_activeWeapon.TabIndex = 5;
            this.label_activeWeapon.Text = "Weapon:";
            this.label_activeWeapon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_ammo
            // 
            this.textBox_ammo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_ammo.Enabled = false;
            this.textBox_ammo.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_ammo.Location = new System.Drawing.Point(281, 44);
            this.textBox_ammo.Name = "textBox_ammo";
            this.textBox_ammo.ReadOnly = true;
            this.textBox_ammo.Size = new System.Drawing.Size(43, 23);
            this.textBox_ammo.TabIndex = 8;
            this.textBox_ammo.Text = "0";
            this.textBox_ammo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ammo
            // 
            this.label_ammo.AutoSize = true;
            this.label_ammo.BackColor = System.Drawing.Color.Transparent;
            this.label_ammo.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_ammo.Location = new System.Drawing.Point(198, 44);
            this.label_ammo.Name = "label_ammo";
            this.label_ammo.Size = new System.Drawing.Size(61, 23);
            this.label_ammo.TabIndex = 7;
            this.label_ammo.Text = "Ammo:";
            this.label_ammo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_lastHitedMob
            // 
            this.textBox_lastHitedMob.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_lastHitedMob.Enabled = false;
            this.textBox_lastHitedMob.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_lastHitedMob.Location = new System.Drawing.Point(601, 16);
            this.textBox_lastHitedMob.Name = "textBox_lastHitedMob";
            this.textBox_lastHitedMob.ReadOnly = true;
            this.textBox_lastHitedMob.Size = new System.Drawing.Size(69, 23);
            this.textBox_lastHitedMob.TabIndex = 10;
            this.textBox_lastHitedMob.Text = "0/0";
            this.textBox_lastHitedMob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_lastHitedMob
            // 
            this.label_lastHitedMob.AutoSize = true;
            this.label_lastHitedMob.BackColor = System.Drawing.Color.Transparent;
            this.label_lastHitedMob.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_lastHitedMob.Location = new System.Drawing.Point(388, 16);
            this.label_lastHitedMob.Name = "label_lastHitedMob";
            this.label_lastHitedMob.Size = new System.Drawing.Size(207, 23);
            this.label_lastHitedMob.TabIndex = 9;
            this.label_lastHitedMob.Text = "Last hited mob HP/DMG:";
            this.label_lastHitedMob.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox_stats
            // 
            this.groupBox_stats.Controls.Add(this.label_data3);
            this.groupBox_stats.Controls.Add(this.textBox_value3);
            this.groupBox_stats.Controls.Add(this.label_data2);
            this.groupBox_stats.Controls.Add(this.textBox_value2);
            this.groupBox_stats.Controls.Add(this.label_data1);
            this.groupBox_stats.Controls.Add(this.textBox_value1);
            this.groupBox_stats.Controls.Add(this.label_playerHP);
            this.groupBox_stats.Controls.Add(this.textBox_lastHitedMob);
            this.groupBox_stats.Controls.Add(this.textBox_playerHP);
            this.groupBox_stats.Controls.Add(this.label_lastHitedMob);
            this.groupBox_stats.Controls.Add(this.label_GameCounter);
            this.groupBox_stats.Controls.Add(this.textBox_ammo);
            this.groupBox_stats.Controls.Add(this.textBox_score);
            this.groupBox_stats.Controls.Add(this.label_ammo);
            this.groupBox_stats.Controls.Add(this.label_activeWeapon);
            this.groupBox_stats.Controls.Add(this.textBox_activeWeapon);
            this.groupBox_stats.Location = new System.Drawing.Point(0, 0);
            this.groupBox_stats.Name = "groupBox_stats";
            this.groupBox_stats.Size = new System.Drawing.Size(688, 110);
            this.groupBox_stats.TabIndex = 11;
            this.groupBox_stats.TabStop = false;
            this.groupBox_stats.Text = "Stats";
            // 
            // label_data3
            // 
            this.label_data3.AutoSize = true;
            this.label_data3.BackColor = System.Drawing.Color.Transparent;
            this.label_data3.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_data3.Location = new System.Drawing.Point(376, 76);
            this.label_data3.Name = "label_data3";
            this.label_data3.Size = new System.Drawing.Size(96, 23);
            this.label_data3.TabIndex = 16;
            this.label_data3.Text = "<GData1>:";
            this.label_data3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_data3.Visible = false;
            // 
            // textBox_value3
            // 
            this.textBox_value3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_value3.Enabled = false;
            this.textBox_value3.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_value3.Location = new System.Drawing.Point(481, 76);
            this.textBox_value3.Name = "textBox_value3";
            this.textBox_value3.ReadOnly = true;
            this.textBox_value3.Size = new System.Drawing.Size(45, 23);
            this.textBox_value3.TabIndex = 17;
            this.textBox_value3.Text = "<->";
            this.textBox_value3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_value3.Visible = false;
            // 
            // label_data2
            // 
            this.label_data2.AutoSize = true;
            this.label_data2.BackColor = System.Drawing.Color.Transparent;
            this.label_data2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_data2.Location = new System.Drawing.Point(196, 76);
            this.label_data2.Name = "label_data2";
            this.label_data2.Size = new System.Drawing.Size(96, 23);
            this.label_data2.TabIndex = 14;
            this.label_data2.Text = "<GData2>:";
            this.label_data2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_data2.Visible = false;
            // 
            // textBox_value2
            // 
            this.textBox_value2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_value2.Enabled = false;
            this.textBox_value2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_value2.Location = new System.Drawing.Point(301, 76);
            this.textBox_value2.Name = "textBox_value2";
            this.textBox_value2.ReadOnly = true;
            this.textBox_value2.Size = new System.Drawing.Size(45, 23);
            this.textBox_value2.TabIndex = 15;
            this.textBox_value2.Text = "<->";
            this.textBox_value2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_value2.Visible = false;
            // 
            // label_data1
            // 
            this.label_data1.AutoSize = true;
            this.label_data1.BackColor = System.Drawing.Color.Transparent;
            this.label_data1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_data1.Location = new System.Drawing.Point(12, 76);
            this.label_data1.Name = "label_data1";
            this.label_data1.Size = new System.Drawing.Size(96, 23);
            this.label_data1.TabIndex = 12;
            this.label_data1.Text = "<GData1>:";
            this.label_data1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_data1.Visible = false;
            // 
            // textBox_value1
            // 
            this.textBox_value1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_value1.Enabled = false;
            this.textBox_value1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_value1.Location = new System.Drawing.Point(126, 76);
            this.textBox_value1.Name = "textBox_value1";
            this.textBox_value1.ReadOnly = true;
            this.textBox_value1.Size = new System.Drawing.Size(45, 23);
            this.textBox_value1.TabIndex = 13;
            this.textBox_value1.Text = "<->";
            this.textBox_value1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_value1.Visible = false;
            // 
            // groupBox_log
            // 
            this.groupBox_log.Controls.Add(this.richTextBox_log);
            this.groupBox_log.Location = new System.Drawing.Point(694, 0);
            this.groupBox_log.Name = "groupBox_log";
            this.groupBox_log.Size = new System.Drawing.Size(286, 110);
            this.groupBox_log.TabIndex = 12;
            this.groupBox_log.TabStop = false;
            this.groupBox_log.Text = "Log";
            // 
            // richTextBox_log
            // 
            this.richTextBox_log.Enabled = false;
            this.richTextBox_log.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox_log.HideSelection = false;
            this.richTextBox_log.Location = new System.Drawing.Point(6, 13);
            this.richTextBox_log.Name = "richTextBox_log";
            this.richTextBox_log.ReadOnly = true;
            this.richTextBox_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_log.Size = new System.Drawing.Size(274, 91);
            this.richTextBox_log.TabIndex = 14;
            this.richTextBox_log.Text = "<Log>";
            // 
            // groupBox_debug
            // 
            this.groupBox_debug.Controls.Add(this.textBox_time);
            this.groupBox_debug.Controls.Add(this.label_time);
            this.groupBox_debug.Controls.Add(this.richTextBox_debugLog);
            this.groupBox_debug.Controls.Add(this.textBox_actionCount);
            this.groupBox_debug.Controls.Add(this.label_actionCount);
            this.groupBox_debug.Controls.Add(this.textBox_entityCount);
            this.groupBox_debug.Controls.Add(this.label_entityCount);
            this.groupBox_debug.Controls.Add(this.textBox_ticklessRunCount);
            this.groupBox_debug.Controls.Add(this.label_ticklessRunCount);
            this.groupBox_debug.Controls.Add(this.textBox_tickCount);
            this.groupBox_debug.Location = new System.Drawing.Point(694, 0);
            this.groupBox_debug.Name = "groupBox_debug";
            this.groupBox_debug.Size = new System.Drawing.Size(286, 110);
            this.groupBox_debug.TabIndex = 13;
            this.groupBox_debug.TabStop = false;
            this.groupBox_debug.Text = "Debug";
            this.groupBox_debug.Visible = false;
            // 
            // textBox_time
            // 
            this.textBox_time.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_time.Enabled = false;
            this.textBox_time.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_time.Location = new System.Drawing.Point(212, 92);
            this.textBox_time.Name = "textBox_time";
            this.textBox_time.ReadOnly = true;
            this.textBox_time.Size = new System.Drawing.Size(62, 16);
            this.textBox_time.TabIndex = 23;
            this.textBox_time.Text = "0s";
            this.textBox_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_time.Location = new System.Drawing.Point(179, 91);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(39, 16);
            this.label_time.TabIndex = 22;
            this.label_time.Text = "Time:";
            this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBox_debugLog
            // 
            this.richTextBox_debugLog.Enabled = false;
            this.richTextBox_debugLog.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox_debugLog.HideSelection = false;
            this.richTextBox_debugLog.Location = new System.Drawing.Point(6, 13);
            this.richTextBox_debugLog.Name = "richTextBox_debugLog";
            this.richTextBox_debugLog.ReadOnly = true;
            this.richTextBox_debugLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_debugLog.Size = new System.Drawing.Size(274, 63);
            this.richTextBox_debugLog.TabIndex = 13;
            this.richTextBox_debugLog.Text = "<Debug log>";
            // 
            // textBox_actionCount
            // 
            this.textBox_actionCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_actionCount.Enabled = false;
            this.textBox_actionCount.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_actionCount.Location = new System.Drawing.Point(139, 92);
            this.textBox_actionCount.Name = "textBox_actionCount";
            this.textBox_actionCount.ReadOnly = true;
            this.textBox_actionCount.Size = new System.Drawing.Size(31, 16);
            this.textBox_actionCount.TabIndex = 21;
            this.textBox_actionCount.Text = "0";
            this.textBox_actionCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_actionCount
            // 
            this.label_actionCount.AutoSize = true;
            this.label_actionCount.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_actionCount.Location = new System.Drawing.Point(93, 91);
            this.label_actionCount.Name = "label_actionCount";
            this.label_actionCount.Size = new System.Drawing.Size(52, 16);
            this.label_actionCount.TabIndex = 20;
            this.label_actionCount.Text = "Actions:";
            this.label_actionCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_entityCount
            // 
            this.textBox_entityCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_entityCount.Enabled = false;
            this.textBox_entityCount.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_entityCount.Location = new System.Drawing.Point(53, 92);
            this.textBox_entityCount.Name = "textBox_entityCount";
            this.textBox_entityCount.ReadOnly = true;
            this.textBox_entityCount.Size = new System.Drawing.Size(32, 16);
            this.textBox_entityCount.TabIndex = 17;
            this.textBox_entityCount.Text = "0";
            this.textBox_entityCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_entityCount
            // 
            this.label_entityCount.AutoSize = true;
            this.label_entityCount.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_entityCount.Location = new System.Drawing.Point(6, 91);
            this.label_entityCount.Name = "label_entityCount";
            this.label_entityCount.Size = new System.Drawing.Size(53, 16);
            this.label_entityCount.TabIndex = 16;
            this.label_entityCount.Text = "Entities:";
            this.label_entityCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_ticklessRunCount
            // 
            this.textBox_ticklessRunCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_ticklessRunCount.Enabled = false;
            this.textBox_ticklessRunCount.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_ticklessRunCount.Location = new System.Drawing.Point(208, 76);
            this.textBox_ticklessRunCount.Name = "textBox_ticklessRunCount";
            this.textBox_ticklessRunCount.ReadOnly = true;
            this.textBox_ticklessRunCount.Size = new System.Drawing.Size(69, 16);
            this.textBox_ticklessRunCount.TabIndex = 15;
            this.textBox_ticklessRunCount.Text = "0";
            this.textBox_ticklessRunCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ticklessRunCount
            // 
            this.label_ticklessRunCount.AutoSize = true;
            this.label_ticklessRunCount.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_ticklessRunCount.Location = new System.Drawing.Point(180, 75);
            this.label_ticklessRunCount.Name = "label_ticklessRunCount";
            this.label_ticklessRunCount.Size = new System.Drawing.Size(34, 16);
            this.label_ticklessRunCount.TabIndex = 14;
            this.label_ticklessRunCount.Text = "TLR:";
            this.label_ticklessRunCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_tickCount
            // 
            this.textBox_tickCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_tickCount.Enabled = false;
            this.textBox_tickCount.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_tickCount.Location = new System.Drawing.Point(6, 76);
            this.textBox_tickCount.Name = "textBox_tickCount";
            this.textBox_tickCount.ReadOnly = true;
            this.textBox_tickCount.Size = new System.Drawing.Size(168, 16);
            this.textBox_tickCount.TabIndex = 2;
            this.textBox_tickCount.Text = "T:0 | M:0 | Pr:0 | A:0 | Pl:0";
            this.textBox_tickCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox_Menu
            // 
            this.groupBox_Menu.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox_Menu.Controls.Add(this.button_SaveAndExit);
            this.groupBox_Menu.Controls.Add(this.button_Save);
            this.groupBox_Menu.Controls.Add(this.button_Options);
            this.groupBox_Menu.Controls.Add(this.button_NewGame);
            this.groupBox_Menu.Controls.Add(this.label_MENU);
            this.groupBox_Menu.Controls.Add(this.button_Continue);
            this.groupBox_Menu.Enabled = false;
            this.groupBox_Menu.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox_Menu.Location = new System.Drawing.Point(404, 161);
            this.groupBox_Menu.Name = "groupBox_Menu";
            this.groupBox_Menu.Size = new System.Drawing.Size(185, 242);
            this.groupBox_Menu.TabIndex = 13;
            this.groupBox_Menu.TabStop = false;
            this.groupBox_Menu.Visible = false;
            // 
            // button_SaveAndExit
            // 
            this.button_SaveAndExit.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_SaveAndExit.Location = new System.Drawing.Point(33, 192);
            this.button_SaveAndExit.Name = "button_SaveAndExit";
            this.button_SaveAndExit.Size = new System.Drawing.Size(120, 30);
            this.button_SaveAndExit.TabIndex = 5;
            this.button_SaveAndExit.Text = "Save and Exit";
            this.button_SaveAndExit.UseVisualStyleBackColor = true;
            this.button_SaveAndExit.Click += new System.EventHandler(this.Button_SaveAndExit_Click);
            // 
            // button_Save
            // 
            this.button_Save.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Save.Location = new System.Drawing.Point(33, 163);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(120, 30);
            this.button_Save.TabIndex = 4;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // button_Options
            // 
            this.button_Options.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Options.Location = new System.Drawing.Point(33, 118);
            this.button_Options.Name = "button_Options";
            this.button_Options.Size = new System.Drawing.Size(120, 30);
            this.button_Options.TabIndex = 3;
            this.button_Options.Text = "Options";
            this.button_Options.UseVisualStyleBackColor = true;
            this.button_Options.Click += new System.EventHandler(this.Button_Options_Click);
            // 
            // button_NewGame
            // 
            this.button_NewGame.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_NewGame.Location = new System.Drawing.Point(33, 76);
            this.button_NewGame.Name = "button_NewGame";
            this.button_NewGame.Size = new System.Drawing.Size(120, 30);
            this.button_NewGame.TabIndex = 2;
            this.button_NewGame.Text = "New game";
            this.button_NewGame.UseVisualStyleBackColor = true;
            this.button_NewGame.Click += new System.EventHandler(this.Button_NewGame_Click);
            // 
            // label_MENU
            // 
            this.label_MENU.AutoSize = true;
            this.label_MENU.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_MENU.Location = new System.Drawing.Point(48, 0);
            this.label_MENU.Name = "label_MENU";
            this.label_MENU.Size = new System.Drawing.Size(89, 35);
            this.label_MENU.TabIndex = 1;
            this.label_MENU.Text = "MENU";
            this.label_MENU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_Continue
            // 
            this.button_Continue.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Continue.Location = new System.Drawing.Point(33, 47);
            this.button_Continue.Name = "button_Continue";
            this.button_Continue.Size = new System.Drawing.Size(120, 30);
            this.button_Continue.TabIndex = 0;
            this.button_Continue.Text = "Continue";
            this.button_Continue.UseVisualStyleBackColor = true;
            this.button_Continue.Click += new System.EventHandler(this.Button_Continue_Click);
            // 
            // listBox_GameModes
            // 
            this.listBox_GameModes.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBox_GameModes.FormattingEnabled = true;
            this.listBox_GameModes.ItemHeight = 17;
            this.listBox_GameModes.Items.AddRange(new object[] {
            "Notes collector",
            "Portal run"});
            this.listBox_GameModes.Location = new System.Drawing.Point(6, 16);
            this.listBox_GameModes.Name = "listBox_GameModes";
            this.listBox_GameModes.Size = new System.Drawing.Size(185, 55);
            this.listBox_GameModes.TabIndex = 14;
            // 
            // button_Choose
            // 
            this.button_Choose.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Choose.Location = new System.Drawing.Point(6, 75);
            this.button_Choose.Name = "button_Choose";
            this.button_Choose.Size = new System.Drawing.Size(91, 30);
            this.button_Choose.TabIndex = 15;
            this.button_Choose.Text = "Choose";
            this.button_Choose.UseVisualStyleBackColor = true;
            this.button_Choose.Click += new System.EventHandler(this.Button_Choose_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Cancel.Location = new System.Drawing.Point(102, 75);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(92, 30);
            this.button_Cancel.TabIndex = 16;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // groupBox_chooseGameMode
            // 
            this.groupBox_chooseGameMode.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox_chooseGameMode.Controls.Add(this.label_warning_gamelost);
            this.groupBox_chooseGameMode.Controls.Add(this.listBox_GameModes);
            this.groupBox_chooseGameMode.Controls.Add(this.button_Cancel);
            this.groupBox_chooseGameMode.Controls.Add(this.button_Choose);
            this.groupBox_chooseGameMode.Enabled = false;
            this.groupBox_chooseGameMode.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox_chooseGameMode.Location = new System.Drawing.Point(396, 231);
            this.groupBox_chooseGameMode.Name = "groupBox_chooseGameMode";
            this.groupBox_chooseGameMode.Size = new System.Drawing.Size(201, 130);
            this.groupBox_chooseGameMode.TabIndex = 17;
            this.groupBox_chooseGameMode.TabStop = false;
            this.groupBox_chooseGameMode.Text = "Choose game mode";
            this.groupBox_chooseGameMode.Visible = false;
            // 
            // label_warning_gamelost
            // 
            this.label_warning_gamelost.AutoSize = true;
            this.label_warning_gamelost.ForeColor = System.Drawing.Color.Red;
            this.label_warning_gamelost.Location = new System.Drawing.Point(7, 108);
            this.label_warning_gamelost.Name = "label_warning_gamelost";
            this.label_warning_gamelost.Size = new System.Drawing.Size(184, 16);
            this.label_warning_gamelost.TabIndex = 17;
            this.label_warning_gamelost.Text = "WARNING! Progress will be lost!";
            // 
            // FormWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(980, 657);
            this.Controls.Add(this.groupBox_chooseGameMode);
            this.Controls.Add(this.groupBox_debug);
            this.Controls.Add(this.groupBox_log);
            this.Controls.Add(this.groupBox_stats);
            this.Controls.Add(this.groupBox_Menu);
            this.Controls.Add(this.pictureBox_Screen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormWindow";
            this.Text = "Cube Invasion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormWindow_FormClosed);
            this.Load += new System.EventHandler(this.FormWindow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormWindow_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Screen)).EndInit();
            this.groupBox_stats.ResumeLayout(false);
            this.groupBox_stats.PerformLayout();
            this.groupBox_log.ResumeLayout(false);
            this.groupBox_debug.ResumeLayout(false);
            this.groupBox_debug.PerformLayout();
            this.groupBox_Menu.ResumeLayout(false);
            this.groupBox_Menu.PerformLayout();
            this.groupBox_chooseGameMode.ResumeLayout(false);
            this.groupBox_chooseGameMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_activeWeapon;
        private System.Windows.Forms.Label label_ammo;
        private System.Windows.Forms.Label label_lastHitedMob;
        private System.Windows.Forms.GroupBox groupBox_stats;
        private System.Windows.Forms.Label label_ticklessRunCount;
        private System.Windows.Forms.Label label_actionCount;
        private System.Windows.Forms.Label label_entityCount;
        internal System.Windows.Forms.TextBox textBox_playerHP;
        internal System.Windows.Forms.TextBox textBox_score;
        internal System.Windows.Forms.TextBox textBox_activeWeapon;
        internal System.Windows.Forms.TextBox textBox_ammo;
        internal System.Windows.Forms.TextBox textBox_lastHitedMob;
        internal System.Windows.Forms.TextBox textBox_tickCount;
        internal System.Windows.Forms.TextBox textBox_ticklessRunCount;
        internal System.Windows.Forms.TextBox textBox_actionCount;
        internal System.Windows.Forms.TextBox textBox_entityCount;
        internal System.Windows.Forms.GroupBox groupBox_log;
        internal System.Windows.Forms.GroupBox groupBox_debug;
        internal System.Windows.Forms.PictureBox pictureBox_Screen;
        internal System.Windows.Forms.RichTextBox richTextBox_debugLog;
        internal System.Windows.Forms.RichTextBox richTextBox_log;
        internal System.Windows.Forms.TextBox textBox_time;
        private System.Windows.Forms.Label label_time;
        internal System.Windows.Forms.GroupBox groupBox_Menu;
        private System.Windows.Forms.Label label_MENU;
        internal System.Windows.Forms.Button button_SaveAndExit;
        internal System.Windows.Forms.Button button_Save;
        internal System.Windows.Forms.Button button_Options;
        internal System.Windows.Forms.Button button_NewGame;
        internal System.Windows.Forms.Button button_Continue;
        private System.Windows.Forms.ListBox listBox_GameModes;
        internal System.Windows.Forms.Button button_Cancel;
        internal System.Windows.Forms.Button button_Choose;
        private System.Windows.Forms.GroupBox groupBox_chooseGameMode;
        private System.Windows.Forms.Label label_playerHP;
        internal System.Windows.Forms.Label label_GameCounter;
        internal System.Windows.Forms.Label label_data1;
        internal System.Windows.Forms.TextBox textBox_value1;
        internal System.Windows.Forms.Label label_data3;
        internal System.Windows.Forms.TextBox textBox_value3;
        internal System.Windows.Forms.Label label_data2;
        internal System.Windows.Forms.TextBox textBox_value2;
        private System.Windows.Forms.Label label_warning_gamelost;
    }
}