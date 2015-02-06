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
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Resources;
using System.Collections;
using System.Windows.Forms;

namespace AIONMeter
{
    public static class Config
    {
        private static string application_path;

        public static void set_application_path(string path)
        {
            application_path = path;
        }

        public static string get_application_path()
        {
            return application_path;
        }

        public static bool settings_upgraded()
        {
            return Properties.Settings.Default.settings_upgraded;
        }

        public static void upgrade_settings() // check if user's settings has been upgraded from previous version, if not do so
        {
            if (!Properties.Settings.Default.settings_upgraded)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.settings_upgraded = true;
                Properties.Settings.Default.Save();
            }
        }

        public static void set_language(string lang)
        {
            Properties.Settings.Default.language = lang;
            Properties.Settings.Default.Save();
        }

        public static string get_language()
        {
            return Properties.Settings.Default.language;
        }

        public static void set_meter_render_mode(Meter.RENDER_MODE mode) // set meter render mode
        {
            Properties.Settings.Default.render_mode = (byte)mode;
            Properties.Settings.Default.Save();
        }

        public static Meter.RENDER_MODE get_meter_render_mode() // get meter render mode
        {
            return (Meter.RENDER_MODE)Properties.Settings.Default.render_mode;
        }

        public static void set_scan_previos_session_on_startup(bool value) // set scan_previos_session
        {
            Properties.Settings.Default.scan_previous_session_on_startup = value;
            Properties.Settings.Default.Save();
        }

        public static bool get_scan_previos_session_on_startup() // get scan_previos_session
        {
            return Properties.Settings.Default.scan_previous_session_on_startup;
        }

        public static void set_bar_texture_name(string name) // set bar texture
        {
            Properties.Settings.Default.bar_texture = name;
            Properties.Settings.Default.Save();
        }

        public static string get_bar_texture_name() // get_bar_texture
        {
            return Properties.Settings.Default.bar_texture;
        }

        public static System.Drawing.Image get_texture() // returns the configured bar texture
        {
            ResourceManager rm = new ResourceManager(typeof(Textures));
            using (ResourceSet rs = rm.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentUICulture, true, true))
            {
                IDictionaryEnumerator resourceEnumerator = rs.GetEnumerator();
                while (resourceEnumerator.MoveNext())
                {
                    if (resourceEnumerator.Value is System.Drawing.Image)
                    {
                        if (Config.get_bar_texture_name() == resourceEnumerator.Key.ToString())
                            return (System.Drawing.Image)resourceEnumerator.Value;
                    }
                }
            }
            return null;
        }

        public static void set_font(string name, float size) // set default font
        {
            Properties.Settings.Default.font_name = name;
            Properties.Settings.Default.font_size = size;
            Properties.Settings.Default.Save();
        }

        public static System.Drawing.Font get_font() // return the font
        {
            string name = Properties.Settings.Default.font_name;
            float size = Properties.Settings.Default.font_size;

            if (name.Trim().Length == 0) // if not set use defaults
                name = "Verdana";
            if (size == 0)
                size = 8;
            return new System.Drawing.Font(name, size);
        }

        public static void set_color(System.Drawing.Color color) // set text forecolor
        {
            Properties.Settings.Default.color = System.Drawing.ColorTranslator.ToHtml(color);
            Properties.Settings.Default.Save();
        }

        public static System.Drawing.Color get_color() // get text forecolor
        {
            if (Properties.Settings.Default.color.Trim().Length > 0)
                return System.Drawing.ColorTranslator.FromHtml(Properties.Settings.Default.color);
            else
                return System.Drawing.Color.White; //the default color                  
        }

        public static void set_window_on_top(bool ontop)
        {
            Properties.Settings.Default.window_on_top = ontop;
            Properties.Settings.Default.Save();
        }

        public static bool get_window_on_top()
        {
            return Properties.Settings.Default.window_on_top;
        }

        public static void set_window_opacity(double opacity)
        {
            Properties.Settings.Default.windows_opacity = opacity;
            Properties.Settings.Default.Save();
        }

        public static double get_window_opacity()
        {
            return Properties.Settings.Default.windows_opacity;
        }

        public static void set_window_width(int width)
        {
            Properties.Settings.Default.window_width = width;
            Properties.Settings.Default.Save();
        }

        public static int get_window_width()
        {
            return Properties.Settings.Default.window_width;
        }

        public static void set_window_height(int height)
        {
            Properties.Settings.Default.window_height = height;
            Properties.Settings.Default.Save();
        }

        public static int get_window_height()
        {
            return Properties.Settings.Default.window_height;
        }

        public static string get_game_log_path() // returns the game logspath
        {
            return Properties.Settings.Default.AION_Path + "\\Chat.log";
        }

        public static string get_game_path() // returns configured game path
        {
            return Properties.Settings.Default.AION_Path;
        }

        public static void set_game_path(string path) // set game path
        {
            Properties.Settings.Default.AION_Path = path;
            Properties.Settings.Default.Save();
        }

        public static Boolean game_path_exists() // check the configuration
        {
            string path = get_game_path();
            string aion_exe = path + "/bin32/aion.bin";
            string chat_log = path + "/Chat.log";

            if (!Directory.Exists(path) || !File.Exists(aion_exe)) // check game directory and aion.bin
            {
                // try to read aion path from registry setting
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\NCSoft\\Aion");
                if (key == null)
                    return false;
                else
                {
                    object install_path = key.GetValue("InstallPath");
                    if (install_path != null) // if we found the install path
                    {
                        set_game_path(install_path.ToString());
                        return true;
                    }
                    else
                        return false;
                }
            }
            else
            {   //check Chat.log, if it's not there create it
                if (!File.Exists(chat_log))
                {
                    StreamWriter w = new StreamWriter(chat_log);
                    w.Write("");
                    w.Close();
                }
            }
            return true;
        }

        
        public static void create_aion_config_file() // creates system.ovr file tells AION to log combat & chat
        {
            string ovr_file = get_game_path() + "/system.ovr";
            StreamWriter sw = new StreamWriter(ovr_file);
            sw.WriteLine("g_chatlog = \"1\"");
            sw.WriteLine("log_IncludeTime = \"1\"");
            sw.WriteLine("log_Verbosity = \"1\"");
            sw.WriteLine("log_FileVerbosity = \"1\"");
            sw.Close();
        }
    }
}
