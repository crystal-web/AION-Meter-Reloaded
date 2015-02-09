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

namespace AIONMeter
{
    public class Replay : IDisposable // Replays a log file
    {
        public enum MODE
        {
            NORMAL,
            IGNORE_COMBAT,
            DEBUG
        }

        public MODE mode;
        private StreamReader reader;
        private bool file_exists = true;
        private bool disposed = false;
        public delegate void delegate_notify_progress(int progress); // notifies progress
        public event delegate_notify_progress notify_progress;
        public delegate void delegate_notify_max(string status, int max); // notifies line count of the log file
        public event delegate_notify_max notify_max;
        public delegate void delegate_notify_finished(); // notifies that job is finished
        public event delegate_notify_finished notify_finished;

        public Replay(MODE _mode, string replay_file)
        {
            mode = _mode;

            if (mode == MODE.DEBUG)
            {
                DebugLog.start();
            }

            if (mode == MODE.IGNORE_COMBAT)
            {
                Meter.active_meter.parser.mode = Parser.MODE.IGNORE_COMBAT;
            }

            try
            {
                FileStream fs = new FileStream(replay_file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                reader = new StreamReader(fs, System.Text.Encoding.Default);
                notify_progress += Program.main_window.notify_progress_bar; // bind the notifier events
                notify_max += Program.main_window.notify_progress_bar_max;
                notify_finished += Program.main_window.notify_progress_bar_finished;
                count_lines();
                start();
            }
            catch (FileNotFoundException e)
            {
                file_exists = false;
            }
            finally
            {
                if(Meter.active_meter.parser!=null)
                    Meter.active_meter.parser.mode = Parser.MODE.NORMAL;
            }
        }

        private void start()
        {
            int i = 1;
            string line;

            if (file_exists)
            {
                while ((line = reader.ReadLine()) != null) // read till end line by line
                {
                    line = line.Replace('\uFFFD', '\''); // temprorary fix for character \u2019 which rendered as \uFFFD (non-printable character)

                    if (DebugLog.on)
                    {
                        DebugLog.write_line("[" + line + "]");
                    }

                    Meter.active_meter.parser.parse_line(line); // parse all data including combat messages
                    notify_progress(i);
                    i++;
                }
                notify_finished();
                reader.Close();
                reader = null;
            }
            if (DebugLog.on) DebugLog.close();
        }

        public void count_lines()
        {
            string[] all_lines = reader.ReadToEnd().Split(new char[] { '\n' }); // first find the line count
            reader.BaseStream.Seek(0, SeekOrigin.Begin); // rewind cursor position to the start

            if (DebugLog.on)
            {
                DebugLog.write_line("Input file has a total of " + all_lines.Length + " lines.");
            }
            notify_max("Reading previos session..", all_lines.Length); // notify about the line count
        }

        ~Replay()
        {
            Dispose(true);
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
                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                    notify_progress = null;
                    notify_max = null;
                    notify_finished = null;
                }
            }
            disposed = true;
        }
    }
}
