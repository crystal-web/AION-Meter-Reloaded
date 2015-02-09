using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AIONMeter
{
    public partial class frmSyncMacro : Form
    {
        public frmSyncMacro()
        {
            InitializeComponent();
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtMacro.Text);
        }
    }
}
