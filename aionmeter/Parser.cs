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

/* Regex based line parser */

namespace AIONMeter
{
    public class Parser : IDisposable
    {
		private LogWriter writer = LogWriter.Instance;

        public enum MODE
        {
            NORMAL,
            IGNORE_COMBAT
        }
        public List<Filter> filters; // the list of regex patterns
        public MODE mode = MODE.NORMAL;
        private bool disposed = false;

        public Parser(Meter meter)
        {
            // Add the regex patterns and the callback functions for a succesfull match
            filters = new List<Filter>();

            // Filter(string pattern, delegate_callback _callback, Boolean _combat_filter)


            // damage filters
            filters.Add(new Filter(
				"(?<time>\\d{4}.\\d{2}.\\d{2} \\d{2}:\\d{2}:\\d{2}) : (?<critical>(Coup critique !)*)(?<who>.*?)( a| avez) infligé (?<amount>\\d+(.\\d{3})*) points de dégâts (|critiques )à (?<target>.*).", 
                new Filter.delegate_callback(meter.commit_damage),
                true)
                );
            /*
            filters.Add(new Filter(Properties.Resources.FILTER_DIRECT_DAMAGE, new Filter.delegate_callback(meter.commit_damage), true));
            // damage overtime-effects filters
            filters.Add(new Filter(Properties.Resources.FILTER_DOT, new Filter.delegate_callback(meter.commit_dot_effect), true));
            // healing filters
            filters.Add(new Filter(Properties.Resources.FILTER_DIRECT_HEALING_SKILL, new Filter.delegate_callback(meter.commit_healing), true));
            filters.Add(new Filter(Properties.Resources.FILTER_DIRECT_HEALING, new Filter.delegate_callback(meter.commit_healing), true));
            // healing over-time effects
            filters.Add(new Filter(Properties.Resources.FILTER_HOT_1, new Filter.delegate_callback(meter.commit_hot_effect), true));
            filters.Add(new Filter(Properties.Resources.FILTER_HOT_2, new Filter.delegate_callback(meter.commit_hot_effect), true));
            // pet summons
            filters.Add(new Filter(Properties.Resources.FILTER_SUMMON_1, new Filter.delegate_callback(meter.summon_pet), true));
            filters.Add(new Filter(Properties.Resources.FILTER_SUMMON_2, new Filter.delegate_callback(meter.summon_pet), true));

            // Group-awareness filters
            filters.Add(new Filter(Properties.Resources.FILTER_PLAYER_ONLINE, new Filter.delegate_callback(meter.player_online)));
            filters.Add(new Filter(Properties.Resources.FILTER_PLAYER_JOIN_GROUP, new Filter.delegate_callback(meter.group_message)));
            filters.Add(new Filter(Properties.Resources.FILTER_PLAYER_LEFT_GROUP, new Filter.delegate_callback(meter.member_left)));
            filters.Add(new Filter(Properties.Resources.FILTER_PLAYER_KICKED, new Filter.delegate_callback(meter.member_left)));
            filters.Add(new Filter(Properties.Resources.FILTER_GROUP_DISBAND, new Filter.delegate_callback(meter.group_disbanded)));
            filters.Add(new Filter(Properties.Resources.FILTER_GROUP_MESSAGE, new Filter.delegate_callback(meter.group_message)));
            filters.Add(new Filter(Properties.Resources.FILTER_SELF_MESSAGE, new Filter.delegate_callback(meter.self_message))); 
            //*/
        }

        public void parse_line(string line)
        {

            if (line.Trim().Length > 0) // filter out any empty lines
            {
                bool ignore_combat = false;
                if (mode == MODE.IGNORE_COMBAT)
                {
					writer.WriteToLog("Parser/parse_line: ignore_combat: true");
                    ignore_combat = true;
                }
                    


                try
                {
                    foreach (Filter f in filters) // try filters on the line
                    {


                        if (!ignore_combat || (ignore_combat && !f.combat_filter)) // ingore_combat may be state dropping of combat message lines.
                        {
                             // until a match
                            if (f.Run(line))
                            {
                                break;
                            }
						} else {writer.WriteToLog("Parser/parse_line: ignore_combat: true no check Filter");}
                    }
                }
                catch (Exception e)
                {
                    if (DebugLog.on)
                    {
                        DebugLog.write_line(e.Message);
                    }
                    
                }
            }
        }

        ~Parser()
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
                    if (filters != null)
                    {
                        foreach (Filter f in filters)
                        {
                            f.Dispose();
                        }
                        filters.Clear();
                        filters = null;
                    }
                }
            }
            disposed = true;
        }
    }
}
