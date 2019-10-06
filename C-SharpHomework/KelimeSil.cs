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
    public partial class KelimeSil : Form
    {
        KelimeIslemleri kelimeIslemleri;
        List<string> kelimeler;
        public KelimeSil()
        {
            InitializeComponent();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                kelimeIslemleri.SilKelime(listBox1.SelectedIndex,kelimeler);
                KelimeYukle();
            }
                
        }

        private void KelimeSil_Load(object sender, EventArgs e)
        {
            KelimeYukle();
        }
        void KelimeYukle()
        {
            listBox1.Items.Clear();
            kelimeIslemleri = new KelimeIslemleri();
            kelimeler = kelimeIslemleri.OkuKelime();
            foreach (string item in kelimeler)
            {
                listBox1.Items.Add(item);
            }
        }
    }
}
