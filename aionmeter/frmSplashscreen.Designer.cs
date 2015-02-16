namespace AIONMeter
{
    partial class frmSplashscreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplashscreen));
            this.SuspendLayout();
            // 
            // frmSplashscreen
            // 
            this.AllowDrop = true;
            //this.BackgroundImage = global::AIONMeter.Properties.Resources.aionmeter_reloaded;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(285, 55);
            this.ControlBox = false;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(301, 93);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(301, 93);
            this.Name = "frmSplashscreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);

        }

        #endregion
    }
}