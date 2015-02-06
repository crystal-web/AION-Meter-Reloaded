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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AIONMeter
{
    public partial class frmDetails : Form
    {
        public frmDetails()
        {
            InitializeComponent();
            load_players();
        }

        private void load_players()
        {
            foreach (Player player in Meter.active_meter.group.members.Values)
            {
                combobox_players.Items.Add(player.name);
            }
            if (combobox_players.Items.Count > 0)
                combobox_players.SelectedIndex = 0;
        }

        private void load_meter_data()
        {
            listView.Items.Clear();
            string name = combobox_players.Items[combobox_players.SelectedIndex].ToString();
            Player player = (Player)Meter.active_meter.group[name];
            foreach (Action action in player.details)
            {
                ListViewItem i = new ListViewItem(action.time.ToString());
                i.SubItems.Add(action.skill.name);
                i.SubItems.Add(action.who.name);
                i.SubItems.Add(action.target);

                string amount = "";
                switch (action.skill.sub_type)
                {
                    case SUB_TYPES.ATTACK:
                    case SUB_TYPES.DEBUFF:
                        amount += action.damage.ToString();
                        if (action.critical) amount += " CRITICAL!";
                        break;
                    case SUB_TYPES.HEAL:
                    case SUB_TYPES.BUFF:
                        amount += action.healing.ToString();
                        if (action.critical) amount += " CRITICAL!";
                        break;
                }
                i.SubItems.Add(amount);

                if (action.ticks != null)
                {
                    string ticks = "";
                    foreach (Int64 tick in action.ticks)
                    {
                        ticks += tick + ", ";
                    }
                    i.SubItems.Add(ticks);
                }
                listView.Items.Add(i);
            }

            if (listView.Items.Count > 0)
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            else
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void combobox_players_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_meter_data();
        }
    }
}
