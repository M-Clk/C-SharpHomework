using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindNumber
{
    public partial class Secenekler : Form
    {
        public Secenekler()
        {
            InitializeComponent();
        }
        int maximum = 30;
        private void numericUpDown_Press(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); //Sadece sayi girilmesi icin...
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            NumericUpDown numeric = (NumericUpDown)sender;
            if (numeric.Value > maximum) numeric.Value = maximum;
            if (numeric.Value < 1) numeric.Value = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CikisYap();
        }
        void CikisYap()
        {
            Program.cevapHakki = (int)numericCevapHakki.Value;
            Program.basamakSayisi = (int)numericBasamakSayisi.Value;
            Program.tekrarliMi = Convert.ToBoolean(comboTekrarliMi.SelectedIndex);
            this.Close();
        }

        private void Secenekler_Load(object sender, EventArgs e)
        {
            VarsayilanlariYukle();   
        }
        void VarsayilanlariYukle()
        {
            comboTekrarliMi.SelectedIndex = Convert.ToInt32(Program.tekrarliMi);
            numericBasamakSayisi.Value = Program.basamakSayisi;
            numericCevapHakki.Value = Program.cevapHakki;
        }

        private void comboTekrarliMi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTekrarliMi.SelectedIndex == 0)
            {
                maximum = 10;
                numericBasamakSayisi.Maximum = 10;
                numericUpDown1_KeyUp(numericBasamakSayisi, new KeyEventArgs(Keys.Enter));
            }
            else
            {
                maximum = 30;
                numericBasamakSayisi.Maximum = 30;
            }
        }
    }
}
