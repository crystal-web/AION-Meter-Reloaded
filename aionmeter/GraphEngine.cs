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
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AIONMeter
{
    public static class GraphEngine
    {
        public static void paint_meter(Graphics graphics, PaintEventArgs e, PictureBox Scene)
        {
            Meter.active_meter.calculate_statistics(); // recalculate meter statistics

            Image bar_texture=null;
            Player player=null;
            ImageAttributes attr=null;
            ColorMatrix cm;
            string total="";
            string peak_value = "";
            string str_ps = "";
            string str_ps_value = "";
            string str_burst = "";

            Font font_bold = new Font(Config.get_font().FontFamily, 9, FontStyle.Bold);
            Font font_normal = new Font(Config.get_font().FontFamily, 7, FontStyle.Regular);            
            SolidBrush text_brush = new SolidBrush(Config.get_color());            

            int area_width = Scene.ClientRectangle.Width; // calculate the usable area
            int area_height = Scene.ClientRectangle.Height;
            int y_offset = 0; // bar starting offset;
            int bar_height = 32;

            try
            {
                bar_texture = Config.get_texture();
                foreach (KeyValuePair<string, double> pair in Meter.active_meter.get_sorted_list()) // get a sorted list
                {
                    player = (Player)Meter.active_meter.group[pair.Key]; // get the player                        
                    Int32 bar_width = Convert.ToInt32(player.percent * area_width) / 100; // the bar width based on percentage

                    // Colorize the texture using ColorMatrix                        
                    attr = new ImageAttributes();
                    cm = get_colormatrix(player.color);
                    attr.SetColorMatrix(cm);

                    // Draw the bar
                    graphics.DrawImage(bar_texture, new Rectangle(0, y_offset, bar_width, bar_height), 0, 0, (bar_texture.Width * (float)player.percent) / 100, bar_texture.Height, GraphicsUnit.Pixel, attr);

                    // Draw the text
                    graphics.DrawString(player.name, font_bold, text_brush, 0, y_offset); // draw the player_name                    

                    switch (Meter.active_meter.render_mode)
                    {
                        case Meter.RENDER_MODE.render_damage:
                            peak_value = player.peak_damage.ToString();
                            str_ps = "dps";
                            str_ps_value = player.DPS.ToString("#0");
                            str_burst = player.burst_DPS.ToString("#0") + " burst";
                            total = player.damage.ToString() + " total - " + player.percent.ToString("#0.00") + "% / ";
                            break;
                        case Meter.RENDER_MODE.render_healing:
                            peak_value = player.peak_healing.ToString();
                            str_ps = "hps";
                            str_ps_value = player.HPS.ToString("#0");
                            str_burst = player.burst_HPS.ToString("#0") + " burst";  
                            total = player.healing.ToString() + " total - " + player.percent.ToString("#0.00") + "% / ";
                            break;
                    }

                    SizeF str_damge_size = graphics.MeasureString(total, font_normal); // damage & healing done
                    graphics.DrawString(total, font_normal, text_brush, 0, y_offset + 16);
                    peak_value += " peak";
                    graphics.DrawString(peak_value, font_normal, text_brush, str_damge_size.Width, y_offset + 16);
                    SizeF ps_size = graphics.MeasureString(str_ps, font_normal);
                    SizeF ps_value_size = graphics.MeasureString(str_ps_value, font_bold);
                    graphics.DrawString(str_ps, font_normal, text_brush, area_width - ps_size.Width, y_offset + 2);
                    graphics.DrawString(str_ps_value, font_bold, text_brush, area_width - (ps_value_size.Width + ps_size.Width), y_offset);
                    SizeF burst_size = graphics.MeasureString(str_burst, font_normal);
                    graphics.DrawString(str_burst, font_normal, text_brush, area_width - burst_size.Width, y_offset + 16);
                    y_offset += bar_height + 1; // increment the offset for next bar

                    attr.Dispose();
                    cm = null;
                }
            }
            catch (Exception) { } // an meter update may broke the traversing
            finally
            {
                font_bold.Dispose();
                font_bold = null;
                font_normal.Dispose();
                font_normal = null;
                text_brush.Dispose();
                text_brush = null;
                if(bar_texture!=null)
                    bar_texture.Dispose();
                bar_texture = null;
                cm = null;
                if(attr!=null)
                    attr.Dispose();
                attr = null;
                total = null;
                peak_value = null;
                str_ps = null;
                str_ps_value = null;
                str_burst = null;
                total = null;
                GC.Collect();
            }
        }

        public static ColorMatrix get_colormatrix(Color c)
        {
            float red = (float)(c.R - Color.DarkGray.R) / 255;
            float green = (float)(c.G - Color.DarkGray.B) / 255;
            float blue = (float)(c.B - Color.DarkGray.B) / 255;
            float alpha = (float)c.A / 255;
            return new ColorMatrix(new float[][]
            {
                new float[]{1,0,0,0,0},
                new float[]{0,1,0,0,0},
                new float[]{0,0,1,0,0},
                new float[]{0,0,0,1,0},
                new float[]{red,green,blue,alpha,1}
            });
        }
    }
}
