using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    class DosyaIslemleri
    {
        public bool KayitliSorulariYukle()
        {
            return false;
        }
        private static DosyaIslemleri instance;

        private FileStream fileStream;
        private StreamWriter streamWriter;

        private DosyaIslemleri()
        {
            DosyaKontrol(Program.kayitliAyarDosyaAdi);
            DosyaKontrol(Program.skorDosyaAdi);
        }

        public static DosyaIslemleri KurucuGetir()
        {
            if (instance == null) instance = new DosyaIslemleri();
            return instance;
        }

        public string Oku(string dosyaAdi)
        {
                string str = File.ReadAllText(dosyaAdi);
                return str;
        }
        public void Ekle(string dosyaAdi, string value, char ayrac)
        {
            using (fileStream = new FileStream(dosyaAdi, FileMode.Append))
            {
                streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(value + ayrac.ToString());
                streamWriter.Flush();
                fileStream.Close();
            }
        }
        public void Guncelle(string dosyaAdi, string yeniVeri)
        {
            using (streamWriter = new StreamWriter(dosyaAdi))
            {
                streamWriter.Write(yeniVeri);
                streamWriter.Flush();
                fileStream.Close();
            }
        }
        private void DosyaKontrol(string dosyaAdi)
        {
            if (!File.Exists(dosyaAdi))
            {
               fileStream= File.Create(dosyaAdi);
                fileStream.Close();
            }
        }

    }
    
}
