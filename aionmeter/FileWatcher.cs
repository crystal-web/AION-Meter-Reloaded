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
    class FileWatcher : IDisposable
    {
        private FileSystemWatcher watcher;
        private bool disposed = false;

        public FileWatcher(FileSystemEventHandler file_changed_func)
        {
            watcher = new FileSystemWatcher();  // init the filesystem watcher
            watcher.Path = Config.get_game_path(); // the directory
            watcher.NotifyFilter = NotifyFilters.LastWrite; // just check last write attribute
            watcher.Filter = "Chat.log"; // the file to watch for
            watcher.Changed += new FileSystemEventHandler(file_changed_func); // the callback function
            watcher.EnableRaisingEvents = true; // start watching
        }

        ~FileWatcher()
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
                    watcher.EnableRaisingEvents = false;
                    watcher.Dispose();
                    watcher = null;
                }
            }
            disposed = true;
        }
    }
}
