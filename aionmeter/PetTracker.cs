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

namespace AIONMeter
{
    public class PetTracker : IDisposable
    {
        public List<Pet> active_pets;
        private bool disposed = false;

        public PetTracker()
        {
            LogWriter writer = LogWriter.Instance;
            
            // init the structure
            if (active_pets == null)
            {
                writer.WriteToLog("PetTracker.PetTracker: init the structure");
                active_pets = new List<Pet>();
            }
            else
            {
                writer.WriteToLog("PetTracker.PetTracker: reset the structure");
                active_pets.Clear(); // reset if already inited
            }
        }

        public void track(Pet p)
        {
            LogWriter writer = LogWriter.Instance;
            writer.WriteToLog("PetTracker.track: " + p.ToString());
            active_pets.Add(p);
        }

        public void remove(Pet p) // Players may summon new pets, so they should be removed from Pet Tracker
        {
            LogWriter writer = LogWriter.Instance;
            writer.WriteToLog("PetTracker.remove: " + p.ToString());
            active_pets.Remove(p);
        }

        public void commit_pet_action(string time, string who, Int32 amount, string target, string skill)
        {
            LogWriter writer = LogWriter.Instance;
            writer.WriteToLog("PetTracker.commit_pet_action: " + time + " who:" + who + " amout:" + amount + " target:" + target + " skill:" + skill + " active_pets.Count:" + active_pets.Count);

            if (Properties.Resources.You == who && (active_pets == null || active_pets.Count == 0) )
            {
                track(new Pet(who, new Player(who)));
                // track(who, new Player(who));
            }

            if (active_pets != null && active_pets.Count > 0)
            {
                writer.WriteToLog("PetTracker.commit_pet_action.(active_pets != null && active_pets.Count > 0): ");
                foreach (Pet pet in active_pets)
                {
                    if (pet.name == who)
                    {
                        writer.WriteToLog("PetTracker.commit_pet_action.(pet.name == who): ");
                        pet.commit_action(time, who, amount, target, skill);
                        break;
                    }
                }
            }
        }

        ~PetTracker()
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
                    if (active_pets != null)
                    {
                        foreach (Pet p in active_pets)
                        {
                            p.Dispose();
                        }
                        active_pets.Clear();
                        active_pets = null;
                    }
                }
            }
            disposed = true;
        }
    }
}
