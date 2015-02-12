using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIONMeter
{
    public partial class frmSplashscreen : Form
    {
        public frmSplashscreen()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        public void hideSplash()
        {
            System.Threading.Thread.Sleep(5000);

            do
            {
                this.Opacity -= 0.25;
                System.Threading.Thread.Sleep(50);
            } while (this.Opacity == 0);

            this.Close();
        }

        private void onMouseClickEvent(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("http://crystal-web.org/dev/AIONMeter-Reloaded");
        }
    }
}
