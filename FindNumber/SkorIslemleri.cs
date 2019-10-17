using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindNumber
{
    class SkorIslemleri
    {
        public List<int> OkuSkor()
        {
            string tekSatir = File.ReadAllText(Program.skorDosyaAdi);
            string[] satirlar = tekSatir.Split('|');
            List<int> skorlar = new List<int>();
            foreach (string item in satirlar)
            {
                int tempInt; //Dosyaya mudahale edilebilir. O yuzden dosyayi tryParse ile atama yap
                if (int.TryParse(item, out tempInt))
                    skorlar.Add(tempInt);
            }
            skorlar.Sort(); //Kucukten buyuge sirala
            skorlar.Reverse(); //Siralamayi ters cevir
            return skorlar;
        }

        public void EkleSkor(int skor)
        {
            if (!OkuSkor().Contains(skor))
                File.AppendAllText(Program.skorDosyaAdi, skor.ToString() + "|");
        }
    }
}
