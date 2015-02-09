using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace AIONMeter
{
    public partial class frmMeter : Form
    {
        #region form stuff

        /* allows borderless forms to move */
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private enum WINDOW_STATE
        {
            BORDERLESS,
            SIZEABLE
        }
        private WINDOW_STATE window_state = WINDOW_STATE.BORDERLESS;

        public frmMeter()
        {
            // Load splashscreen
            frmSplashscreen splashscreen = new frmSplashscreen();
            // Show it
            splashscreen.Show();
            
            InitializeComponent();

            // Double buffer the form drawing in order to prevent flickering
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            // Hide the splashscreen
            splashscreen.hideSplash();
        }

        private void frmMeter_Load(object sender, EventArgs e)
        {
            this.TopMost = Config.get_window_on_top();
            this.Opacity = Config.get_window_opacity();
            this.Width = Config.get_window_width();
            this.Height = Config.get_window_height();

            Meter m = new Meter(Meter.MODE.LIVE);
            this.set_render_mode(Config.get_meter_render_mode()); // put the meter on saved render-mode
            this.btn_tracker_status.Image = liveToolStripMenuItem.Image;

            Timer ui_timer = new Timer(); // set up the timer that'll redraw the graphs
            ui_timer.Interval = 1000; // redraw every 1 seconds
            ui_timer.Tick += new EventHandler(ui_timer_tick);
            ui_timer.Start();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.set_window_width(this.Width);
            Config.set_window_height(this.Height);
            this.notifyIcon.Visible = false;
            Meter.active_meter.thread.Abort();
            Application.Exit();
        }

        private void Scene_DoubleClick(object sender, EventArgs e)
        {
            switch_window_state();
        }

        private void frmMeter_DoubleClick(object sender, EventArgs e)
        {
            switch_window_state();
        }

        private void switch_window_state()
        {
            if (window_state == WINDOW_STATE.BORDERLESS)
            {
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                window_state = WINDOW_STATE.SIZEABLE;
            }
            else if (window_state == WINDOW_STATE.SIZEABLE)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                window_state = WINDOW_STATE.BORDERLESS;
            }
        }

        public void update_window_settings()
        {
            this.TopMost = Config.get_window_on_top();
            this.Opacity = Config.get_window_opacity();
        }

        private void Scene_MouseDown(object sender, MouseEventArgs e)
        {
            if (window_state == WINDOW_STATE.BORDERLESS)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void frmMeter_MouseDown(object sender, MouseEventArgs e)
        {
            if (window_state == WINDOW_STATE.BORDERLESS)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        #endregion

        #region UI painting
        private void ui_timer_tick(object sender, EventArgs eArgs)
        {
            Scene.Refresh(); // refresh the scene so paint will trigger
        }

        private void Scene_Paint(object sender, PaintEventArgs e)
        {
            GraphEngine.paint_meter(e.Graphics, e, this.Scene); // paint the graphs
        }

        public Region DrawRoundRect(float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y); // Line
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90); // Corner
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2)); // Line
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90); // Corner
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height); // Line
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90); // Corner
            gp.AddLine(x, y + height - (radius * 2), x, y + radius); // Line
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90); // Corner
            gp.CloseFigure();
            return new Region(gp);
        }

        protected override void OnPaint(PaintEventArgs paintEvnt)
        {
            if (window_state == WINDOW_STATE.BORDERLESS)
                this.Region = DrawRoundRect(0, 0, this.Width, this.Height, 5);
        }
        #endregion

        #region User Interface
        private void btn_config_Click(object sender, EventArgs e)
        {
            frmConfig c = new frmConfig();
            c.ShowDialog();
        }

        private void btn_details_Click(object sender, EventArgs e)
        {
            frmDetails f = new frmDetails();
            f.Show();
        }

        private void btn_announce_Click(object sender, EventArgs e)
        {
            string paste = "";
            int i = 1;
            paste += "<AIONMeter> ";
            foreach (KeyValuePair<string, double> pair in Meter.active_meter.get_sorted_list())
            {
                Player player = (Player)Meter.active_meter.group[pair.Key];
                paste += " [" + i + "-" + player.name + "] " + player.damage + " dmg (" + player.percent.ToString("#0.00") + "%)";
                i++;
            }
            Clipboard.SetText(paste);
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Meter.active_meter.group.clear();
        }


        private void fAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/aionmeter/wiki/FAQ");
        }

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/aionmeter/wiki/UserGuide");
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.aionmeter.com/donate/");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

        private delegate void delegate_notify_progress(int progress); // progress bar notify
        private delegate void delegate_notify_progress_max(string status, int max); // prograss bar max value notify
        private delegate void delegate_notify_progress_finished(); // notifies the progress bar that job is finished

        public void notify_progress_bar(int progress)
        {
            try
            {
                if (this.InvokeRequired) // let the main UI thread run the code
                    this.BeginInvoke(new delegate_notify_progress(notify_progress_bar), new object[] { progress });
                else
                {
                    progressbar.Value = progress;
                }
            }
            catch (Exception e) { }
        }

        public void notify_progress_bar_max(string status, int max)
        {
            try
            {
                if (this.InvokeRequired) // let the main UI thread run the code
                    this.BeginInvoke(new delegate_notify_progress_max(notify_progress_bar_max), new object[] { status, max });
                else
                {
                    lbl_status.Visible = true;
                    lbl_status.Text = status;
                    progressbar.Minimum = 0;
                    progressbar.Maximum = max;
                    progressbar.Visible = true;
                }
            }
            catch (Exception e) { }
        }

        public void notify_progress_bar_finished()
        {
            try
            {
                if (this.InvokeRequired) // let the main UI thread run the code
                    this.BeginInvoke(new delegate_notify_progress_finished(notify_progress_bar_finished), new object[] { });
                else
                {
                    progressbar.Visible = false;
                    lbl_status.Visible = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void damageDoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            set_render_mode(Meter.RENDER_MODE.render_damage);
        }

        private void healingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            set_render_mode(Meter.RENDER_MODE.render_healing);
        }

        private void set_render_mode(Meter.RENDER_MODE mode)
        {
            switch (mode)
            {
                case Meter.RENDER_MODE.render_damage:
                    damageDoneToolStripMenuItem.Checked = true;
                    healingToolStripMenuItem.Checked = false;
                    this.Text = "AIONMeter [" + Properties.Resources.DamageDone + "]";
                    break;
                case Meter.RENDER_MODE.render_healing:
                    damageDoneToolStripMenuItem.Checked = false;
                    healingToolStripMenuItem.Checked = true;
                    this.Text = "AIONMeter [" + Properties.Resources.HealingDone + "]";
                    break;
            }

            Meter.active_meter.render_mode = mode;
            Config.set_meter_render_mode(mode); // save render mode
        }
        private void btn_tracker_status_ButtonClick(object sender, EventArgs e)
        {

            if (Meter.active_meter.tracking_mode == Meter.MODE.LIVE)
            {
                btn_tracker_status.Image = pausedToolStripMenuItem.Image;
                Meter.active_meter.pause();
            }
            else if (Meter.active_meter.tracking_mode == Meter.MODE.PAUSED)
            {
                btn_tracker_status.Image = liveToolStripMenuItem.Image;
                Meter.active_meter.resume();
            }
        }

        private void liveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_tracker_status.Image = liveToolStripMenuItem.Image;
            if (Meter.active_meter.tracking_mode == Meter.MODE.PAUSED)
                Meter.active_meter.resume();
            else if (Meter.active_meter.tracking_mode == Meter.MODE.REPLAY || Meter.active_meter.tracking_mode == Meter.MODE.REPLAY_DEBUG)
            {
                Meter m = new Meter(Meter.MODE.LIVE);
            }
        }

        private void pausedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Meter.active_meter.tracking_mode == Meter.MODE.LIVE)
            {
                btn_tracker_status.Image = pausedToolStripMenuItem.Image;
                Meter.active_meter.pause();
            }
        }

        private void replayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Meter m = new Meter(Meter.MODE.REPLAY);
            btn_tracker_status.Image = replayToolStripMenuItem.Image;
        }

        private void replayDebugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Meter m = new Meter(Meter.MODE.REPLAY_DEBUG);
            btn_tracker_status.Image = replayDebugModeToolStripMenuItem.Image;
        }

        private void syncMacroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSyncMacro f = new frmSyncMacro();
            f.ShowDialog();
        }

        private void addGroupMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value="";
            if (InputBox("New member", "Enter new group member's name", ref value) == DialogResult.OK)
            {
                Meter.active_meter.group.join(value);
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        #endregion
    }
}
