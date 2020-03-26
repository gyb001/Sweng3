using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class LogConsole
    {

        private static LogConsole instance = null;
        private static readonly object padlock = new object();


        public static LogConsole Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LogConsole();
                    }
                    return instance;
                }
            }
        }

        StreamWriter sw;
        StreamReader sr;

        public LogConsole()
        {
    

            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            Process p = Process.Start(psi);

            sw = p.StandardInput;
            sr = p.StandardOutput;

        }
        public void addMsg (string str)
        {
         sw.WriteLine(str);
        }

    }
}
