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
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

using System.Windows.Forms;

namespace AIONMeter
{
    public class Meter : IDisposable
    {        
        public enum MODE // Meter's mode
        {
            LIVE, // will watch the Chat.log file and parse it live
            PAUSED, // paused state for a live meter
            REPLAY, // meter will analyse a given log file
            REPLAY_DEBUG // additional to REPLAY mode,meter will output debug.log
        }

        public enum RENDER_MODE // Current rendering mode for meter
        {
            render_damage,
            render_healing
        }

        private LogWriter writer = LogWriter.Instance;

        public static Meter active_meter = null; // the active meter

        public MODE tracking_mode; // the parsing mode
        public RENDER_MODE render_mode; // the render mode
        public Thread thread; // meter's running thread
        public Parser parser; // the log parser
        public Group group; // the players group
        public LiveTracker log_tracker; // the live Chat.log watcher & tracker
        public OverTimeEffectTracker effect_tracker; // the DOT and HOT tracker
        public PetTracker pet_tracker; // the player pet's tracker
        private bool disposed = false;

        #region General Functions

        public Meter(MODE mode)
        {
            if (!Skills.loaded) Skills.init(); // if skill db not loaded yet do so

            if (Meter.active_meter != null) // dispose the previos meter and dealloc resources
                Meter.active_meter.Dispose();

            Meter.active_meter = this; // set the active meter            

            thread = new Thread(new ParameterizedThreadStart(run)); // run the meter on a seperate threat other than GUI thread
            try { thread.CurrentUICulture = new CultureInfo(Config.get_language()); } // set threads culture for localization of resources
            catch (Exception e) {
                StreamWriter debug_writer = new StreamWriter(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "/LogsException.log");
                debug_writer.WriteLine(e.Message);
                debug_writer.Close(); // close the stream
                debug_writer = null;
            }

            thread.Start(mode); // start the meter's own thread
        }

        private void run(object _mode)
        {
            tracking_mode = (MODE)_mode; // get the meter's mode

            parser = new Parser(this);
            group = new Group(); // the players group, with who use it
            effect_tracker = new OverTimeEffectTracker(); // the DOT and HOT tracker
            pet_tracker = new PetTracker(); // Pet Tracker            

            if (tracking_mode == MODE.LIVE)
            {
                // scan the previous lines of log file, to get the current group members
                if (Config.get_scan_previos_session_on_startup())
                {
                    Replay r = new Replay(Replay.MODE.IGNORE_COMBAT, Config.get_game_log_path());
                    r.Dispose();
                }
                log_tracker = new LiveTracker(); // init the log reader & file watcher
            }
            else if (tracking_mode == MODE.REPLAY)
            { 
                Replay r = new Replay(Replay.MODE.NORMAL, Config.get_game_log_path());
                r.Dispose();
            }
            else if (tracking_mode == MODE.REPLAY_DEBUG)
            { 
                Replay r = new Replay(Replay.MODE.DEBUG, Config.get_game_log_path());
                r.Dispose();
            }
        }

        public void pause()
        {
            if (tracking_mode == MODE.LIVE)
            {
                tracking_mode = MODE.PAUSED;
                log_tracker = null;
            }
        }

        public void resume()
        {
            if (tracking_mode == MODE.PAUSED)
            {
                tracking_mode = MODE.LIVE;
                log_tracker = new LiveTracker();
            }
        }

