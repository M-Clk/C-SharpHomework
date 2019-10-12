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

    public partial class AnaForm : Form
    {
        int kalan_saniye = 30;
        int[] rakamlar;
        int cevapHakki;
        SkorIslemleri skorIslemleri;
        public AnaForm()
        {
            InitializeComponent();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = (NumericUpDown)sender;
            numeric.Value = BasaDon((int)numeric.Value);
        }
        int BasaDon(int num)
        {
            if (num == 10)
                return 0;
            else if (num == -1)
                return 9;
            else return num;
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            Secenekler secenekler = new Secenekler();
            skorIslemleri = new SkorIslemleri();
            secenekler.ShowDialog();
            timer.Start();
            Sifirla();
        }

        void NumericUpDownOlustur()
        {
            for (int i = 0; i < Program.basamakSayisi; i++)
            {
                NumericUpDown numericUpDown = new NumericUpDown();
                numericUpDown.BackColor = Color.AliceBlue;
                numericUpDown.Font = new Font("Tahoma", 10F, FontStyle.Bold);
                numericUpDown.ForeColor = SystemColors.WindowText;
                numericUpDown.Minimum = -1;
                numericUpDown.ReadOnly = true;
                numericUpDown.Size = new Size(29, 24);
                numericUpDown.ValueChanged += new EventHandler(numericUpDown_ValueChanged);
                flowPanelSayi.Controls.Add(numericUpDown);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblKalanSure.Text = kalan_saniye + " Saniye";
            if (kalan_saniye > 0)
            {
                kalan_saniye--;
                lblKalanSure.ForeColor = Color.RoyalBlue;
            }
            else
            {
                timer.Stop();
                lblKalanSure.ForeColor = Color.Red;
                if (cevapHakki == 1)
                    OyunBitti(false);
                else
                    button1.Text = "Geç";
            }
        }
        bool HakAzalt()
        {
            if (--cevapHakki > 0)
            {
                lblHak.Text = cevapHakki.ToString();
                kalan_saniye = 30;
                timer.Start();
                button1.Text = "Sonuç";
                return true;
            }
            else return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SonucuGor();
        }
        void SonucuGor()
        {
            if (DogrulukTestEt() && kalan_saniye > 0)
                OyunBitti(true);
            else if (!HakAzalt())
                OyunBitti(false);
        }
        bool DogrulukTestEt()
        {
            int dogruSayisi = 0;
            for (int i = 0; i < rakamlar.Length; i++)
            {
                NumericUpDown numeric = (NumericUpDown)flowPanelSayi.Controls[i];
                if (numeric.Value == rakamlar[i])
                {
                    numeric.BackColor = Color.DodgerBlue;
                    dogruSayisi++;
                }
                else if (rakamlar.Contains((int)numeric.Value)) numeric.BackColor = Color.OrangeRed;
                else numeric.BackColor = Color.AliceBlue;
            }
            return dogruSayisi == rakamlar.Length;
        }
        int SkorGetir()
        {
            return cevapHakki * 100 / Program.cevapHakki * Program.basamakSayisi;
        }
        void OyunBitti(bool kazandiMi)
        {
            timer.Stop();
            string mesaj = "", baslik = "";
            if (kazandiMi)
            {
                skorIslemleri.EkleSkor(SkorGetir());
                mesaj = "Skorunuz: " + SkorGetir() + "\nOyunu kazandınız. Skorunuz kaydedildi. Bir oyun daha oynamak ister misiniz?";
                baslik = "Tebrikler!";
            }
            else
            {
                string sayi="";
                foreach (int item in rakamlar)
                {
                    sayi += item.ToString();
                }
                mesaj = "Cevap hakkınız doldu.\nDoğru cevap:" + sayi+ ".\nBir oyun daha oynamak ister misiniz?";
                baslik = "Oops!";
            }
            DialogResult dialogResult = MessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                Sifirla();
            else Application.Exit();
        }
        private void button16_Click(object sender, EventArgs e)
        {
            Sifirla();
        }
        void Sifirla()
        {
            flowPanelSayi.Controls.Clear();
            button1.Text = "Sonuç";
            kalan_saniye = 30;
            SkorYukle();
            SayisalIslemler sayisalIslemler = new SayisalIslemler();
            rakamlar = sayisalIslemler.SayiGetir();
            cevapHakki = Program.cevapHakki;
            lblHak.Text = cevapHakki.ToString();
            NumericUpDownOlustur();
        }
        void SkorYukle()
        {
            skorList.Items.Clear();
            foreach (int item in skorIslemleri.OkuSkor())
                skorList.Items.Add(item);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Secenekler secenekler = new Secenekler();
            ((Button)secenekler.Controls.Find("button1",true)[0]).Text = "Kaydet";
            ((Label)secenekler.Controls.Find("lblBilgi", true)[0]).Visible= false;
            secenekler.ShowDialog();
        }
    }
}
