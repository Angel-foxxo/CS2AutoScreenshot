using System;
using System.Globalization;
using Gtk;

namespace CS2AutoScreenshot
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            var app = new Application("org.CS2AutoScreenshot.CS2AutoScreenshot", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();


        }
    }
}
