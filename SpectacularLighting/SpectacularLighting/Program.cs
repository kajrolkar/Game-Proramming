using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SpectacularLighting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 app = new Form1();
            app.Width = 800;
            app.Height = 500;
            app.InitializeGraphics();
            app.Show();
            while (app .Created )
            {
                app.Render();
                Application.DoEvents();
            }

            app.DisposeGraphics();
        }

    }
}
