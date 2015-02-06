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
    public class OverTimeEffectTracker : IDisposable
    {
        public List<Action> list;
        private bool disposed = false;

        public OverTimeEffectTracker()
        {
            if (list == null) // init the structure
                list = new List<Action>();
            else
                list.Clear(); // reset if already inited
        }

        public void track(Action a)
        {
            list.Add(a);
        }

        public void stop()
        {
            list.Clear();
        }

        public void apply_effect(string _time, string _target, Int32 _amount, string _skill)
        {
            DateTime time = DateTime.Parse(_time);
            System.Diagnostics.Debug.WriteLine(_skill);
            Skill skill = (Skill)Skills.list[_skill];

            List<Action> completed_effects = new List<Action>(); // the list for completed effects after applied tick

            foreach (Action action in list)
            {
                TimeSpan ts = time - action.last_tick; // timespan between action's last tick and current effect's time
                if (ts.Seconds + 1 >= skill.effect_tick && // if it's in the range of skills effect_period and effect_tick intervals
                     action.skill == skill &&  // if skill's match
                     action.target == _target) // if target's match
                {
                    if (action.ticks == null) // if no ticks happened yet
                        action.ticks = new List<Int32>(); // prepare the list

                    action.ticks.Add(_amount); // let the tick happen
                    action.last_tick = time;

                    if (skill.is_overtime_effect)
                    {
                        if (skill.effect_type == EFFECT_TYPES.HEAL)
                            action.who.healing += _amount;
                        else
                            action.who.damage += _amount;
                    }

                    if (action.ticks.Count >= skill.maximum_ticks) // check if effect ticks is complete
                        completed_effects.Add(action);

                    if (DebugLog.on)
                    {
                        if (null != action.last_tick_ToString())
                        {
                            DebugLog.write_line(action.last_tick_ToString());
                        }
                    }

                    break;
                }
            }

            // remove the completed effects
            foreach (Action action in completed_effects)
            {
                list.Remove(action);
            }
        }

        ~OverTimeEffectTracker()
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
                    if (list != null)
                    {
                        foreach (Action a in list)
                        {
                            a.Dispose();
                        }
                        list.Clear();
                        list = null;
                    }
                }
            }
            disposed = true;
        }

    }
}
