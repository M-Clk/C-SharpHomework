using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
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
                int tempInt;
                if (int.TryParse(item, out tempInt))
                    skorlar.Add(tempInt);
            }
            skorlar.Sort();
            skorlar.Reverse();
            return skorlar;
        }

        public void EkleSkor(int skor)
        {
            if (!OkuSkor().Contains(skor))
                File.AppendAllText(Program.skorDosyaAdi, skor.ToString() + "|");
        }
    }
}
