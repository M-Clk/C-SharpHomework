using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    class KelimeIslemleri
    {
        private DosyaIslemleri dosyaIslemleri;
        public KelimeIslemleri()
        {
            dosyaIslemleri = DosyaIslemleri.KurucuGetir();
        }

        public List<string> OkuKelime()
        {
            string tekSatir = dosyaIslemleri.Oku(Program.kelimeDosyaAdi);
            string[] satirlar = tekSatir.Split('|');
            List<string> kelimeler = new List<string>();
            foreach (string item in satirlar)
                if (item.Length >= 2) kelimeler.Add(item.ToUpper());
            return kelimeler;
        }
        public List<int> OkuSkor()
        {
            string tekSatir = dosyaIslemleri.Oku(Program.skorDosyaAdi);
            string[] satirlar = tekSatir.Split('|');
            List<int> skorlar = new List<int>();
            foreach (string item in satirlar)
            {
                int tempInt;
                if (int.TryParse(item, out tempInt))
                    skorlar.Add(tempInt);
            }
            SkorSirala(skorlar, Program.buyuktenKucugeMi);
            return skorlar;
        }
        public void SkorSirala(List<int> karisikListe, bool buyuktenKucugeMi)
        {
            int temp=0;
            for (int i = 0; i < karisikListe.Count; i++)
            {
                for (int j = i; j < karisikListe.Count; j++)
                {
                    if ((karisikListe[j] > karisikListe[i])==buyuktenKucugeMi) {
                        temp = karisikListe[i];
                        karisikListe[i] = karisikListe[j];
                        karisikListe[j] = temp;
                    }
                }
            }
        }
         
        public string EkleKelime(string kelime)
        {
            if (kelime.Length < 2) return "En az iki harfli bir kelime yazin.";
            if (OkuKelime().Contains(kelime.ToUpper())) return "Bu kelime zaten var.";
            dosyaIslemleri.Ekle(Program.kelimeDosyaAdi, kelime, '|');
            return null;
        }
        public string KelimeGetir()
        {
            int length=0;
            int count;
            Random random = new Random();
            if (Program.harfSayisi == 0)
                count = random.Next(2, 51);
            else
                count = Program.harfSayisi;
            List<string> kelimeHavuzu = new List<string>();
            List<string> kelimeler = OkuKelime();
            for (int fark = 0; fark < 49; fark++)
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
            dosyaIslemleri.Ekle(Program.skorDosyaAdi,skor.ToString(),'|');
        }
        public void SilKelime(int pastIndex, List<string> kelimeler)
        {
            string str = "";
            for (int i = 0; i < kelimeler.Count; i++)
            {
                if (i == pastIndex) continue;
                str += kelimeler[i]+"|";
            }
            dosyaIslemleri.Guncelle(Program.kelimeDosyaAdi, str);
        }
       
        
    }

}
