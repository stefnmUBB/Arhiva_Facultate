using AI.Lab9.Solvers;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AI.Lab9
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());            
        }
    }
}
