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
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Security.Principal;
using System.Net;

namespace AIONMeter
{
    static class Program
    { 
        public static IniFile iniFile;
        // Guid de AIONMeter-Reloaded
        public static readonly Guid guid = // (Guid)System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), true)[0];
            new Guid("3b48a4dc-8c28-440a-a687-3c449676132c");

        public static frmMeter main_window;
        public static Boolean logEvent = true;
        public static LogWriter writer = LogWriter.Instance;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

			iniFile = new IniFile ();
			try {
				iniFile.IniFilePath("data/" + Config.get_language() + ".ini");
			} catch(FileNotFoundException) {
				using (WebClient Client = new WebClient ())
				{
					// Client.DownloadFile("http://www.abc.com/file/song/a.mpeg", "a.mpeg");
				}
			}

            // Pas lancé deux fois ^^
            using (Mutex mutex = new Mutex(false, "Global\\" + guid))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                writer.WriteToLog("Running application guid:" + guid + " version:" + Application.ProductVersion);

                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Can not run multi instance off this AIONMeter-Reloaded");
                    return;
                }

                if (!isAdministrator())
                {
                    DialogResult result = MessageBox.Show(
                        "Unauthorized Access, please run this application in Administrator mode",
                        "Unauthorized Access",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1
                    );
                    return;
                }

                isConfigReady(); // void

                // https://raw.githubusercontent.com/crystal-web/AION-Meter-Reloaded/master/Release/updates.txt
                // Application.ProductVersion


                // Try to switch to configured locale
                try { Thread.CurrentThread.CurrentUICulture = new CultureInfo(Config.get_language()); }
                catch (Exception)
                {
                    MessageBox.Show("The language " + Config.get_language() + " couldn't be loaded. Reverting culture to en-US.",
                        "Language Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Config.set_language("en-US");
                }

                // Game splash ^^
                frmSplashscreen splash = new frmSplashscreen();

                


                minifyLog(0);

                splash.hideSplash();

                main_window = new frmMeter();
                Application.Run(main_window);

            }
        }

        /**
         * TODO Devrait-on pas vidé le fichier de log ? maxLineCount à 0 pour vidé
         */
        static void minifyLog(int maxLineCount)
        {
            string logSourcePath = Config.get_game_path();
            string logCopyPath = logSourcePath + "/logs";
            string destLog = logCopyPath + "/Chat-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".log";


            string[] lines = File.ReadAllLines(logSourcePath + "/Chat.log");                // Fichier log => array
            string[] linesToCopy = new string[maxLineCount];                                // Futur fichier log => array
            // Si le nombre est suppérieur a maxLineCount
            if (lines.Length > maxLineCount)
            {
                // On test si le dossier logs, ou stocké le fichier log, n'existe pas
                if (!System.IO.Directory.Exists(logCopyPath))
                {
                    // On le créer
                    System.IO.Directory.CreateDirectory(logCopyPath);
                }
                // Copie le fichier de base dans le dossier logs
                File.Copy(logSourcePath + "/Chat.log", destLog);
                // On garde les "maxLineCount" lignes du fichier logs...
                Array.Copy(lines, lines.Length - maxLineCount, linesToCopy, 0, maxLineCount);
                // On ecrire le fichier log
                File.WriteAllLines(logSourcePath + "/Chat.log", linesToCopy);
            }
        }

        /**
         * Test is administrator ?
         * app.manifest include ?
         */
        static bool isAdministrator() {
            bool isElevated;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            if (!isElevated)
            {
                return false;
                // Force close 
                // Process.GetCurrentProcess().Kill();
            }
            return true;
        }

        static void isConfigReady()
        {
            Config.set_application_path(Application.StartupPath);
            Updater.check(false); // check for updates

            if (!Config.settings_upgraded())
            {
                Config.upgrade_settings();
            }

            if (!Config.game_path_exists()) // if AION path is not configured yet, do so
            {
                MessageBox.Show("Please set the path for your AION installation", "Need AION path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                frmConfig cfg = new frmConfig();
                cfg.ShowDialog();
            }
        }

        static void versionControl()
        {
            // get the running version
            // Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string curVersion = Application.ProductVersion;
            string lastBuildVersion = (new System.Net.WebClient()).DownloadString("https://raw.githubusercontent.com/crystal-web/AION-Meter-Reloaded/master/Release/updates.txt");

            var version1 = new Version(curVersion);
            var version2 = new Version(lastBuildVersion);

            var result = version1.CompareTo(version2);
            if (result > 0)
            {
                writer.WriteToLog("curVersion is greater, are you developper ? ^^");
            }
            else if (result < 0)
            {
                writer.WriteToLog("lastBuildVersion is greater");
                if (System.Windows.Forms.MessageBox.Show(
                    "There is a new AIONMeter-Reloaded version avaible for download! Would you like to download it now?", 
                    "Update Check", 
                    System.Windows.Forms.MessageBoxButtons.YesNo, 
                    System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes
                ) {
                    System.Diagnostics.Process.Start("https://github.com/crystal-web/AION-Meter-Reloaded/tree/master/Release");
                }
            }
            else
            {
                writer.WriteToLog("versions are equal");
            }
        }
    }
}
