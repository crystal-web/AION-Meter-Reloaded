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
using System.Net;
using System.IO;
using System.Threading;

// TODO remove or change this file ^^ Update test is run in Program.cs
namespace AIONMeter
{
    public static class Updater
    {
        public static void check(bool verbose) // check any updates
        {
            Thread t = new Thread(new ParameterizedThreadStart(check_for_updates));
            t.Start(verbose);
        }

        private static void check_for_updates(object verbose)
        {
            Version current_version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Version online_version = get_update_info();

            if (online_version.ToString() == "0.0.0.0") return; // if a version check returns 0.0.0.0, a firewall may be blocking our connection

            if (current_version == online_version && (bool)verbose){
                System.Windows.Forms.MessageBox.Show("You already use the latest version of the AIONMeter-Reloaded.", "Update Check", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else if (current_version < online_version)
            {
                if (System.Windows.Forms.MessageBox.Show("There is a new AIONMeter-Reloaded version avaible for download! Would you like to download it now?", "Update Check", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://github.com/crystal-web/AION-Meter-Reloaded/tree/master/Release");
                }
            }
        }

        private static Version get_update_info()
        {
            Int32 major = 0, minor = 0, build = 0, revision = 0;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/crystal-web/AION-Meter-Reloaded/master/Release/updates.txt");
                WebResponse response = request.GetResponse();
                StreamReader web_reader = new StreamReader(response.GetResponseStream());
                string line;
                while ((line = web_reader.ReadLine()) != null)
                {
                    string[] info = line.Split('=');
                    switch (info[0])
                    {
                        case "major":
                            major = Int32.Parse(info[1]);
                            break;
                        case "minor":
                            minor = Int32.Parse(info[1]);
                            break;
                        case "build":
                            build = Int32.Parse(info[1]);
                            break;
                        case "revision":
                            revision = Int32.Parse(info[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e) { }
            return new Version(major, minor, build, revision);
        }

        public static Version get_version()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}
