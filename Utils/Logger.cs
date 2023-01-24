using Renci.SshNet;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace simicon.automation.Utils;

public static class Logger
{
    private static string _userName = "";
    private static string _launchDate = "";
    private static string _parentCaller;
    public static string _LogFolder = "";
    private static string _logFile = "";
    private static string prefix = "";
    private static readonly object thisLock = new object();
    public static void InitLogger([CallerMemberName] string memberName = "", [CallerLineNumber] int codeLine = 0)
    {

        Write("has entered into InitLogger", "TraceRoute");

#pragma warning disable CA1416
        _launchDate = DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
        prefix = ":) " + DateTime.Now.ToString("yyyy-MM-dd hh:mm : ");
        _userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
        prefix = $"Call From {memberName}, code line {codeLine} at: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //_LogFilePath = $"F:\\AutomationLogs\\{launchDate}\\{_userName}\\";
        _LogFolder = @"T:\AutomationLogs\" + _launchDate + '\\' + _userName;
        _parentCaller = GetParentCaller();
        _parentCaller = GetParentCaller();
#       pragma warning restore  CA1416
        var dirInfo = new DirectoryInfo(_LogFolder);
        if (!dirInfo.Exists)
        {
            Directory.CreateDirectory(_LogFolder);
        }
    }

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
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int codeLine = 0)
    {
        _logFile = $"{_LogFolder}\\{TAG}.log";
        bool lockTaken = false;
        try
        {
            if (TryEnter(thisLock))
            {
                using StreamWriter sw = File.AppendText(_logFile);
                {
                    string lineprefix = $"Call From {memberName}, code line {codeLine} at: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    sw.WriteLine($"_>{lineprefix} : {message}");
                }
                //File.WriteAllText(_logFile, $"_>{prefix} : {message}");
            }
        }
        finally
        {
            if (lockTaken)
            {
                Exit(thisLock);
                lockTaken = false;
            }
        }
    }

    //public static void Write(string message, string? TAG,
    //        [CallerMemberName] string memberName = "",
    //        //[CallerFilePath] string sourceFilePath = "",
    //        [CallerLineNumber] int codeLine = 0)
    //{
    //    if (TAG == null)
    //    {
    //        TAG = memberName;
    //    }
    //    _logFile = $"{_LogFolder}\\{TAG}.log";

    //    _launchDate = DateTime.Now.ToString("yyyy-MM-dd hh-mm");
    //    string prefix = $"Call From {memberName}, code line {codeLine} at: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
    //    Console.WriteLine($"{TAG} {prefix} {message};");



    //////////////////////////////////////////////////////////////////
    /// swap stream to filestream
    //////////////////////////////////////////////////////////////////


}//end of class


