using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }
        int[] karisik_dizi;
        PictureBox seciliPictureBox;
        bool pictureBoxTiklanabilir;
        int seciliOyuncu;
        bool bildi;
        int acilanCiftSayisi;

        void KapakResimleriGoster()
        {
            IEnumerable<PictureBox> pictureBoxDizisi = picturePanel.Controls.OfType<PictureBox>();
            foreach (PictureBox item in pictureBoxDizisi)
                if(item.Enabled)
                item.Image = imageList1.Images[20];
        }
        void TumResimleriEtkinlestir()
        {
            IEnumerable<PictureBox> pictureBoxDizisi = picturePanel.Controls.OfType<PictureBox>();
            foreach (PictureBox item in pictureBoxDizisi)
                item.Enabled = true;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;
            if (!pictureBoxTiklanabilir) return;
            picture.Image = imageList1.Images[(int)picture.Tag];
            if (seciliPictureBox == null)
            {
                picture.Image = imageList1.Images[(int)picture.Tag];
                seciliPictureBox = picture;
                timer1.Start();
            }
            else
            {
                if (seciliPictureBox.Tag.Equals(picture.Tag))
                {
                    bildi = true;
                    picture.Enabled = false;
                    seciliPictureBox.Enabled = false;
                    if (PuanArttir())
                        OyunBitti(false);
                    else if (acilanCiftSayisi==20)
                        OyunBitti(true);
                    else
                    timer1.Stop();
                }
                else
                {
                    
                    seciliPictureBox.Image = imageList1.Images[20];
                    picture.Image = imageList1.Images[20];
                    timer1.Stop();
                }
                seciliPictureBox = null;
                
                SeciliOyunucuDegistir();
                bildi = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                KapakResimleriGoster();
                seciliPictureBox = null;
                timer1.Stop();
            if(pictureBoxTiklanabilir)
            SeciliOyunucuDegistir();
            else pictureBoxTiklanabilir = true;
            
        }
        void ResimleriKaristir()
        {
            for (int i = 0; i < 40; i = i + 20)
                for (int j = 0; j < 20; j++)
                {
                    PictureBox picture = (PictureBox)this.Controls.Find("pictureBox" + karisik_dizi[j + i], true)[0];
                    picture.Image = imageList1.Images[j];
                    picture.Tag = j;//esi olan picturebox idsi
                    Image image = imageList1.Images[0];
                }
        }
        void RastgeleIndexDizisiOlustur()
        {
            Random random = new Random();
            int[] dizi = Enumerable.Range(0, 40).ToArray(); //0 dan 39 a kadar sirali dizi olusturuldu.
            karisik_dizi = dizi.OrderBy(x => random.Next(0, 40)).ToArray(); //olusturlan dizi linq yardimiyla karistirldi.
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Sifirla();
        }
        void OyuncuLabelRenkleriTersCevir()
        {
            Label lbl = seciliOyuncu == 1 ? lblOyuncu1 : lblOyuncu2;
            Color tempColor = lbl.BackColor;
            lbl.BackColor = lbl.ForeColor;
            lbl.ForeColor = tempColor;
            lbl = seciliOyuncu == 1 ? lblOyuncu2 : lblOyuncu1;
            tempColor = lbl.BackColor;
            lbl.BackColor = lbl.ForeColor;
            lbl.ForeColor = tempColor;
        }
        void SeciliOyunucuDegistir()
        {
            if (bildi) return;
            seciliOyuncu = seciliOyuncu == 1 ? 2 : 1;
            OyuncuLabelRenkleriTersCevir();
        }
        bool PuanArttir()
        {
            Label lbl = (Label)tableLayoutPanel3.Controls.Find("lblPuan" + seciliOyuncu, true)[0];
            int dogruSayisi = int.Parse(lbl.Text) + 1;
            lbl.Text = dogruSayisi.ToString();
            if (dogruSayisi > 10) return true;
            else return false;
        }
        void OyunBitti(bool berabereBitti)
        {
            string mesaj = berabereBitti ? "Oyun berabere bitti." : "Tebrikler. "+seciliOyuncu+". oyuncu kazandı.";
            DialogResult dialogResult = MessageBox.Show(mesaj+" Tekrar oynamak ister misiniz?", "Oyun Bitti", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Sifirla();
            }
            else Application.Exit();
        }
        void Sifirla()
        {
            lblPuan1.Text = "0";
            lblPuan2.Text = "0";
            bildi = false;
            TumResimleriEtkinlestir();
            RastgeleIndexDizisiOlustur();
            ResimleriKaristir();
            seciliOyuncu = 1;
            timer1.Stop();//Yeniden baslatilan durumlarda...
            timer1.Start();
            pictureBoxTiklanabilir = false;
            lblOyuncu1.ForeColor = Color.AliceBlue;
            lblOyuncu2.BackColor = Color.AliceBlue;
            lblOyuncu1.BackColor = SystemColors.HotTrack;
            lblOyuncu2.ForeColor = SystemColors.HotTrack;
            buttonBasla.Enabled = false;
            acilanCiftSayisi = 0;
        }
    }
}
