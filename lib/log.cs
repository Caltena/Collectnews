using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;

namespace sino.CollectNews
{
    /// <summary>
    /// 
    /// </summary>
    public static class cLog
    {
        private static bool SHOWONCONSOLE = false;
        private static bool DEBUG = false;
        private static string LOGFILE = "log.txt";

        public static bool Debug
        {
            get { return DEBUG; }
            set { DEBUG = value;  }
        }
        public static bool ShowOnConsole
        {
            get { return SHOWONCONSOLE; }
            set { SHOWONCONSOLE = value;  }
        }
        public static string Logfile
        {
            get { return LOGFILE; }
            set { LOGFILE = value;  }
        }
        private static string GetDate()
        {
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("de-DE");
            return localDate.ToString(culture);
        }

        public static void error(string message, string modul)
        {
            write("ERROR", message, modul);
        }

        public static void info(string message, string modul)
        {
            write("INFO", message, modul);
        }
        
        public static void debug(string message, string modul)
        {
            if(DEBUG) write("DEBUG", message, modul);
        }

        private static void write(string sType, string message, string modul = "")
        {
            if ( !SHOWONCONSOLE )
            {
                using (StreamWriter sw = File.AppendText(LOGFILE))
                {
                    sw.WriteLine("[{0}] \t [{1}] \t [Modul:'{2}'] \t Message: {3}", GetDate(), sType, modul, message);
                }
            }
            else 
                Console.WriteLine( "[{0}] \t [{1}] \t [Modul:'{2}'] \t Message: {3}", GetDate(), sType, modul, message);
            
        }
    }
}
