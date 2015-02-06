/* 
AIONMeter is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

AIONMeter is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with AIONMeter.  If not, see <http://www.gnu.org/licenses/>.

Hüseyin Uslu, <shalafiraistlin nospam gmail dot com> 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Collections;
using System.Globalization;

namespace AIONMeter
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                txt_aion_path.Text = Config.get_game_path();

                for (int i = 0; i < combobox_language.Items.Count; i++)
                {
                    if (Config.get_language().ToLower() == ((string)combobox_language.Items[i]).ToLower())
                    {
                        combobox_language.SelectedIndex = i;
                        break;
                    }
                }

                chk_scan_previos_session.Checked = Config.get_scan_previos_session_on_startup();
                chkWindowOnTop.Checked = Config.get_window_on_top();

                lbl_font.Font = Config.get_font();
                lbl_font.Text = lbl_font.Font.Name + " " + lbl_font.Font.Size;
                lbl_color.ForeColor = Config.get_color();

                this.numericUpDown_windowopacity.Value = (decimal)(Config.get_window_opacity() * 100);

                // Iterate through embbeded textures
                combobox_textures.ImageList = new ImageList();
                combobox_textures.ImageList.ColorDepth = ColorDepth.Depth32Bit;
                combobox_textures.ImageList.ImageSize = new Size(256, 32);
                ResourceManager rm = new ResourceManager(typeof(Textures));
                using (ResourceSet rs = rm.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentUICulture, true, true))
                {
                    IDictionaryEnumerator resourceEnumerator = rs.GetEnumerator();
                    while (resourceEnumerator.MoveNext())
                    {
                        if (resourceEnumerator.Value is Image)
                        {
                            Image img = (Image)resourceEnumerator.Value;
                            combobox_textures.ImageList.Images.Add(img);
                            combobox_textures.Items.Add(new ComboBoxExItem(combobox_textures.ImageList.Images.Count - 1, resourceEnumerator.Key.ToString()));
                            if (Config.get_bar_texture_name() == resourceEnumerator.Key.ToString())
                                combobox_textures.SelectedIndex = combobox_textures.Items.Count - 1;

                        }
                    }
                }
            }
            catch (Exception exc)
            { }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            Config.set_game_path(txt_aion_path.Text);
            Config.set_font(lbl_font.Font.Name, lbl_font.Font.Size);
            Config.set_color(lbl_color.ForeColor);
            ComboBoxExItem selected = (ComboBoxExItem)combobox_textures.SelectedItem;
            Config.set_bar_texture_name(selected.tag);
            Config.set_scan_previos_session_on_startup(chk_scan_previos_session.Checked);
            Config.set_window_on_top(chkWindowOnTop.Checked);
            Config.set_language(combobox_language.SelectedItem.ToString());
            Config.set_window_opacity((double)numericUpDown_windowopacity.Value/100);
            if(Program.main_window!=null)
                Program.main_window.update_window_settings();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            cmdApply_Click(this, e);
            if (!Config.game_path_exists())
            {
                MessageBox.Show("AION path is not correcty set. Please choose your AION install path",
                    "AION path needed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Config.create_aion_config_file();
                    this.Close();
                }
                catch (UnauthorizedAccessException)
                {
                    DialogResult result = System.Windows.Forms.MessageBox.Show("Unauthorized Access, please run this application in Administrator mode",
                        "Unauthorized Access",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign,
                        true);

                    Application.Exit();
                }
            }
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selected_folder = folderBrowserDialog.SelectedPath;
                txt_aion_path.Text = selected_folder;
            }
        }

        private void btn_select_font_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Font selected_font = fontDialog.Font;
                lbl_font.Font = selected_font;
                lbl_font.Name = selected_font.Name;
                lbl_font.Text = lbl_font.Font.Name + " " + lbl_font.Font.Size;
            }
        }

        private void btn_select_color_Click(object sender, EventArgs e)
        {
            colorDialog.FullOpen = true;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color selected_color = colorDialog.Color;
                lbl_color.ForeColor = selected_color;
            }
        }
    }
}
