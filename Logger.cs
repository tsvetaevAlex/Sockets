using Renci.SshNet;
using System.Diagnostics;

namespace simicon.automation;

    public static class Logger
    {
        private static string _prefix = "";
        private static string _userName = "";
        private static string _LogFilePath = "";
        private static string _launchDate = "";
        private static string _parentCaller;
        private static string _LogFolder = "";
        private static string _logFile = "";
        private static string _TAG = "";
    public static void  InitLogfilename()
        {
            _launchDate = DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
            _prefix = ":) " + DateTime.Now.ToString("yyyy-MM-dd hh:mm : ");
            _userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            //_LogFilePath = $"F:\\AutomationLogs\\{launchDate}\\{_userName}\\";
            _LogFolder = @"T:\AutomationLogs\" + _launchDate +'\\'+ _userName;
            _parentCaller = GetParentCaller();

            Directory.CreateDirectory(_LogFolder);
            Write("Test message from 'InitLogfilename' to verify logger", "initLogger");
        }

        //dirInfo = new DirectoryInfo(_LogFilePath);
        //    if (!dirInfo.Exists)
        //    {
        //        System.IO.Directory.CreateDirectory(_LogFilePath);
        //    }


        private static string GetParentCaller()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            if (stackFrames.Length > 1)
            {
                var callerMethodName = stackFrames.Skip(1).First().GetMethod().Name;
                return callerMethodName;
            }

            return "logger";
        }

        public static void Write(string message, string? TAG)
        {
            if (TAG is null)
            {
                TAG = GetParentCaller();
            }
            _TAG = TAG;
            _logFile = $"{_LogFolder}\\{TAG}.log";

        string launchDate = DateTime.Now.ToString("yyyy-MM-dd hh-mm");
        string prefix = ":) " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //>>Console
        Console.WriteLine(TAG + prefix + message);



        //////////////////////////////////////////////////////////////////
        /// swap stream to filestream
        //////////////////////////////////////////////////////////////////

        //fstream = new FileStream("note3.dat", FileMode.OpenOrCreate);
        //>>_LogFilePath
        //using (FileStream fs = new FileStream( _LogFile, FileMode.OpenOrCreate);
        using FileStream fs = File.Create(_logFile);
        using var sw = new StreamWriter(fs);
        {
            sw.WriteLine($"{TAG} : {prefix} : {message}");
        }
        //TODO: [System.Runtime.CompilerServices.CallerMemberName] string memberName="";
        //TODO: System.Diagnostics.Trace.WriteLine("member name: " + memberName);
    }

    }

