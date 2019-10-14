using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    class SayisalIslemler
    {
        Random random;
        public SayisalIslemler()
        {
            random = new Random();
        }
        int RastgeleSayiUret()
        {
            switch (Program.seviye)
            {
                case 1:
                    return random.Next(0,10);
                case 2:
                    return random.Next(10, 100);
                case 3:
                    return random.Next(100, 1000);
                case 4:
                    return random.Next(1000, 10000);
                default:
                    return random.Next(10000, 100000);
            }
        }
       public string[] SoruDizisiGetir()
        {
            string[] sorular = new string[20];
            for (int i = 0; i < 20; i++)
            {
                int ilkSayi = RastgeleSayiUret();
                int ikinciSayi;
                while ((ikinciSayi = RastgeleSayiUret()) == 0 && Program.islemTuruIndex==3) { }
                decimal cevap;
                switch (Program.islemTuruIndex)
                {
                    case 0:
                        cevap = ilkSayi + ikinciSayi;
                        break;
                    case 1:
                        cevap = ilkSayi - ikinciSayi;
                        break;
                    case 2:
                        cevap = ilkSayi * ikinciSayi;
                        break;
                    default:
                        cevap = (decimal)ilkSayi / (decimal)ikinciSayi;
                        break;
                }
                sorular[i] = ilkSayi + Program.islemler[Program.islemTuruIndex] + ikinciSayi+"="+cevap.ToString("0.##")+"=false";
            }
            return sorular;
        }
    }
}
