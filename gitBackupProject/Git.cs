using System;
using System.Diagnostics;

namespace gitBackupProject
{
    public class Git
    {
        public void git_init(string path)
        {
            Log.WriteLog("git init");

            var statusCommand = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = @"/c cd " + path + " & git init",
            };

            var output = Process.Start(statusCommand).StandardOutput.ReadToEnd();
            Log.WriteLog(output);

        }

        public string git_branch_name(string path)
        {
            Log.WriteLog("git rev-parse --abbrev-ref HEAD");

            var statusCommand = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = @"/c cd " + path + " & git rev-parse --abbrev-ref HEAD",
            };


            var output = Process.Start(statusCommand).StandardOutput.ReadToEnd();
            Log.WriteLog(output);

            return output;
        }

        public string git_remote(string path)
        {
            var statusCommand = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = @"/c cd " + path + " & git remote -v",
            };

            return Process.Start(statusCommand).StandardOutput.ReadToEnd();
        }

        public void git_remote_add(string path, string remote_url)
        {
            Log.WriteLog("git remote add origin " + remote_url);

            var statusCommand = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = @"/c cd " + path + " & git remote add origin " + remote_url,
            };

            string output = Process.Start(statusCommand).StandardOutput.ReadToEnd();
            Log.WriteLog(output);

        }

        public string git_status(string path)
        {
            Log.WriteLog("git status");

            var statusCommand = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = @"/c cd " + path + " & git status",
            };

            return Process.Start(statusCommand).StandardOutput.ReadToEnd();
        }

        public void git_add(string path)
        {
            Log.WriteLog("git add .");

            var statusCommand = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = @"/c cd " + path + " & git add .",
            };

            Process.Start(statusCommand).StandardOutput.ReadToEnd();
        }

        public string git_commit(string path)
        {
            var message = "\"" + DateTime.Now.ToString() + "\"";
            Log.WriteLog("git commit -m " + message);

            var statusCommand = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = @"/c cd " + path + " & git commit -m " + message,
            };

            return Process.Start(statusCommand).StandardOutput.ReadToEnd();
        }

        public string git_push(string path, string branch)
        {

            git_add(path);
            git_commit(path);

            Log.WriteLog("git push -f origin " + branch);

            var statusCommand = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = @"/c cd " + path + " & git push -f origin " + branch,
            };


            string output = Process.Start(statusCommand).StandardOutput.ReadToEnd();
            Log.WriteLog(output);
            Log.WriteLog("\nCycle Completed\n");

            return output;
        }

    }
}
