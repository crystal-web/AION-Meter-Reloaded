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
using System.Windows.Forms;

namespace AIONMeter
{
    public class LiveTracker : IDisposable // File Reader
    {
        private StreamReader reader; // the file reader
        private FileWatcher watcher; // the file change watcher
        private bool disposed = false;

        public LiveTracker()
        {
            string file = Config.get_game_path() + "/Chat.log";
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); // open the file unlocked
                reader = new StreamReader(fs, System.Text.Encoding.Default);
                reader.ReadToEnd(); // set position to end
                watcher = new FileWatcher(file_changed); // start the filewatcher
            }
            catch (Exception e)
            {
                // TODO Show config path
                MessageBox.Show("The log file " + file + " can not be found! Please set your AION path", 
                    "Log file error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void file_changed(object source, FileSystemEventArgs e) // on file change
        {
            read_changes(); // read the latest changes
        }

        private void read_changes() // read the latest changes
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Replace('\uFFFD', '\''); // temprorary fix for character \u2019 which rendered as \uFFFD (non-printable character)
                Meter.active_meter.parser.parse_line(line); // let the parse engine check the line

            }
        }

        ~LiveTracker()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // the GC shouldn't try to finalize the object as disposed it
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                    watcher.Dispose();
                }
            }
            disposed = true;
        }

    }
}
