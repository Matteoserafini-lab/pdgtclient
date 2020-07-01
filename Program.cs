using System;
using Gtk;

namespace PDGTClient
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try{
                Application.Init();
                
                var app = new Application("org.MainWindow.MainWindow", GLib.ApplicationFlags.None);
                app.Register(GLib.Cancellable.Current);
                
                var win = new MainWindow();
                app.AddWindow(win);
                
                win.Show();
                
                Application.Run();

            } catch (Exception) {
                Console.WriteLine("qualcosa è andato storto!");
            }
        }
    }
}