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
    public partial class AnaForm : Form
    {
        int hak;
        int toplamHak;
        string sorulanKelime;
        int skor;
        private KelimeIslemleri kelimeIslemleri;
        public AnaForm()
        {
            InitializeComponent();
        }

        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (KelimeEkle ekle = new KelimeEkle())
                ekle.ShowDialog();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (KelimeSil sil = new KelimeSil())
                sil.ShowDialog();
        }
        private void HarfTiklanmasi(object sender, EventArgs e)
        {
            if (!Program.oynaniyorMu) return;
            Button button = (Button)sender;
            button.Enabled = false;
            HarfKontrolu(button.Text);
            
        }
        private void HarfKontrolu(string harf)
        {
            bool flag = false;
            foreach (Label item in flowPanelKelimeler.Controls)
            {
                if(item.Tag.Equals(char.Parse(harf)))
                {
                    item.Text = harf;
                    flag = true;
                }
            }
            hak--;
            lblHak.Text = "Cevap Hakkınız: " + hak;
            if (!flag) 
            {
                YanlisHarfGirisi(harf);
                skor--;
            }

            else if (KazandiMi())
                OyunBitti(true);
            if (hak == 0) OyunBitti(false);
        }
        
        void YanlisHarfGirisi(string harf)
        {
            lblYanlisGirilen.Text += lblYanlisGirilen.Text.Equals("") ? harf : ", " + harf;
            int imageIndex = (toplamHak-hak)* 11 / toplamHak;
            pictureBox1.Image = stepImagesList.Images[imageIndex];
        }
        void OyunBitti(bool kazandiMi)
        {
            string mesaj = "", baslik="";
            
            if (KazandiMi())
            {
                if (hak == 0) hak++;
                kelimeIslemleri.EkleSkor(SkorGetir());
                mesaj = "Skorunuz: " + SkorGetir() + "\nOyunu kazandınız. Skorunuz kaydedildi. Bir oyun daha oynamak ister misiniz?";
                baslik = "Tebrikler!";
            }
            else
            {
                mesaj = "Cevap hakkınız doldu. Bir oyun daha oynamak ister misiniz?";
                baslik = "Oops!";
            }
            DialogResult dialogResult = MessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                Sifirla();
            else Application.Exit();
        }
        int SkorGetir()
        {
            int hakYuzdesi = hak * 100 / toplamHak;
            int skorYuzedesi = skor * 100 / toplamHak;
            return hakYuzdesi*skorYuzedesi;
        }
        bool KazandiMi()
        {
            bool flag = true;
            foreach (Label item in flowPanelKelimeler.Controls)
            {
                if (item.Text.Equals("_"))
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }
        void Sifirla()
        {
            pictureBox1.Image = null;
            SkorYukle();
            flowPanelKelimeler.Controls.Clear();
            ButonAktiflestir(flowLayoutPanel1);
            ButonAktiflestir(flowLayoutPanel2);
            if (KelimeYukle())
            {
                KelimeGoster();
                Program.oynaniyorMu = true;
            }
            
            lblYanlisGirilen.Text = "";
            

        }
        void ButonAktiflestir(FlowLayoutPanel flowLayoutPanel)
        {
            foreach (Control item in flowLayoutPanel.Controls)
                item.Enabled = true;
        }
        bool KelimeYukle()
        {
            sorulanKelime = kelimeIslemleri.KelimeGetir();

            if (sorulanKelime == null)
            {
                DialogResult dialogResult = MessageBox.Show("Veritabanında kelime bulunamadı. Kelime eklemek ister misiniz?", "Uyari", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                    using (KelimeEkle ekle = new KelimeEkle())
                        ekle.ShowDialog();
                return false;
            }
            return true;
        }
        

        private void AnaForm_Load(object sender, EventArgs e)
        {
            using (Acilis acilis = new Acilis())
                acilis.ShowDialog();
            kelimeIslemleri = new KelimeIslemleri();
            Sifirla();
        }

        void KelimeGoster()
        {
            hak = 0;
            int fontSize = (sorulanKelime.Length < 30) ? 18 : 10;
            int width = (sorulanKelime.Length < 30) ? 25 : 20;
            int heigth = (sorulanKelime.Length < 30) ? 40 : 30;

            foreach (char item in sorulanKelime)
            {
                Label tempLabel = new Label();
                tempLabel.Text = (item == ' ') ? " " : "_";
                tempLabel.Tag = item;
                tempLabel.Width = width;
                tempLabel.Height = heigth;
                tempLabel.Font = new Font(tempLabel.Font.Name, fontSize, FontStyle.Bold);
                flowPanelKelimeler.Controls.Add(tempLabel);
                if (!(item == ' ')) hak++;
            }
            hak += 2;
            toplamHak = hak;
            skor = hak;
            lblHak.Text= "Cevap Hakkınız: "+hak;
        }
        void SkorYukle()
        {
            skorList.Items.Clear();
            foreach (int item in kelimeIslemleri.OkuSkor())
                skorList.Items.Add(item);
        }

        private void harfSayısıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Acilis acilis = new Acilis())
                acilis.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Sifirla();
        }
    }
}
