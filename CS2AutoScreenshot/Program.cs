using System;
using System.Windows.Forms;

namespace CS2AutoScreenshot
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Application.Run(new MainForm());
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