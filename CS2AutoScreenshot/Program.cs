using DarkModeForms;
using System;
using System.Windows.Forms;

namespace CS2AutoScreenshot
{
    class Program
    {
        public static MainForm MainForm;
        public static DarkModeCS DarkModeCS = new();

        [STAThread]
        public static void Main(string[] args)
        {

            try
            {
                MainForm = new MainForm();

                DarkModeCS.ThemeColors = DarkModeCS.GetAppTheme();
                DarkModeCS.Style(MainForm);

                Application.Run(MainForm);
            }
            catch (Exception ex)
            {
                AllocConsole();
                Console.WriteLine($"Fatal error: {ex}");
                Console.ReadKey();
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}