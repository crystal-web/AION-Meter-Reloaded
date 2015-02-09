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
using System.Collections;

namespace AIONMeter
{
    public class Pet : IDisposable
    {
        public string name;
        public Int32 damage;
        public Player owner;
        private bool disposed = false;

        public Pet(string _name, Player _owner)
        {
            name = _name;
            damage = 0;
            owner = _owner;
        }

        public void commit_action(string time, string who, Int32 amount, string target, string skill)
        {
            Action a = new Action(time, owner, amount, target, skill,false);
            owner.details.Add(a);
            if (DebugLog.on)
            {
                if (null != a.ToString())
                {
                    DebugLog.write_line(a.ToString());
                }
            }
        }
        
        public override string ToString()
        {
            return name + " damaged " + damage + " owner:" + owner;
        }

        ~Pet()
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
                    name = null;
                    owner = null;
                }
            }
            disposed = true;
        }
    }
}
