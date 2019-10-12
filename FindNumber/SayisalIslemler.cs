using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindNumber
{
    class SayisalIslemler
    {
        int[] rakamlar;
        Random random;
        int index;

        public SayisalIslemler()
        {
            random = new Random();
        }
        int RakamUret()
        {
            int randomSayi = random.Next(0, 10);
            if (Program.tekrarliMi)
                return randomSayi;
            while (rakamlar.Contains(randomSayi))randomSayi = random.Next(0, 10);//0 dizide tum elemanlarin varsayilan degeri old. son elemanda sonsuz donguye griyor. Duzelt
            return randomSayi;
        }
        public int[] SayiGetir()
        {
            Sifirla();
            bool ozelDurum = Program.basamakSayisi == 10 && !Program.tekrarliMi;
            int basamakSayisi = ozelDurum ? 9 : Program.basamakSayisi;
            for (int i = 0; i <basamakSayisi; i++)
            {
                rakamlar[i] = RakamUret();
                index++;
            }if(ozelDurum)
            SonElemaniDegistir();//0 varsayilan old. eger 10 basamakli ve tekrarsiz olmasi isteniyorsa son eleman 0 olur ve RakamUret methodunda sonsuz donguye girilir. Buna karsi boyle bir ozel duruma karsi son eleman dongude hesap edilmez diger 9 basamak rastgele atanir son eleman 0 olarak kaldigi icin  o da rastgele istenen bir indexli eleman ile degisir ve %100 rastgele olusturulmus bir 10 elemanli rakam dizisi elde edilmis olur.
            return rakamlar;
        }
        public void SonElemaniDegistir()
        {
            int randomIndex = random.Next(0, 10);
            rakamlar[9] = rakamlar[randomIndex];
            rakamlar[randomIndex] = 0;
        }
        void Sifirla()
        {
            rakamlar = null;
            GC.Collect();//Kullanici baska bir oyuna gectiginde basamak sayisini degistirmis olma olasiligindan dolayi bir onceki diziyi gargabe collector ile manuel olarak bellekten sildik. null bir deger atayarak referansini kaybetmesini sagladik. Aksi halde silemezdik.
            rakamlar = new int[Program.basamakSayisi];//Basamak sayisi kadar bir rakamlar dizisi olusturduk.
        }
    }
}
