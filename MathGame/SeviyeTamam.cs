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
    public partial class SeviyeTamam : Form
    {
        int yildizSayisi;
        int skor;
        public SeviyeTamam(int yildizSayisi, int skor)
        {
            this.yildizSayisi = yildizSayisi;
            this.skor = skor;
            InitializeComponent();
        }

        private void SeviyeTamamlandi_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = imageList1.Images[yildizSayisi - 1];
            if(Program.seviye==5)
            {
                SkorKaydet();
                lblMesaj.Text = "Tebrikler! Oyunu kazandınız. Skorunuz:"+Program.skor+" Daha yüksek bir skor için yeniden oynamak ister misiniz?";
                button1.Text = "Yeniden Oyna";
            }
            else
            lblMesaj.Text = "Tebrikler! "+Program.seviye+". seviyeyi geçtiniz." ;
        }

        private void btnDevam_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.seviye = (Program.seviye < 5) ? Program.seviye + 1 : 1;
            this.Close();

        }
        void SkorKaydet()
        {
            SkorIslemleri skorIslemleri = new SkorIslemleri();
            skorIslemleri.EkleSkor(skor);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
