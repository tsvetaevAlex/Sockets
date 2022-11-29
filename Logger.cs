using System.Diagnostics;

namespace simicon.automation;

    public static class Logger
    {
        private static string prefix = "";
        private static string logPrefix = "";
        private static string userName = "";
        private static string LogFile = "";
        private static DirectoryInfo dirInfo;

        private static void  INItLogfilename()
        {
            string launchDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            logPrefix = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ");
            prefix = ":) " + DateTime.Now.ToString("yyyy-MM-dd hh:mm: ");
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            LogFile = $"C:\\AutomationLogs\\{launchDate}\\{userName}\\";
            dirInfo = new DirectoryInfo(LogFile);
            if (!dirInfo.Exists)
            {
                System.IO.Directory.CreateDirectory(LogFile);
                return LogFile;
            }
        }


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
        static readonly string parentCaller = GetParentCaller();

        public static void Write(string message, string? TAG)
        {

            if (TAG is null)
            {
                TAG = GetParentCaller();
            }

        string launchDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
        string prefix = ":) " + DateTime.Now.ToString("yyyy-MM-dd hh:mm: ");
        //>>Console
        Console.WriteLine(TAG + prefix + message);

        string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
        string LogFilePath = $"s:\\AutomationLogs\\{launchDate}\\{userName}";
        if (!Directory.Exists(LogFilePath))
            Directory.CreateDirectory(LogFilePath);
        
        //>>LogFile
        using (StreamWriter sw = File.AppendText(LogFile))
        {
            sw.WriteLine($"{TAG} : {prefix} : {message}");
        }
        //TODO: [System.Runtime.CompilerServices.CallerMemberName] string memberName="";
        //TODO: System.Diagnostics.Trace.WriteLine("member name: " + memberName);
    }

    }

