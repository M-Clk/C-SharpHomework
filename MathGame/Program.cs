using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathGame
{
    static class Program
    {
        public static int seviye = 1;
        public static int islemTuruIndex = 0;
        public static string[] islemler = { "+", "-", "x", "/" };
        public static string[] sorular;
        public static int skor = 0;
        public static string kayitliAyarDosyaAdi = "saves.clk";
        public static string skorDosyaAdi = "scores.clk";
        public static bool buyuktenKucugeMi = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int temp_seviye = 0;
            if (args.Length == 2)
            {
                if ("open".Equals(args[0].ToLower()) && int.TryParse(args[1], out temp_seviye))
                    seviye = (temp_seviye > 0 && temp_seviye <= 5) ? temp_seviye : 1;
                else if ("open".Equals(args[0].ToLower()) && "all".Equals(args[1].ToLower()))
                    seviye = 5;
            }
            else
            {
                string kayitliBilgiler = File.ReadAllText(kayitliAyarDosyaAdi);
                if (kayitliBilgiler != null)
                {
                    int.TryParse(kayitliBilgiler.Split('|')[0], out seviye);
                    if (seviye < 0 || seviye > 5) //Degistirilmis olmasina karsin bir onlem
                        seviye = 0;
                    int.TryParse(kayitliBilgiler.Split('|')[1], out skor);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnaForm());

        }
    }
}
