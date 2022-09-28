using Predictor_FORM.Forms;
using Predictor_FORM.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Predictor_FORM
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //List<MapObject> c = new List<MapObject>();
            //Application.Run(new GameWindow(new Map.Map("as",c)));
        }
    }
}
