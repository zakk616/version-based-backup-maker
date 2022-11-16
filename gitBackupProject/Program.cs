using System;
using System.Configuration;
using System.Threading;

namespace gitBackupProject
{
    class Program
    {
        static bool isCompleted = true;
        public static string getAPIBaseUrl(string configKey) => ConfigurationManager.AppSettings[configKey];
        public static string path = getAPIBaseUrl("folderPath");
        public static string remote_url = getAPIBaseUrl("remotePath");
        public static string branch = getAPIBaseUrl("branchName");

        static void Main(string[] args)
        {
            Log.WriteLog("Program Started!");

            Git g = new Git();

            g.git_init(path);
            branch = g.git_branch_name(path);
            g.git_remote_add(path, remote_url);

            Timer _timer = new Timer(TimerCallback, g, 0, 60000);

            Console.ReadLine();
        }

        public static void TimerCallback(Object o)
        {
            Git g = o as Git;

            if (isCompleted)
            {
                isCompleted = false;

                if (DateTime.Now.Minute % 5 == 0)
                {
                    try
                    {
                        var output = g.git_status(path);

                        Log.WriteLog(output);
                        

                        //if (output.Contains("Untracked files:"))
                        //{
                        //    g.git_push(path, branch);
                        //}
                        //else if (output.Contains("Changes to be committed:"))
                        //{
                        //    g.git_push(path, branch);
                        //}
                        //else if (output.Contains("Changes not staged for commit:"))
                        //{
                        //    g.git_push(path, branch);
                        //}

                        g.git_push(path, branch);
                        Log.WriteLog("In TimerCallback");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Log.WriteLog(e.Message);
                    }

                }
                else
                {
                    Log.WriteLog("In TimerCallback");
                }

                isCompleted = true;
            }

        }

    }
}
