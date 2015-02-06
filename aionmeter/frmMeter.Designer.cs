namespace AIONMeter
{
    partial class frmMeter
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeter));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lbl_status = new System.Windows.Forms.Label();
            this.Scene = new System.Windows.Forms.PictureBox();
            this.statusStrip = new AIONMeter.StatusStripEx();
            this.btn_filter = new System.Windows.Forms.ToolStripSplitButton();
            this.healingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.damageDoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_tracker_status = new System.Windows.Forms.ToolStripSplitButton();
            this.liveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pausedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replayDebugModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_clear = new System.Windows.Forms.ToolStripDropDownButton();
            this.btn_announce = new System.Windows.Forms.ToolStripDropDownButton();
            this.syncMacroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGroupMemberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_details = new System.Windows.Forms.ToolStripDropDownButton();
            this.btn_config = new System.Windows.Forms.ToolStripDropDownButton();
            this.btn_about = new System.Windows.Forms.ToolStripDropDownButton();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fAQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressbar = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.Scene)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "Chat.log";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            // 
            // lbl_status
            // 
            resources.ApplyResources(this.lbl_status, "lbl_status");
            this.lbl_status.ForeColor = System.Drawing.Color.White;
            this.lbl_status.Name = "lbl_status";
            // 
            // Scene
            // 
            resources.ApplyResources(this.Scene, "Scene");
            this.Scene.Name = "Scene";
            this.Scene.TabStop = false;
            this.Scene.Paint += new System.Windows.Forms.PaintEventHandler(this.Scene_Paint);
            this.Scene.DoubleClick += new System.EventHandler(this.Scene_DoubleClick);
            this.Scene.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scene_MouseDown);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.Black;
            this.statusStrip.ClickThrough = true;
            this.statusStrip.ForeColor = System.Drawing.Color.White;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_filter,
            this.btn_tracker_status,
            this.btn_clear,
            this.btn_announce,
            this.btn_details,
            this.btn_config,
            this.btn_about,
            this.progressbar});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.ShowItemToolTips = true;
            // 
            // btn_filter
            // 
            this.btn_filter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_filter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.healingToolStripMenuItem,
            this.damageDoneToolStripMenuItem});
            resources.ApplyResources(this.btn_filter, "btn_filter");
            this.btn_filter.Name = "btn_filter";
            // 
            // healingToolStripMenuItem
            // 
            this.healingToolStripMenuItem.Name = "healingToolStripMenuItem";
            resources.ApplyResources(this.healingToolStripMenuItem, "healingToolStripMenuItem");
            this.healingToolStripMenuItem.Click += new System.EventHandler(this.healingToolStripMenuItem_Click);
            // 
            // damageDoneToolStripMenuItem
            // 
            this.damageDoneToolStripMenuItem.Checked = true;
            this.damageDoneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.damageDoneToolStripMenuItem.Name = "damageDoneToolStripMenuItem";
            resources.ApplyResources(this.damageDoneToolStripMenuItem, "damageDoneToolStripMenuItem");
            this.damageDoneToolStripMenuItem.Click += new System.EventHandler(this.damageDoneToolStripMenuItem_Click);
            // 
            // btn_tracker_status
            // 
            this.btn_tracker_status.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_tracker_status.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liveToolStripMenuItem,
            this.pausedToolStripMenuItem,
            this.replayToolStripMenuItem,
            this.replayDebugModeToolStripMenuItem});
            resources.ApplyResources(this.btn_tracker_status, "btn_tracker_status");
            this.btn_tracker_status.Name = "btn_tracker_status";
            this.btn_tracker_status.ButtonClick += new System.EventHandler(this.btn_tracker_status_ButtonClick);
            // 
            // liveToolStripMenuItem
            // 
            resources.ApplyResources(this.liveToolStripMenuItem, "liveToolStripMenuItem");
            this.liveToolStripMenuItem.Name = "liveToolStripMenuItem";
            this.liveToolStripMenuItem.Click += new System.EventHandler(this.liveToolStripMenuItem_Click);
            // 
            // pausedToolStripMenuItem
            // 
            resources.ApplyResources(this.pausedToolStripMenuItem, "pausedToolStripMenuItem");
            this.pausedToolStripMenuItem.Name = "pausedToolStripMenuItem";
            this.pausedToolStripMenuItem.Click += new System.EventHandler(this.pausedToolStripMenuItem_Click);
            // 
            // replayToolStripMenuItem
            // 
            resources.ApplyResources(this.replayToolStripMenuItem, "replayToolStripMenuItem");
            this.replayToolStripMenuItem.Name = "replayToolStripMenuItem";
            this.replayToolStripMenuItem.Click += new System.EventHandler(this.replayToolStripMenuItem_Click);
            // 
            // replayDebugModeToolStripMenuItem
            // 
            resources.ApplyResources(this.replayDebugModeToolStripMenuItem, "replayDebugModeToolStripMenuItem");
            this.replayDebugModeToolStripMenuItem.Name = "replayDebugModeToolStripMenuItem";
            this.replayDebugModeToolStripMenuItem.Click += new System.EventHandler(this.replayDebugModeToolStripMenuItem_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btn_clear, "btn_clear");
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.ShowDropDownArrow = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_announce
            // 
            this.btn_announce.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_announce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncMacroToolStripMenuItem,
            this.addGroupMemberToolStripMenuItem});
            resources.ApplyResources(this.btn_announce, "btn_announce");
            this.btn_announce.Name = "btn_announce";
            this.btn_announce.Click += new System.EventHandler(this.btn_announce_Click);
            // 
            // syncMacroToolStripMenuItem
            // 
            this.syncMacroToolStripMenuItem.Name = "syncMacroToolStripMenuItem";
            resources.ApplyResources(this.syncMacroToolStripMenuItem, "syncMacroToolStripMenuItem");
            this.syncMacroToolStripMenuItem.Click += new System.EventHandler(this.syncMacroToolStripMenuItem_Click);
            // 
            // addGroupMemberToolStripMenuItem
            // 
            this.addGroupMemberToolStripMenuItem.Name = "addGroupMemberToolStripMenuItem";
            resources.ApplyResources(this.addGroupMemberToolStripMenuItem, "addGroupMemberToolStripMenuItem");
            this.addGroupMemberToolStripMenuItem.Click += new System.EventHandler(this.addGroupMemberToolStripMenuItem_Click);
            // 
            // btn_details
            // 
            this.btn_details.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btn_details, "btn_details");
            this.btn_details.Name = "btn_details";
            this.btn_details.ShowDropDownArrow = false;
            this.btn_details.Click += new System.EventHandler(this.btn_details_Click);
            // 
            // btn_config
            // 
            this.btn_config.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btn_config, "btn_config");
            this.btn_config.Name = "btn_config";
            this.btn_config.ShowDropDownArrow = false;
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // btn_about
            // 
            this.btn_about.BackColor = System.Drawing.Color.Black;
            this.btn_about.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_about.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.donateToolStripMenuItem,
            this.userGuideToolStripMenuItem,
            this.fAQToolStripMenuItem,
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.btn_about, "btn_about");
            this.btn_about.Name = "btn_about";
            // 
            // donateToolStripMenuItem
            // 
            resources.ApplyResources(this.donateToolStripMenuItem, "donateToolStripMenuItem");
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // userGuideToolStripMenuItem
            // 
            resources.ApplyResources(this.userGuideToolStripMenuItem, "userGuideToolStripMenuItem");
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Click += new System.EventHandler(this.userGuideToolStripMenuItem_Click);
            // 
            // fAQToolStripMenuItem
            // 
            resources.ApplyResources(this.fAQToolStripMenuItem, "fAQToolStripMenuItem");
            this.fAQToolStripMenuItem.Name = "fAQToolStripMenuItem";
            this.fAQToolStripMenuItem.Click += new System.EventHandler(this.fAQToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // progressbar
            // 
            this.progressbar.Name = "progressbar";
            resources.ApplyResources(this.progressbar, "progressbar");
            this.progressbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // frmMeter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.Scene);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMeter";
            this.Opacity = 0.8D;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMeter_Load);
            this.DoubleClick += new System.EventHandler(this.frmMeter_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMeter_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.Scene)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripSplitButton btn_filter;
        private System.Windows.Forms.ToolStripDropDownButton btn_config;
        private System.Windows.Forms.ToolStripMenuItem healingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem damageDoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton btn_details;
        private System.Windows.Forms.ToolStripDropDownButton btn_clear;
        private System.Windows.Forms.ToolStripDropDownButton btn_announce;
        private System.Windows.Forms.ToolStripDropDownButton btn_about;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripProgressBar progressbar;
        private System.Windows.Forms.Label lbl_status;
        private StatusStripEx statusStrip;
        private System.Windows.Forms.ToolStripSplitButton btn_tracker_status;
        private System.Windows.Forms.ToolStripMenuItem liveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pausedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replayDebugModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncMacroToolStripMenuItem;
        private System.Windows.Forms.PictureBox Scene;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fAQToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addGroupMemberToolStripMenuItem;





    }
}

