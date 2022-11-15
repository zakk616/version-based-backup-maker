using System;
using System.Configuration;
using System.Diagnostics;
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

                if (DateTime.Now.Minute % 10 == 0)
                {
                    try
                    {
                        var output = g.git_status(path);

                        Log.WriteLog(output);

                        if (output.Contains("Untracked files:"))
                        {
                            g.git_push(path, branch);
                        }
                        else if (output.Contains("Changes to be committed:"))
                        {
                            g.git_push(path, branch);
                        }
                        else if (output.Contains("Changes not staged for commit:"))
                        {
                            g.git_push(path, branch);
                        }

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

        //public static void git_init(string path)
        //{
        //    Log.WriteLog("git init");
        //    Console.WriteLine("git init");
        //    var statusCommand = new ProcessStartInfo
        //    {
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //        FileName = "CMD.exe",
        //        Arguments = @"/c cd " + path + " & git init",
        //    };

        //    var output = Process.Start(statusCommand).StandardOutput.ReadToEnd();
        //    Log.WriteLog(output);
        //    Console.WriteLine(output);
        //}

        //public static string git_branch_name()
        //{
        //    Log.WriteLog("git rev-parse --abbrev-ref HEAD");
        //    Console.WriteLine("git rev-parse --abbrev-ref HEAD");
        //    var statusCommand = new ProcessStartInfo
        //    {
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //        FileName = "CMD.exe",
        //        Arguments = @"/c cd " + path + " & git rev-parse --abbrev-ref HEAD",
        //    };


        //    var output = Process.Start(statusCommand).StandardOutput.ReadToEnd();
        //    Log.WriteLog(output);
        //    Console.WriteLine(output);
        //    return output;
        //}

        //public static string git_remote(string path)
        //{
        //    var statusCommand = new ProcessStartInfo
        //    {
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //        FileName = "CMD.exe",
        //        Arguments = @"/c cd " + path + " & git remote -v",
        //    };

        //    return Process.Start(statusCommand).StandardOutput.ReadToEnd();
        //}

        //public static void git_remote_add(string path, string remote_url)
        //{
        //    Log.WriteLog("git remote add origin " + remote_url);
        //    Console.WriteLine("git remote add origin " + remote_url);
        //    var statusCommand = new ProcessStartInfo
        //    {
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //        FileName = "CMD.exe",
        //        Arguments = @"/c cd " + path + " & git remote add origin " + remote_url,
        //    };

        //    string output = Process.Start(statusCommand).StandardOutput.ReadToEnd();
        //    Log.WriteLog(output);
        //    Console.WriteLine(output);
        //}

        //public static string git_status(string path)
        //{
        //    Log.WriteLog("git status");
        //    Console.WriteLine("git status");

        //    var statusCommand = new ProcessStartInfo
        //    {
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //        FileName = "CMD.exe",
        //        Arguments = @"/c cd " + path + " & git status",
        //    };

        //    return Process.Start(statusCommand).StandardOutput.ReadToEnd();
        //}

        //public static void git_add(string path)
        //{
        //    Log.WriteLog("git add .");
        //    Console.WriteLine("git add .");
        //    var statusCommand = new ProcessStartInfo
        //    {
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //        FileName = "CMD.exe",
        //        Arguments = @"/c cd " + path + " & git add .",
        //    };

        //    Process.Start(statusCommand).StandardOutput.ReadToEnd();
        //}

        //public static string git_commit(string path)
        //{
        //    var message = "\"" + DateTime.Now.ToString() + "\"";
        //    Log.WriteLog("git commit -m " + message);
        //    Console.WriteLine("git commit -m " + message);
        //    var statusCommand = new ProcessStartInfo
        //    {
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //        FileName = "CMD.exe",
        //        Arguments = @"/c cd " + path + " & git commit -m " + message,
        //    };

        //    return Process.Start(statusCommand).StandardOutput.ReadToEnd();
        //}

        //public static string git_push(string path) {

        //    git_add(path);
        //    git_commit(path);

        //    Log.WriteLog("git push origin");
        //    Console.WriteLine("git push origin");
        //    var statusCommand = new ProcessStartInfo
        //    {
        //        UseShellExecute = false,
        //        RedirectStandardOutput = true,
        //        FileName = "CMD.exe",
        //        Arguments = @"/c cd " + path + " & git push origin "+ branch,
        //    };

        //    return Process.Start(statusCommand).StandardOutput.ReadToEnd();
        //}

    }
}
