using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HangMan
{
    public partial class Acilis : Form
    {
        public Acilis()
        {
            InitializeComponent();
        }

        private void chkRastgele_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Visible = !chkRastgele.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!chkRastgele.Checked)
                Program.harfSayisi = numericUpDown1.Value > 50 ? 50 : (int)numericUpDown1.Value;
            else Program.harfSayisi = 0;
            this.Close();
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 50) numericUpDown1.Value = 50;
        }
    }
}
