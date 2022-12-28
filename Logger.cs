using Renci.SshNet;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace simicon.automation;

public static class Logger
{
    private static string _prefix = "";
    private static string _userName = "";
    private static string _launchDate = "";
    private static string _parentCaller;
    private static string _LogFolder = "";
    private static string _logFile = "";
    private static readonly object thisLock = new object();
    public static void InitLogger()
    {

        Logger.Write("has entered into InitLogger", "TraceRoute");

        _launchDate = DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
        _prefix = ":) " + DateTime.Now.ToString("yyyy-MM-dd hh:mm : ");
        _userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
        //_LogFilePath = $"F:\\AutomationLogs\\{launchDate}\\{_userName}\\";
        _LogFolder = @"T:\AutomationLogs\" + _launchDate + '\\' + _userName;
        _parentCaller = GetParentCaller();

        Directory.CreateDirectory(_LogFolder);
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
            _parentCaller = stackFrames.Skip(1).First().GetMethod().Name;
            return _parentCaller;
        }
        return "logger";
    }

    public static void Write(string message, string? TAG,
            [CallerMemberName] string memberName = "",
            //[CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int codeLine = 0)
    {
        if (TAG == null)
        {
            TAG = memberName;
        }
        _logFile = $"{_LogFolder}\\{TAG}.log";

        _launchDate = DateTime.Now.ToString("yyyy-MM-dd hh-mm");
        string prefix = $"Call From {memberName}, code line {codeLine} at: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        Console.WriteLine($"{TAG} {prefix} {message};");



        //////////////////////////////////////////////////////////////////
        /// swap stream to filestream
        //////////////////////////////////////////////////////////////////

        //fstream = new FileStream("note3.dat", FileMode.OpenOrCreate);
        //>>_LogFilePath
        //using (FileStream fs = new FileStream( _LogFile, FileMode.OpenOrCreate);
        //if(Monitor.TryEnter(thisLock, timeout)

        //lock (thisLock)
        //{
            try
            {
                Monitor.Enter(thisLock);
                FileStream logFile = new FileStream(_logFile, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(logFile);
                sw.WriteLine($"_>{prefix} : {message}");
            }
            finally
            {
                Monitor.Exit(thisLock);
            }
        
    }//end of write

}//end of class

