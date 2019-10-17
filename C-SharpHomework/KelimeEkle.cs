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
    public partial class KelimeEkle : Form
    {
        public KelimeEkle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kaydet();
        }
        void Kaydet()
        {
            KelimeIslemleri kelimeIslemleri = new KelimeIslemleri();
            string sonuc = kelimeIslemleri.EkleKelime(textBox1.Text);
            if (sonuc == null)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show(sonuc);
                textBox1.Clear();
                textBox1.Select();
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar)) || (char.ToUpper(e.KeyChar) == 'X') || (char.ToUpper(e.KeyChar) == 'W') || (char.ToUpper(e.KeyChar) == 'Q') ;

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Kaydet();
        }
    }
}
