using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HangMan
{
    static class Program
    {
        public static string kelimeDosyaAdi = "dbWords.clk";
        public static string skorDosyaAdi = "scores.clk";
        public static bool buyuktenKucugeMi = true;
        public static int harfSayisi = 0;
        public static bool oynaniyorMu = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnaForm());
        }
    }
}