        ~Meter()
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
                    if (thread != null)
                    {
                        thread.Abort();
                        thread = null;
                    }
                    if (parser != null)
                    {
                        parser.Dispose();
                        parser = null;
                    }
                    if (group != null)
                    {
                        group.Dispose();
                        group = null;
                    }
                    if (effect_tracker != null)
                    {
                        effect_tracker.Dispose();
                        effect_tracker = null;
                    }
                    if (pet_tracker != null)
                    {
                        pet_tracker.Dispose();
                        pet_tracker = null;
                    }
                    if (log_tracker != null)
                    {
                        log_tracker.Dispose();
                        log_tracker = null;
                    }
                    Meter.active_meter = null;
                    GC.Collect();
                }
            }
            disposed = true;
        }

        #endregion

        #region Meter Functions
        private void commit_action(string time, string who, Int32 amount, string target, string skill, bool critical)
        {
            who = who.Trim();
            writer.WriteToLog("Meter.commit_action: " + time + " who:" + who + " amout:" + amount + " target:" + target + " skill:" + skill);
            


            if (group[who] != null) // check if he's in party
            {
                writer.WriteToLog("Meter.commit_action.group[who]: " + group[who].ToString());
                Action a = new Action(time, group[who], amount, target, skill, critical); // new combat action
                group[who].details.Add(a); // insert the action
            }
            else // a pet may also inflict the damage, so let PetTracker to check it
            {
                pet_tracker.commit_pet_action(time, who, amount, target, skill);
            }
        }

        public void reset_meter() // reset the meter statistics and group
        {
            group.reset();
            try
            {
                group[Properties.Resources.You].Clear(); // clear the players statistics
            }
            catch (Exception e) { }
        }

        public void calculate_statistics()
        {
            Int64 party_total = 0; // total party damage/healing for percent calculations

            if (group != null && group.members.Count > 0)
            {
                foreach (Player player in group.members.Values)
                {
                    switch (render_mode)
                    {
                        case RENDER_MODE.render_damage:
                            party_total += player.damage;
                            break;
                        case RENDER_MODE.render_healing:
                            party_total += player.healing;
                            break;
                    }
                }

                try
                {
                    foreach (Player player in group.members.Values) // calculate the statistics
                    {                        
                        TimeSpan t = new TimeSpan(0);
                        if (player.details.Count > 0)
                            t = DateTime.Now - player.details[0].time;

                        // DPS & HPS
                        player.DPS = (double)(player.damage / t.TotalSeconds);
                        if (double.IsNaN(player.DPS)) player.DPS = 0;
                        player.HPS = (double)(player.healing / t.TotalSeconds);
                        if (double.IsNaN(player.HPS)) player.HPS = 0;

                        // Burst values
                        Int32 i = 0;
                        Int32 burst_dmg_total=0;
                        Int32 burst_healing_total=0;
                        for (i = player.details.Count-1; i >= 0; i--)
                        {
                            double span = (DateTime.Now - player.details[i].time).TotalSeconds;
                            if (span > 0 && span < 5)
                            {
                                if (player.details[i].skill.sub_type == SUB_TYPES.HEAL)
                                    burst_healing_total += player.details[i].healing;
                                else
                                    burst_dmg_total += player.details[i].damage;
                            }
                            else if(span>5)
                                break;
                        }
                        player.burst_DPS = burst_dmg_total / 5;
                        player.burst_HPS = burst_healing_total / 5;

                        // percentage
                        switch (render_mode)
                        {
                            case RENDER_MODE.render_damage:
                                player.percent = (double)(player.damage * 100) / party_total;
                                break;
                            case RENDER_MODE.render_healing:
                                player.percent = (double)(player.healing * 100) / party_total;
                                break;
                        }
                        if (double.IsNaN(player.percent))
                        {
                            player.percent = 0; // if expression returns Not A Number just assign 0
                        }
                    }
                }
                catch (Exception e)
                {
                    writer.WriteToLog("Meter.calculate_statistics: " + e.Message);
                } // ignore division by zero exception
            }
            else
            {
                writer.WriteToLog("Meter.calculate_statistics !(group != null && group.members.Count > 0)");
            }
        }
        #endregion

        #region Parser Engine Callbacks
        public List<KeyValuePair<string, double>> get_sorted_list()
        {
            List<KeyValuePair<string, double>> sorted_list = new List<KeyValuePair<string, double>>();
            foreach (Player player in group.members.Values)
            {
                sorted_list.Add(new KeyValuePair<string, double>(player.name, player.percent));
            }

            sorted_list.Sort(
                delegate(KeyValuePair<string, double> v1, KeyValuePair<string, double> v2)
                {
                    return -Comparer<double>.Default.Compare(v1.Value, v2.Value);
                }
            );

            return sorted_list;
        }

        public void commit_damage(GroupCollection matches) // direct-damage
        {
            string time = matches["time"].Value.Trim();
            string who = matches["who"].Value.Trim();
            Int32 amount = Int32.Parse(Regex.Replace(matches["amount"].Value,@"[\D]","")); // AION puts speration character between each 3 digits, but it's somewhat not only a '.' or ','. So we're replacing any characters thats not a digit.                                         
            bool critical = false;
            if (matches["critical"].Value.ToString() == "Critical Hit!")
            {
                critical = true;
            }
            string target = matches["target"].Value.Trim();
            string skill = matches["skill"].Value.Trim();
            skill = skill.Replace('.', ' ').Trim(); // fix ending .

            // if no spell name is given, than it's a swing (auto-attack)
            if (skill.Trim().Length == 0)
            {
                skill = "Swing";
            } 

            commit_action(time, who, amount, target, skill,critical);
        }

        public void commit_dot_effect(GroupCollection matches) // damage over-time
        {
            string time = matches["time"].Value.Trim();
            string target = matches["target"].Value.Trim();
            Int32 amount = Int32.Parse(Regex.Replace(matches["amount"].Value, @"[\D]", ""));
            string skill = matches["skill"].Value.Trim();
            skill = skill.Replace('.', ' ').Trim(); // fix ending .
            Meter.active_meter.effect_tracker.apply_effect(time, target, amount, skill);
        }

        public void commit_healing(GroupCollection matches) // direct healing
        {
            string time = matches["time"].Value.Trim();
            string who = matches["who"].Value.Trim();
            Int32 amount = Int32.Parse(Regex.Replace(matches["amount"].Value, @"[\D]", ""));
            bool critical = false;
            string target;
            if (matches["target"].Success) // check if there was a healing target
                target = matches["target"].Value.Trim();
            else // if not it's a self-healing
                target = who;
            string skill = matches["skill"].Value.Trim();
            skill = skill.Replace('.', ' ').Trim(); // fix ending .
            commit_action(time, who, amount, target, skill,critical);
        }

        public void commit_hot_effect(GroupCollection matches) // healing over-time
        {
            string time = matches["time"].Value.Trim();
            string target = matches["target"].Value.Trim();
            bool critical = false;
            string who = "";
            if (matches["who"].Success)
                who = matches["who"].Value.Trim();
            Int32 amount;
            if (matches["amount"].Success)
                amount = Int32.Parse(Regex.Replace(matches["amount"].Value, @"[\D]", ""));
            else
                amount = 0;

            string skill = matches["skill"].Value.Trim();
            skill = skill.Replace('.', ' ').Trim(); // fix ending .

            if (who != "") // if who is missing that means with this line a HOT effect starts
                commit_action(time, who, amount, target, skill,critical);
            else
                Meter.active_meter.effect_tracker.apply_effect(time, target, amount, skill);
        }

        public void summon_pet(GroupCollection matches) // player's pet summons
        {
            string who = matches["who"].Value.Trim();
            if (group[who] != null)
            {
                string time = matches["time"].Value.Trim();
                string target = matches["target"].Value.Trim();
                string skill = matches["skill"].Value.Trim();
                skill = skill.Replace('.', ' ').Trim(); // fix ending .            
                group[who].summon_pet(time, target, skill);
            }
        }

        public void player_online(GroupCollection matches) // player is online, reset the statistics
        {
            reset_meter();
        }

        public void group_disbanded(GroupCollection matches)
        {
            group.reset();
        }

        public void group_message(GroupCollection matches)
        {
            string who = matches["who"].Value.Trim();
            string message = matches["message"].Value.Trim();
            if(message.Contains("<SyncAMGroup>")) // group-sync macro triggered by a group member
                group.sync_group(Group.MESSAGE_SENDER.group_member,who,message);
            else
                group.join(matches["who"].Value.Trim());
        }

        public void self_message(GroupCollection matches)
        {
            string self = matches["who"].Value.Replace(":","").Trim();
            string message = matches["message"].Value.Trim();
            if(message.Contains("<SyncAMGroup>")) // group-sync macro triggered by self
                group.sync_group(Group.MESSAGE_SENDER.self,self,message);
        }

        public void member_left(GroupCollection matches)
        {
            group.leave(matches["who"].Value.Trim());
        }
        #endregion
    }
}
