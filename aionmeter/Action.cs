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
    public class Action : IDisposable// Combat Action
    {
        public DateTime time;
        public Player who;
        public string target;
        public Skill skill;
        public Int32 damage;
        public Int32 healing;
        public List<Int32> ticks;
        public DateTime last_tick;
        public bool critical=false;
        private bool disposed = false;

        public Action(string _time, Player _who, Int32 _amount, string _target, string _skill, bool _critical)
        {
            LogWriter writer = LogWriter.Instance;

            try
            {
                time = DateTime.Parse(_time);
                who = _who;
                target = _target;
                critical = _critical;

                skill = (Skill)Skills.list[_skill];
                writer.WriteToLog("Action.Action: " + time + " who:" + who + " target:" + target + " skill:" + skill);

                switch (skill.sub_type)
                {
                    case SUB_TYPES.ATTACK:
                    case SUB_TYPES.DEBUFF:
                        damage = _amount;
                        who.damage += _amount;
                        if (_amount > who.peak_damage)
                            who.peak_damage = _amount;
                        break;
                    case SUB_TYPES.HEAL:
                    case SUB_TYPES.BUFF:
                        healing = _amount;
                        who.healing += _amount;
                        if (_amount > who.peak_healing)
                            who.peak_healing = _amount;
                        break;
                    default:
                        writer.WriteToLog("Action.Action: UNKNOWN SKILL IN DB " + skill);
                        break;
                }

                if (skill.is_overtime_effect) // if it's an overtime effect
                {
                    last_tick = time; //set the last_tick value to inital damage's time
                    Meter.active_meter.effect_tracker.track(this);
                }
            }
            catch (Exception e)
            {
                writer.WriteToLog("Action.Action: >> Exception: time:" + _time + " who:" + who.name + " amount:" + _amount + " target:" + _target + " skill:" + _skill);
            }
        }

        public override string ToString()
        {
            LogWriter writer = LogWriter.Instance;
            string tmp = "";
            try
            {
                tmp += ">> ";
                tmp += "[who: " + who.name + "] ";
                tmp += "[target: " + target + "] ";
                tmp += "[skill: " + skill.name;

                switch (skill.sub_type)
                {
                    case SUB_TYPES.ATTACK:
                    case SUB_TYPES.DEBUFF:
                        tmp += " (ATTACK)] [amount: " + damage + "]";
                        break;
                    case SUB_TYPES.HEAL:
                        tmp += " (HEAL)] [amount: " + healing + "]";
                        break;
                }

                if (critical)
                {
                    tmp += " [CRITICAL]";
                }

                writer.WriteToLog("Action.ToString: " + tmp);
                return tmp;
            }
            catch (Exception e) 
            {
                writer.WriteToLog("Action.ToString: >> Exception: " + e.Message);
                return "Action.ToString: >> Exception: " + e.Message;
            }            
        }

        public string last_tick_ToString()
        {
            LogWriter writer = LogWriter.Instance;
            string tmp = "";
            try
            {
                tmp += ">> ";
                tmp += "[who: " + who.name + "] ";
                tmp += "[target: " + target + "] ";
                tmp += "[skill: " + skill.name;

                if (skill.is_overtime_effect)
                {
                    switch (skill.sub_type)
                    {
                        case SUB_TYPES.ATTACK:
                        case SUB_TYPES.DEBUFF:
                            tmp += " (ATTACK)] [amount: " + damage + "]";
                            break;
                        case SUB_TYPES.HEAL:
                            tmp += " (HEAL)] [amount: " + healing + "]";
                            break;
                    }
                }
                writer.WriteToLog("Action.last_tick_ToString: " + tmp);
                return tmp;
            }
            catch (Exception e)
            {
                writer.WriteToLog("Action.last_tick_ToString: >> Exception: " + e.Message);
                return "Action.last_tick_ToString: >> Exception: " + e.Message;
            }
        }

        ~Action()
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
                    if (ticks != null)
                    {
                        ticks.Clear();
                        ticks = null;
                    }
                }
            }
            disposed = true;
        }
    }
}
