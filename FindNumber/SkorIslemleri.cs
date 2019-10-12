using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindNumber
{
    class SkorIslemleri
    {
        private DosyaIslemleri dosyaIslemleri;
        public SkorIslemleri()
        {
            dosyaIslemleri = DosyaIslemleri.KurucuGetir();
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
            int temp = 0;
            for (int i = 0; i < karisikListe.Count; i++)
            {
                for (int j = i; j < karisikListe.Count; j++)
                {
                    if ((karisikListe[j] > karisikListe[i]) == buyuktenKucugeMi)
                    {
                        temp = karisikListe[i];
                        karisikListe[i] = karisikListe[j];
                        karisikListe[j] = temp;
                    }
                }
            }
        }

        public void EkleSkor(int skor)
        {
            if (!OkuSkor().Contains(skor))
                dosyaIslemleri.Ekle(Program.skorDosyaAdi, skor.ToString(), '|');
        }
    }
}
