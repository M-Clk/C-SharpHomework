using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindNumber
{
    static class Program
    {
       public static int basamakSayisi = 5;
       public static int cevapHakki = 10;
       public static bool tekrarliMi = true;
       public static bool buyuktenKucugeMi = true;
        public static string skorDosyaAdi = "scores.clk";

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
