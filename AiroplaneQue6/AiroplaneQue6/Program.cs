using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AiroplaneQue6
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
            //Application.Run(new Form1());
            Form1 app = new Form1();
            app.Width = 800;
            app.Height = 500;
            app.InitialGraphics();
            app.Show();
            while(app.Created)
            {
                app.Render();
                Application.DoEvents();
            }
            app.DisposeGraphics();
        }
    }
}
