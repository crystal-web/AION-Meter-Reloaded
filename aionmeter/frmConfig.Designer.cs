namespace AIONMeter
{
    partial class frmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkWindowOnTop = new System.Windows.Forms.CheckBox();
            this.combobox_language = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_aion_path = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chk_scan_previos_session = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_windowopacity = new System.Windows.Forms.NumericUpDown();
            this.chkBorderlessWindow = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_select_color = new System.Windows.Forms.Button();
            this.lbl_color = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_font = new System.Windows.Forms.Label();
            this.btn_select_font = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.combobox_textures = new AIONMeter.ComboBoxEx();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.cmdApply = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_windowopacity)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkWindowOnTop);
            this.tabPage1.Controls.Add(this.combobox_language);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.cmdBrowse);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txt_aion_path);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkWindowOnTop
            // 
            resources.ApplyResources(this.chkWindowOnTop, "chkWindowOnTop");
            this.chkWindowOnTop.Name = "chkWindowOnTop";
            this.chkWindowOnTop.UseVisualStyleBackColor = true;
            // 
            // combobox_language
            // 
            this.combobox_language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_language.FormattingEnabled = true;
            this.combobox_language.Items.AddRange(new object[] {
            resources.GetString("combobox_language.Items"),
            resources.GetString("combobox_language.Items1"),
            resources.GetString("combobox_language.Items2")});
            resources.ApplyResources(this.combobox_language, "combobox_language");
            this.combobox_language.Name = "combobox_language";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cmdBrowse
            // 
            resources.ApplyResources(this.cmdBrowse, "cmdBrowse");
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txt_aion_path
            // 
            resources.ApplyResources(this.txt_aion_path, "txt_aion_path");
            this.txt_aion_path.Name = "txt_aion_path";
            this.txt_aion_path.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chk_scan_previos_session);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chk_scan_previos_session
            // 
            resources.ApplyResources(this.chk_scan_previos_session, "chk_scan_previos_session");
            this.chk_scan_previos_session.Checked = true;
            this.chk_scan_previos_session.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_scan_previos_session.Name = "chk_scan_previos_session";
            this.chk_scan_previos_session.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.numericUpDown_windowopacity);
            this.tabPage3.Controls.Add(this.chkBorderlessWindow);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.btn_select_color);
            this.tabPage3.Controls.Add(this.lbl_color);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.lbl_font);
            this.tabPage3.Controls.Add(this.btn_select_font);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.combobox_textures);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // numericUpDown_windowopacity
            // 
            resources.ApplyResources(this.numericUpDown_windowopacity, "numericUpDown_windowopacity");
            this.numericUpDown_windowopacity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_windowopacity.Name = "numericUpDown_windowopacity";
            this.numericUpDown_windowopacity.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // chkBorderlessWindow
            // 
            resources.ApplyResources(this.chkBorderlessWindow, "chkBorderlessWindow");
            this.chkBorderlessWindow.Name = "chkBorderlessWindow";
            this.chkBorderlessWindow.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btn_select_color
            // 
            resources.ApplyResources(this.btn_select_color, "btn_select_color");
            this.btn_select_color.Name = "btn_select_color";
            this.btn_select_color.UseVisualStyleBackColor = true;
            this.btn_select_color.Click += new System.EventHandler(this.btn_select_color_Click);
            // 
            // lbl_color
            // 
            this.lbl_color.BackColor = System.Drawing.Color.DimGray;
            this.lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_color.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_color, "lbl_color");
            this.lbl_color.Name = "lbl_color";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lbl_font
            // 
            this.lbl_font.BackColor = System.Drawing.Color.DimGray;
            this.lbl_font.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_font.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_font, "lbl_font");
            this.lbl_font.Name = "lbl_font";
            // 
            // btn_select_font
            // 
            resources.ApplyResources(this.btn_select_font, "btn_select_font");
            this.btn_select_font.Name = "btn_select_font";
            this.btn_select_font.UseVisualStyleBackColor = true;
            this.btn_select_font.Click += new System.EventHandler(this.btn_select_font_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // combobox_textures
            // 
            this.combobox_textures.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combobox_textures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_textures.FormattingEnabled = true;
            this.combobox_textures.ImageList = null;
            resources.ApplyResources(this.combobox_textures, "combobox_textures");
            this.combobox_textures.Name = "combobox_textures";
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdApply
            // 
            resources.ApplyResources(this.cmdApply, "cmdApply");
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // frmConfig
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.tabControl);
            this.Name = "frmConfig";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_windowopacity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_aion_path;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Button btn_select_font;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_font;
        private System.Windows.Forms.Button btn_select_color;
        private System.Windows.Forms.Label lbl_color;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label label4;
        private ComboBoxEx combobox_textures;
        private System.Windows.Forms.Button cmdApply;
        private System.Windows.Forms.CheckBox chk_scan_previos_session;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combobox_language;
        private System.Windows.Forms.CheckBox chkBorderlessWindow;
        private System.Windows.Forms.CheckBox chkWindowOnTop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_windowopacity;
    }
}