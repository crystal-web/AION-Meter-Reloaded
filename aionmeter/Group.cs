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
using System.Text.RegularExpressions;

namespace AIONMeter
{
    public class Group : IDisposable
    {
        public static Regex SyncAMGroupMacro=new Regex(@"\(m:(?<name>.*?),p:(?<pet>.*?)\)", RegexOptions.Compiled);

        public enum MESSAGE_SENDER
        {
            self,
            group_member
        }

        public Hashtable members;
        private bool disposed = false;

        public Group()
        {
            members = new Hashtable();
            members.Add(Program.iniFile.IniReadValue("i18n", "You"), new Player(Program.iniFile.IniReadValue("i18n", "You")));
        }

        public Player this[string name] // the indexer
        {
            get
            {
                if ((Player)members[name] != null)
                    return (Player)members[name];
                else
                    return null;
            }
        }

        public void clear() // clear all players data
        {
            foreach (Player player in members.Values)
            {
                player.damage = 0;
                player.healing = 0;
                if (player.details != null)
                {
                    foreach (Action a in player.details)
                    {
                        a.Dispose();
                    }
                    player.details.Clear();
                }
            }
        }

        public void reset() // resets the group
        {
            List<Player> members_to_remove = new List<Player>();

            foreach (Player player in members.Values) // we have to keep a second list as members_to_deleted list, as deleting items while traversing collection is not possible
            {
                if (player.name != Program.iniFile.IniReadValue("i18n", "You")) members_to_remove.Add(player);
            }
            foreach (Player player in members_to_remove)
            {
                player.Dispose();
                members.Remove(player.name);
            }
        }

        public void join(String name)
        {
            try
            {
                Player p = new Player(name);
                members.Add(name, p);
            }
            catch (Exception e) { }
        }

        public void leave(String name)
        {
            if (name != Program.iniFile.IniReadValue("i18n", "You"))
            {
                try
                {
                    members.Remove(name);
                }
                catch (Exception e) { }
            }
            else
                reset();
        }

        public void sync_group(MESSAGE_SENDER sender,string who, string data)
        {
            // This function should be implemented by care at it may cause meter crashing if not handled correct
            // if the sender of the group message is a group-member, the meter should ask if to sync.

            if (data.StartsWith("<SyncAMGroup>"))
            {
                MatchCollection matches = SyncAMGroupMacro.Matches(data);
                foreach (Match m in matches)
                {
                    string player = "";
                    string pet = "";
                    switch (m.Groups[1].Value)
                    {
                        case "[%Group1]":
                        case "[%Group2]":
                        case "[%Group3]":
                        case "[%Group4]":
                        case "[%Group5]":
                            break;
                        default:
                            player = m.Groups[1].Value;
                            join(player);
                            break;
                    }

                    switch (m.Groups[2].Value)
                    {
                        case "[%Pet1]":
                        case "[%Pet2]":
                        case "[%Pet3]":
                        case "[%Pet4]":
                        case "[%Pet5]":
                            break;
					default:
						pet = m.Groups [2].Value;
						if (player != null) { 
							this [player].summon_pet (DateTime.Now.ToString ("yyyy.MM.dd hh:mm:ss"), pet, "Summon"); // TODO, pet should summon with the right skill not with general "Summon" :D
						}
                            break;
                    }
                }
            }
        }

        ~Group()
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
                    if (members != null)
                    {
                        foreach (Player p in members.Values)
                        {
                            p.Dispose();
                        }
                        members.Clear();
                        members = null;
                    }
                }
            }
            disposed = true;
        }
    }
}
