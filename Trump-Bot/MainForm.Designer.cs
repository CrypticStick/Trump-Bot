namespace Trump_Bot
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LblConsole = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Console = new System.Windows.Forms.TextBox();
            this.MenuBar = new System.Windows.Forms.StatusStrip();
            this.DropDownButtons = new System.Windows.Forms.ToolStripDropDownButton();
            this.BtnRainbow = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnReset = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.LblBotName = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblCommandInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblMainCrtl = new System.Windows.Forms.Label();
            this.PnlUsrCtrl = new System.Windows.Forms.Panel();
            this.ChkUsername = new System.Windows.Forms.CheckBox();
            this.UserTabs = new System.Windows.Forms.TabControl();
            this.RolePage = new System.Windows.Forms.TabPage();
            this.LblGuildRoles = new System.Windows.Forms.Label();
            this.LstGuildRoles = new System.Windows.Forms.ListBox();
            this.BtnAddRole = new System.Windows.Forms.Button();
            this.BtnRemoveRole = new System.Windows.Forms.Button();
            this.LblUserRoles = new System.Windows.Forms.Label();
            this.LstUserRoles = new System.Windows.Forms.ListBox();
            this.TBDPage = new System.Windows.Forms.TabPage();
            this.LstUsers = new System.Windows.Forms.ComboBox();
            this.LblUsers = new System.Windows.Forms.Label();
            this.LblUserControl = new System.Windows.Forms.Label();
            this.LblMainSelect = new System.Windows.Forms.Label();
            this.Roles = new System.Windows.Forms.Panel();
            this.LstGuilds = new System.Windows.Forms.ComboBox();
            this.LblGuilds = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LstChannels = new System.Windows.Forms.ComboBox();
            this.PnlBtnCtrl = new System.Windows.Forms.Panel();
            this.TxtChannels = new System.Windows.Forms.TextBox();
            this.BtnSendMessage = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.MenuBar.SuspendLayout();
            this.PnlUsrCtrl.SuspendLayout();
            this.UserTabs.SuspendLayout();
            this.RolePage.SuspendLayout();
            this.Roles.SuspendLayout();
            this.PnlBtnCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblConsole
            // 
            this.LblConsole.AutoSize = true;
            this.LblConsole.Font = new System.Drawing.Font("Adobe Gothic Std B", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblConsole.Location = new System.Drawing.Point(116, 221);
            this.LblConsole.Margin = new System.Windows.Forms.Padding(0);
            this.LblConsole.Name = "LblConsole";
            this.LblConsole.Size = new System.Drawing.Size(101, 30);
            this.LblConsole.TabIndex = 33;
            this.LblConsole.Text = "Console";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.Console);
            this.panel1.Location = new System.Drawing.Point(25, 254);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 317);
            this.panel1.TabIndex = 26;
            // 
            // Console
            // 
            this.Console.BackColor = System.Drawing.Color.Black;
            this.Console.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Console.Location = new System.Drawing.Point(15, 17);
            this.Console.Multiline = true;
            this.Console.Name = "Console";
            this.Console.ReadOnly = true;
            this.Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Console.Size = new System.Drawing.Size(585, 285);
            this.Console.TabIndex = 0;
            this.Console.Tag = "";
            // 
            // MenuBar
            // 
            this.MenuBar.AllowMerge = false;
            this.MenuBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.MenuBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropDownButtons,
            this.LblBotName,
            this.LblStatus,
            this.LblCommandInfo});
            this.MenuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(1022, 29);
            this.MenuBar.SizingGrip = false;
            this.MenuBar.TabIndex = 32;
            // 
            // DropDownButtons
            // 
            this.DropDownButtons.BackColor = System.Drawing.SystemColors.Control;
            this.DropDownButtons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DropDownButtons.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnRainbow,
            this.BtnReset,
            this.BtnClose});
            this.DropDownButtons.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DropDownButtons.Name = "DropDownButtons";
            this.DropDownButtons.Size = new System.Drawing.Size(75, 27);
            this.DropDownButtons.Text = "Options";
            this.DropDownButtons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnRainbow
            // 
            this.BtnRainbow.Name = "BtnRainbow";
            this.BtnRainbow.Size = new System.Drawing.Size(223, 26);
            this.BtnRainbow.Text = "Refresh Status Colors";
            this.BtnRainbow.Click += new System.EventHandler(this.BtnRainbow_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(223, 26);
            this.BtnReset.Text = "Refresh Bot";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(223, 26);
            this.BtnClose.Text = "Close Bot";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // LblBotName
            // 
            this.LblBotName.BackColor = System.Drawing.SystemColors.Control;
            this.LblBotName.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.LblBotName.Name = "LblBotName";
            this.LblBotName.Size = new System.Drawing.Size(76, 24);
            this.LblBotName.Text = "Bot Name";
            // 
            // LblStatus
            // 
            this.LblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.LblStatus.Margin = new System.Windows.Forms.Padding(0, 3, 15, 2);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(49, 24);
            this.LblStatus.Text = "Status";
            this.LblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblCommandInfo
            // 
            this.LblCommandInfo.AutoSize = false;
            this.LblCommandInfo.BackColor = System.Drawing.SystemColors.Control;
            this.LblCommandInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.LblCommandInfo.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.LblCommandInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LblCommandInfo.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.LblCommandInfo.Name = "LblCommandInfo";
            this.LblCommandInfo.Size = new System.Drawing.Size(500, 24);
            this.LblCommandInfo.Text = "Waiting for action...";
            // 
            // LblMainCrtl
            // 
            this.LblMainCrtl.AutoSize = true;
            this.LblMainCrtl.Font = new System.Drawing.Font("Adobe Gothic Std B", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMainCrtl.Location = new System.Drawing.Point(424, 56);
            this.LblMainCrtl.Margin = new System.Windows.Forms.Padding(0);
            this.LblMainCrtl.Name = "LblMainCrtl";
            this.LblMainCrtl.Size = new System.Drawing.Size(138, 30);
            this.LblMainCrtl.TabIndex = 31;
            this.LblMainCrtl.Text = "Bot Control";
            // 
            // PnlUsrCtrl
            // 
            this.PnlUsrCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlUsrCtrl.BackColor = System.Drawing.Color.MidnightBlue;
            this.PnlUsrCtrl.Controls.Add(this.ChkUsername);
            this.PnlUsrCtrl.Controls.Add(this.UserTabs);
            this.PnlUsrCtrl.Controls.Add(this.LstUsers);
            this.PnlUsrCtrl.Controls.Add(this.LblUsers);
            this.PnlUsrCtrl.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.PnlUsrCtrl.Location = new System.Drawing.Point(663, 91);
            this.PnlUsrCtrl.Name = "PnlUsrCtrl";
            this.PnlUsrCtrl.Size = new System.Drawing.Size(331, 480);
            this.PnlUsrCtrl.TabIndex = 28;
            // 
            // ChkUsername
            // 
            this.ChkUsername.AutoSize = true;
            this.ChkUsername.Location = new System.Drawing.Point(25, 48);
            this.ChkUsername.Name = "ChkUsername";
            this.ChkUsername.Size = new System.Drawing.Size(167, 21);
            this.ChkUsername.TabIndex = 20;
            this.ChkUsername.Text = "Show Guild Nickname";
            this.ChkUsername.UseVisualStyleBackColor = true;
            this.ChkUsername.CheckedChanged += new System.EventHandler(this.ChkUsername_CheckedChanged);
            // 
            // UserTabs
            // 
            this.UserTabs.Controls.Add(this.RolePage);
            this.UserTabs.Controls.Add(this.TBDPage);
            this.UserTabs.Location = new System.Drawing.Point(25, 78);
            this.UserTabs.Name = "UserTabs";
            this.UserTabs.SelectedIndex = 0;
            this.UserTabs.Size = new System.Drawing.Size(283, 383);
            this.UserTabs.TabIndex = 19;
            // 
            // RolePage
            // 
            this.RolePage.Controls.Add(this.LblGuildRoles);
            this.RolePage.Controls.Add(this.LstGuildRoles);
            this.RolePage.Controls.Add(this.BtnAddRole);
            this.RolePage.Controls.Add(this.BtnRemoveRole);
            this.RolePage.Controls.Add(this.LblUserRoles);
            this.RolePage.Controls.Add(this.LstUserRoles);
            this.RolePage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RolePage.Location = new System.Drawing.Point(4, 25);
            this.RolePage.Name = "RolePage";
            this.RolePage.Padding = new System.Windows.Forms.Padding(3);
            this.RolePage.Size = new System.Drawing.Size(275, 354);
            this.RolePage.TabIndex = 0;
            this.RolePage.Text = "Roles";
            this.RolePage.UseVisualStyleBackColor = true;
            // 
            // LblGuildRoles
            // 
            this.LblGuildRoles.AutoSize = true;
            this.LblGuildRoles.Location = new System.Drawing.Point(91, 191);
            this.LblGuildRoles.Name = "LblGuildRoles";
            this.LblGuildRoles.Size = new System.Drawing.Size(81, 17);
            this.LblGuildRoles.TabIndex = 5;
            this.LblGuildRoles.Text = "Guild Roles";
            // 
            // LstGuildRoles
            // 
            this.LstGuildRoles.FormattingEnabled = true;
            this.LstGuildRoles.ItemHeight = 16;
            this.LstGuildRoles.Location = new System.Drawing.Point(20, 206);
            this.LstGuildRoles.Name = "LstGuildRoles";
            this.LstGuildRoles.Size = new System.Drawing.Size(231, 116);
            this.LstGuildRoles.TabIndex = 4;
            this.LstGuildRoles.SelectedIndexChanged += new System.EventHandler(this.GuildRoles_Click);
            // 
            // BtnAddRole
            // 
            this.BtnAddRole.Location = new System.Drawing.Point(153, 160);
            this.BtnAddRole.Name = "BtnAddRole";
            this.BtnAddRole.Size = new System.Drawing.Size(30, 23);
            this.BtnAddRole.TabIndex = 3;
            this.BtnAddRole.Text = "▲";
            this.BtnAddRole.UseVisualStyleBackColor = true;
            this.BtnAddRole.Click += new System.EventHandler(this.BtnAddRole_Click);
            // 
            // BtnRemoveRole
            // 
            this.BtnRemoveRole.Location = new System.Drawing.Point(80, 160);
            this.BtnRemoveRole.Name = "BtnRemoveRole";
            this.BtnRemoveRole.Size = new System.Drawing.Size(30, 23);
            this.BtnRemoveRole.TabIndex = 2;
            this.BtnRemoveRole.Text = "▼";
            this.BtnRemoveRole.UseVisualStyleBackColor = true;
            this.BtnRemoveRole.Click += new System.EventHandler(this.BtnRemoveRole_Click);
            // 
            // LblUserRoles
            // 
            this.LblUserRoles.AutoSize = true;
            this.LblUserRoles.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblUserRoles.Location = new System.Drawing.Point(91, 12);
            this.LblUserRoles.Name = "LblUserRoles";
            this.LblUserRoles.Size = new System.Drawing.Size(78, 17);
            this.LblUserRoles.TabIndex = 1;
            this.LblUserRoles.Text = "User Roles";
            // 
            // LstUserRoles
            // 
            this.LstUserRoles.FormattingEnabled = true;
            this.LstUserRoles.ItemHeight = 16;
            this.LstUserRoles.Location = new System.Drawing.Point(20, 27);
            this.LstUserRoles.Name = "LstUserRoles";
            this.LstUserRoles.Size = new System.Drawing.Size(231, 116);
            this.LstUserRoles.TabIndex = 0;
            this.LstUserRoles.SelectedIndexChanged += new System.EventHandler(this.UserRoles_Click);
            // 
            // TBDPage
            // 
            this.TBDPage.Location = new System.Drawing.Point(4, 25);
            this.TBDPage.Name = "TBDPage";
            this.TBDPage.Padding = new System.Windows.Forms.Padding(3);
            this.TBDPage.Size = new System.Drawing.Size(275, 354);
            this.TBDPage.TabIndex = 1;
            this.TBDPage.Text = "TBD";
            this.TBDPage.UseVisualStyleBackColor = true;
            // 
            // LstUsers
            // 
            this.LstUsers.FormattingEnabled = true;
            this.LstUsers.Location = new System.Drawing.Point(81, 18);
            this.LstUsers.Name = "LstUsers";
            this.LstUsers.Size = new System.Drawing.Size(176, 24);
            this.LstUsers.Sorted = true;
            this.LstUsers.TabIndex = 17;
            this.LstUsers.SelectedIndexChanged += new System.EventHandler(this.LstUsers_Click);
            // 
            // LblUsers
            // 
            this.LblUsers.AutoSize = true;
            this.LblUsers.Location = new System.Drawing.Point(25, 18);
            this.LblUsers.Name = "LblUsers";
            this.LblUsers.Size = new System.Drawing.Size(45, 17);
            this.LblUsers.TabIndex = 16;
            this.LblUsers.Text = "Users";
            // 
            // LblUserControl
            // 
            this.LblUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblUserControl.AutoSize = true;
            this.LblUserControl.Font = new System.Drawing.Font("Adobe Gothic Std B", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUserControl.Location = new System.Drawing.Point(751, 57);
            this.LblUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.LblUserControl.Name = "LblUserControl";
            this.LblUserControl.Size = new System.Drawing.Size(150, 30);
            this.LblUserControl.TabIndex = 30;
            this.LblUserControl.Text = "User Control";
            // 
            // LblMainSelect
            // 
            this.LblMainSelect.AutoSize = true;
            this.LblMainSelect.Font = new System.Drawing.Font("Adobe Gothic Std B", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMainSelect.Location = new System.Drawing.Point(100, 56);
            this.LblMainSelect.Margin = new System.Windows.Forms.Padding(0);
            this.LblMainSelect.Name = "LblMainSelect";
            this.LblMainSelect.Size = new System.Drawing.Size(144, 30);
            this.LblMainSelect.TabIndex = 29;
            this.LblMainSelect.Text = "Guild Select";
            // 
            // Roles
            // 
            this.Roles.BackColor = System.Drawing.Color.MidnightBlue;
            this.Roles.Controls.Add(this.LstGuilds);
            this.Roles.Controls.Add(this.LblGuilds);
            this.Roles.Controls.Add(this.label1);
            this.Roles.Controls.Add(this.LstChannels);
            this.Roles.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Roles.Location = new System.Drawing.Point(25, 91);
            this.Roles.Name = "Roles";
            this.Roles.Size = new System.Drawing.Size(297, 111);
            this.Roles.TabIndex = 27;
            // 
            // LstGuilds
            // 
            this.LstGuilds.FormattingEnabled = true;
            this.LstGuilds.Items.AddRange(new object[] {
            "N/A"});
            this.LstGuilds.Location = new System.Drawing.Point(96, 20);
            this.LstGuilds.Name = "LstGuilds";
            this.LstGuilds.Size = new System.Drawing.Size(176, 24);
            this.LstGuilds.Sorted = true;
            this.LstGuilds.TabIndex = 14;
            this.LstGuilds.DropDown += new System.EventHandler(this.Refresh_Guilds);
            this.LstGuilds.SelectedIndexChanged += new System.EventHandler(this.LstGuilds_Click);
            // 
            // LblGuilds
            // 
            this.LblGuilds.AutoSize = true;
            this.LblGuilds.Location = new System.Drawing.Point(15, 20);
            this.LblGuilds.Name = "LblGuilds";
            this.LblGuilds.Size = new System.Drawing.Size(48, 17);
            this.LblGuilds.TabIndex = 4;
            this.LblGuilds.Text = "Guilds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Channels";
            // 
            // LstChannels
            // 
            this.LstChannels.FormattingEnabled = true;
            this.LstChannels.Location = new System.Drawing.Point(96, 65);
            this.LstChannels.Name = "LstChannels";
            this.LstChannels.Size = new System.Drawing.Size(176, 24);
            this.LstChannels.Sorted = true;
            this.LstChannels.TabIndex = 15;
            this.LstChannels.SelectedIndexChanged += new System.EventHandler(this.LstChannels_Click);
            // 
            // PnlBtnCtrl
            // 
            this.PnlBtnCtrl.BackColor = System.Drawing.Color.MidnightBlue;
            this.PnlBtnCtrl.Controls.Add(this.TxtChannels);
            this.PnlBtnCtrl.Controls.Add(this.BtnSendMessage);
            this.PnlBtnCtrl.Location = new System.Drawing.Point(344, 91);
            this.PnlBtnCtrl.Name = "PnlBtnCtrl";
            this.PnlBtnCtrl.Size = new System.Drawing.Size(297, 145);
            this.PnlBtnCtrl.TabIndex = 25;
            // 
            // TxtChannels
            // 
            this.TxtChannels.Location = new System.Drawing.Point(18, 17);
            this.TxtChannels.Multiline = true;
            this.TxtChannels.Name = "TxtChannels";
            this.TxtChannels.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtChannels.Size = new System.Drawing.Size(263, 86);
            this.TxtChannels.TabIndex = 1;
            // 
            // BtnSendMessage
            // 
            this.BtnSendMessage.Enabled = false;
            this.BtnSendMessage.Location = new System.Drawing.Point(85, 109);
            this.BtnSendMessage.Name = "BtnSendMessage";
            this.BtnSendMessage.Size = new System.Drawing.Size(119, 28);
            this.BtnSendMessage.TabIndex = 0;
            this.BtnSendMessage.Text = "Send message";
            this.BtnSendMessage.UseVisualStyleBackColor = true;
            this.BtnSendMessage.Click += new System.EventHandler(this.BtnSendMessage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(1022, 583);
            this.Controls.Add(this.LblConsole);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MenuBar);
            this.Controls.Add(this.LblMainCrtl);
            this.Controls.Add(this.PnlUsrCtrl);
            this.Controls.Add(this.LblUserControl);
            this.Controls.Add(this.LblMainSelect);
            this.Controls.Add(this.Roles);
            this.Controls.Add(this.PnlBtnCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Trump Bot";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.PnlUsrCtrl.ResumeLayout(false);
            this.PnlUsrCtrl.PerformLayout();
            this.UserTabs.ResumeLayout(false);
            this.RolePage.ResumeLayout(false);
            this.RolePage.PerformLayout();
            this.Roles.ResumeLayout(false);
            this.Roles.PerformLayout();
            this.PnlBtnCtrl.ResumeLayout(false);
            this.PnlBtnCtrl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblConsole;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox Console;
        private System.Windows.Forms.StatusStrip MenuBar;
        private System.Windows.Forms.ToolStripDropDownButton DropDownButtons;
        private System.Windows.Forms.ToolStripMenuItem BtnRainbow;
        private System.Windows.Forms.ToolStripMenuItem BtnReset;
        private System.Windows.Forms.ToolStripMenuItem BtnClose;
        private System.Windows.Forms.ToolStripStatusLabel LblBotName;
        private System.Windows.Forms.ToolStripStatusLabel LblStatus;
        private System.Windows.Forms.ToolStripStatusLabel LblCommandInfo;
        private System.Windows.Forms.Label LblMainCrtl;
        private System.Windows.Forms.Panel PnlUsrCtrl;
        private System.Windows.Forms.CheckBox ChkUsername;
        private System.Windows.Forms.TabControl UserTabs;
        private System.Windows.Forms.TabPage RolePage;
        private System.Windows.Forms.Label LblGuildRoles;
        private System.Windows.Forms.ListBox LstGuildRoles;
        private System.Windows.Forms.Button BtnAddRole;
        private System.Windows.Forms.Button BtnRemoveRole;
        private System.Windows.Forms.Label LblUserRoles;
        private System.Windows.Forms.ListBox LstUserRoles;
        private System.Windows.Forms.TabPage TBDPage;
        private System.Windows.Forms.ComboBox LstUsers;
        private System.Windows.Forms.Label LblUsers;
        private System.Windows.Forms.Label LblUserControl;
        private System.Windows.Forms.Label LblMainSelect;
        private System.Windows.Forms.Panel Roles;
        private System.Windows.Forms.ComboBox LstGuilds;
        private System.Windows.Forms.Label LblGuilds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LstChannels;
        private System.Windows.Forms.Panel PnlBtnCtrl;
        private System.Windows.Forms.TextBox TxtChannels;
        private System.Windows.Forms.Button BtnSendMessage;
    }
}

