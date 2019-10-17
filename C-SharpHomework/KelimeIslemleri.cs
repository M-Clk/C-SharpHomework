using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    class KelimeIslemleri
    {
        public KelimeIslemleri()
        {
        }
        const int  kelimeUzunlukSiniri= 49;
        public List<string> OkuKelime()
        {
            string tekSatir = File.ReadAllText(Program.kelimeDosyaAdi);
            string[] satirlar = tekSatir.Split('|');
            List<string> kelimeler = new List<string>();
            foreach (string item in satirlar)
                if (item.Length >= 2) kelimeler.Add(item.ToUpper());
            return kelimeler;
        }
        public List<int> OkuSkor()
        {
            string tekSatir = File.ReadAllText(Program.skorDosyaAdi);
            string[] satirlar = tekSatir.Split('|');
            List<int> skorlar = new List<int>();
            foreach (string item in satirlar)
            {
                int tempInt;
                if (int.TryParse(item, out tempInt))
                    skorlar.Add(tempInt);
            }
            skorlar.Sort();
            skorlar.Reverse();
            return skorlar;
        }

        public string EkleKelime(string kelime)
        {
            if (kelime.Length < 2) return "En az iki harfli bir kelime yazin.";
            if (OkuKelime().Contains(kelime.ToUpper())) return "Bu kelime zaten var.";
            File.AppendAllText(Program.kelimeDosyaAdi, kelime + "|");
            return null;
        }
        public string KelimeGetir()
        {
            int length=0;
            int count;
            Random random = new Random();
            if (Program.harfSayisi == 0)//Harf sayisi 0 ise rastgele bir harf sayisi uret
                count = random.Next(2, 51);
            else
                count = Program.harfSayisi;
            List<string> kelimeHavuzu = new List<string>(); //Verilen uzunlukta birden fazla kelime olabilir. O durumda hepsini bu havuza ekleyerek icinden rastgele bir kelime secerek gonder
            List<string> kelimeler = OkuKelime();
            for (int fark = 0; fark < kelimeUzunlukSiniri; fark++)//Veritabaninda verilen uzunlukta kelime olmayabilir. Bu durumda tum kelimelere bakarak verilen uzunluga en yakin kelime(leri) bul ve havuza ekle. 0 dan baslayarak kelime uzunlugu sinirina kadar tum kelimeleri tek tek test et
                foreach (string item in kelimeler)
                    if ((count - item.Length) == fark || (count - item.Length) == -fark)
                    {
                        if (length == 0)
                        {
                            length = item.Length;
                            kelimeHavuzu.Add(item);
                        }
                        else if (length == item.Length)
                            kelimeHavuzu.Add(item);
                    }
            if (kelimeHavuzu.Count > 0)
            {
                int randomIndex = random.Next(0, kelimeHavuzu.Count);
                return kelimeHavuzu[randomIndex];
            }
            else return null;
            
        }
        public void EkleSkor(int skor)
        {
            if(!OkuSkor().Contains(skor))
            File.AppendAllText(Program.skorDosyaAdi, skor.ToString() + "|");

        }
        public void SilKelime(int pastIndex, List<string> kelimeler)
        {
            string str = "";
            for (int i = 0; i < kelimeler.Count; i++)
            {
                if (i == pastIndex) continue;
                str += kelimeler[i]+"|";
            }
            File.WriteAllText(Program.kelimeDosyaAdi, str);
        }
       
        
    }

}
