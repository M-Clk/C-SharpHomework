using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathGame
{
    public partial class Secenekler : Form
    {
        public Secenekler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Secenekler_Load(object sender, EventArgs e)
        {
            comboIslemTuru.SelectedIndex=4;
        }
        private void Secenekler_FormClosing(object sender, FormClosingEventArgs e)
        {
            Random rdn = new Random();
            Program.islemTuruIndex = (comboIslemTuru.SelectedIndex == 4) ? rdn.Next(0, 4) : comboIslemTuru.SelectedIndex;
        }
    }
}
