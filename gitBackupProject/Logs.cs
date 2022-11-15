using System;
using System.IO;

namespace gitBackupProject
{
    public class Log
    {
        public static void WriteLog(string sEvent)
        {

            StreamWriter log;
            if (!File.Exists("logfile.txt"))
            {
                log = new StreamWriter("logfile.txt");
            }
            else
            {
                log = File.AppendText("logfile.txt");
            }

            string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + sEvent + "\n";
            log.WriteLine(line);
            Console.WriteLine(line);

            log.Close();
        }

    }
}
