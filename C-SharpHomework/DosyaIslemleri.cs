using System;
using System.IO;
using System.Windows.Forms;

namespace HangMan
{
    public class DosyaIslemleri
    {
        private static DosyaIslemleri instance;

        private FileStream fileStream;
        private StreamWriter streamWriter;
        private StreamReader streamReader;

        private DosyaIslemleri()
        {
            DosyaKontrol(Program.kelimeDosyaAdi);
            DosyaKontrol(Program.skorDosyaAdi);
        }
   
        public static DosyaIslemleri KurucuGetir() 
        {
            if (instance == null) instance = new DosyaIslemleri();
            return instance;
        }

        public string Oku(string dosyaAdi)
        {
            using (fileStream = new FileStream(dosyaAdi, FileMode.Open))
            {
                streamReader = new StreamReader(fileStream);
                string str = streamReader.ReadToEnd();
                fileStream.Close();
                return str;
            }
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
                File.Create(dosyaAdi);
        }
    }
}
