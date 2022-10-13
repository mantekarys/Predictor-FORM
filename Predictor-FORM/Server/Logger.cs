using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Server
{
    public class Logger
    {
        private static Logger instance = new Logger();
        private Logger()
        {
        }
        public static Logger getInstance()
        {
            return instance;
        }

        public void WriteMessage(string message)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path2 = Path.Combine(path, "log.txt");
            using (StreamWriter sw = File.AppendText(path2))
            {
                sw.WriteLine(string.Format("{0}: {1}", DateTime.Now, message));
            }
        }
    }
}