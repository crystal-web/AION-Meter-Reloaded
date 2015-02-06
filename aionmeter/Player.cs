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
    public class Player : IDisposable
    {
        public string name;
        public Int32 damage = 0;
        public Int32 healing = 0;
        public Int32 peak_damage = 0;
        public Int32 peak_healing = 0;
        public double DPS = 0;
        public double burst_DPS = 0;
        public double HPS = 0;
        public double burst_HPS = 0;
        public List<Action> details;
        public List<Pet> pets;
        public double percent = 0;
        public System.Drawing.Color color;
        private bool disposed = false;

        public Player(string name)
        {

            LogWriter writer = LogWriter.Instance;
            writer.WriteToLog("Player.Player: " + name);

            this.name = name;
            details = new List<Action>();
            if (name == Properties.Resources.You) {
                color = System.Drawing.ColorTranslator.FromHtml("#8F1327");
            }
            else
            {
                color = System.Drawing.ColorTranslator.FromHtml("#425B8C");
            }
        }

        public void summon_pet(string time, string target, string skill)
        {
            if (pets == null) pets = new List<Pet>(); // if the player summons first pet, prepare the structure
            if (pets.Count > 0) Meter.active_meter.pet_tracker.remove((Pet)pets[pets.Count - 1]); //if players has already summoned pet, first remove the last pet from Pet Tracker

            Pet p = new Pet(target, this); // new pet
            pets.Add(p); // add to players pet list
            Meter.active_meter.pet_tracker.track(p); // track the pet

            Action a = new Action(time, this, 0, target, skill, false); // insert the summon skill also as an action
            details.Add(a);
            if (DebugLog.on)
            {
                if (null != a.ToString()){
                    DebugLog.write_line(a.ToString());
                }
            }
        }

        public void Clear()
        {
            details.Clear();
            damage = 0;
            percent = 0;
        }

        ~Player()
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
                    if (details != null)
                    {
                        foreach (Action a in details)
                        {
                            a.Dispose();
                        }
                        details.Clear();
                        details = null;
                    }
                    if (pets != null)
                    {
                        foreach (Pet p in pets)
                        {
                            p.Dispose();
                        }
                        pets.Clear();
                        pets = null;
                    }
                }
            }
            disposed = true;
        }
    }
}
