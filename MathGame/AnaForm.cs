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
    public partial class AnaForm : Form
    {
        int kalan_saniye=60;
        int blokIndexBaslangici = 0;
        bool pasHakki = true;
        SayisalIslemler sayisalIslemler;
        DosyaIslemleri dosyaIslemleri;
        SkorIslemleri skorIslemleri;
        public AnaForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (kalan_saniye > 0)
                BlokAtla();
            else SeviyeSonu();
        }
        void Kaybetti(int dogruSayisi)
        {
            DialogResult dialogResult = MessageBox.Show("Malesef kazanamadınız. Bu seviyeyi tekrar oynamak ister misiniz?", "Oops!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Sifirla();
            }
            else Application.Exit();
        }

        void SeviyeSonu()
        {
            int dogruSayisi = 0;
            if ((dogruSayisi = DogruSayisiHesapla()) >= 11)
                SeviyeTamamlandi(dogruSayisi);
            else Kaybetti(dogruSayisi);
        }

        void BlokAtla()
        {
            if (CevapKontrolEt(true))
            {
                if (IlkYanlisIndexGetir() == -1)
                    SeviyeTamamlandi(20);
                if (blokIndexBaslangici != 15)
                {
                    blokIndexBaslangici += 5;
                    SorulariYukle();
                }
                else if (pasHakki && IlkYanlisIndexGetir() < 15)
                    PasGec();
                else SeviyeSonu();
            }
        }
        void PasGec()
        {
            if (pasHakki && kalan_saniye > 0)
            {
                CevapKontrolEt(false);
                if (blokIndexBaslangici == 15)
                {
                    int yanlisIndex = IlkYanlisIndexGetir();
                    blokIndexBaslangici = yanlisIndex / 5;
                    blokIndexBaslangici *= 5;
                    pasHakki = false;
                    btnPas.Enabled = false;
                }
                else blokIndexBaslangici += 5;
                SorulariYukle();
            }
        }
        void Sifirla()
        {
            btnPas.Enabled = true;
            pasHakki = true;
            blokIndexBaslangici = 0;
            lblKalanSure.Text = "";
            lblUyari.Visible = false;
            TextboxEnableDegistir(true);
            kalan_saniye = 50 * Program.seviye + (Program.islemTuruIndex + 1) * 10;
            Program.sorular = sayisalIslemler.SoruDizisiGetir();
            SorulariYukle();
            timer.Start();
            SkorYukle();
        }
        void IlerlemeKaydet(int dogruSayisi)
        {
            DosyaIslemleri dosyaIslemleri = DosyaIslemleri.KurucuGetir();
            dosyaIslemleri.Guncelle(Program.kayitliAyarDosyaAdi,Program.seviye+"|"+SkorGetir(dogruSayisi));
        }
        void SeviyeTamamlandi(int dogruSayisi)
        {
            timer.Stop();
            int yildiz;
            if (dogruSayisi <= 15)
                yildiz = 1;
            else if (dogruSayisi <= 18)
                yildiz = 2;
            else yildiz = 3;
            SeviyeTamam tamam = new SeviyeTamam(yildiz,SkorGetir(dogruSayisi));
            DialogResult sonuc = tamam.ShowDialog();
            IlerlemeKaydet(dogruSayisi);
            if( sonuc== DialogResult.No)
                Application.Exit();
            Sifirla();
            
        }
        int IlkYanlisIndexGetir()
        {
            for (int i = 0; i < Program.sorular.Length; i++)
            {
                if (Program.sorular[i].Contains("false")) return i;
            }
            return -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PasGec();
        }

        private bool CevapKontrolEt(bool renklendir)
        {
            bool bayrak = true;
            for (int i = 0; i < 5; i++)
            {
                TextBox textBox = (TextBox)this.Controls.Find("txtCevap" + i, true)[0];
                string dogruCevapTxt = Program.sorular[i + blokIndexBaslangici].Split('=')[1];
                Label label = (Label)this.Controls.Find("lblSoru" + i, true)[0];
                decimal cevap = 0;
                decimal.TryParse(textBox.Text, out cevap);
                if (decimal.Parse(dogruCevapTxt).Equals(cevap)) 
                {
                    Program.sorular[i + blokIndexBaslangici] = Program.sorular[i + blokIndexBaslangici].Replace("false", "true");
                    label.ForeColor = SystemColors.HotTrack;
                }
                else
                {
                    if (renklendir)
                    {
                        label.ForeColor = Color.Red;
                        lblUyari.Visible = true;
                    }
                    else label.ForeColor = SystemColors.HotTrack;
                    bayrak = false;
                }
            }
            return bayrak;
        }
        int DogruSayisiHesapla()
        {
            int dogruSayisi = 0;
            foreach (string item in Program.sorular)
                if (item.Contains("true")) dogruSayisi++;
            return dogruSayisi;
        }
        private void txtCevap_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('-') && !e.KeyChar.Equals(',');
        }

        void SorulariYukle()
        {
           lblUyari.Visible = false;
            lblSeviye.Text = Program.seviye + ". Seviye";
            lblBlok.Text = "Sayfa: " + ((blokIndexBaslangici / 5) + 1) + "/4";
            for (int i = 0; i < 5; i++)
            {
                TextBox textBox = (TextBox)this.Controls.Find("txtCevap" + i, true)[0];
                string[] soruTamHali = Program.sorular[i + blokIndexBaslangici].Split('=');
                Label label = (Label)this.Controls.Find("lblSoru" + i, true)[0];
                label.Text = soruTamHali[0];
                if (soruTamHali[2].Equals("true"))
                    textBox.Text = soruTamHali[1];
                else textBox.Clear();
            }
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            sayisalIslemler = new SayisalIslemler();
            dosyaIslemleri = DosyaIslemleri.KurucuGetir();
            skorIslemleri = new SkorIslemleri();
            Secenekler secenekler = new Secenekler();
            secenekler.ShowDialog();
            Sifirla();
        }
        void SkorYukle()
        {
            skorList.Items.Clear();
            foreach (int item in skorIslemleri.OkuSkor())
                skorList.Items.Add(item);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            lblKalanSure.Text = "Kalan süre: " + kalan_saniye + " saniye";
            if (kalan_saniye > 0)
            {
                kalan_saniye--;
                lblKalanSure.ForeColor = SystemColors.HotTrack ;
            }
            else
            {
                timer.Stop();
                lblKalanSure.ForeColor = Color.Red;
                TextboxEnableDegistir(false);
                CevapKontrolEt(false);
                SeviyeSonu();
            }
        }
        int SkorGetir(int dogruSayisi)
        {
            int skor = dogruSayisi*5* (Program.islemTuruIndex + Program.seviye);
            return skor;
        }
        void TextboxEnableDegistir(bool enabled)
        {
            for (int i = 0; i < 5; i++)
            {
                TextBox textBox = (TextBox)this.Controls.Find("txtCevap" + i, true)[0];
                textBox.Enabled = enabled;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Sifirla();
        }
    }
}
