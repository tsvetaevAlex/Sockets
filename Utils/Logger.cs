
using simicon.automation.Tests;

namespace simicon.automation.Tests.Utils;

public class Logger : ILogger
{
    public string _LogFolder = string.Empty;
    private string _logFile = "";
    public readonly object thisLock = new object();
    private readonly string NewLine = Environment.NewLine;
    private Snapshot _snapshots = new Snapshot();
    private string launchDateTime;
    private DataHeap testRunData = new DataHeap();

    #region aliases for log file names by type
    private readonly string _RouteTag = "TraceRoute";
    private readonly string _WarningTag = "Warning";
    private readonly string _FailureTag = "Failure";
    private readonly string _ExceptionTag = "Exceptions";
    private readonly string _InfoTag = "Info";
    private readonly string _Snapshot = "Snapshots";
    private readonly string _Testcase = "TesСase";
    private readonly string _success = "success";
    private readonly string _sendorapp = "sensorapp";
    private readonly string _Networtk = "Network";
    #endregion

    public Logger()
    {
        launchDateTime = testRunData.launchDateTime;

        InitLogger();
    }

    private void LogString(string Prefix, string message, string tag)
    {
        string logStr = Prefix + message;
        Console.WriteLine(logStr + Environment.NewLine);
        Write(logStr, tag);
        Console.WriteLine(Prefix + message);
    }
    public void WhereLogs()
    {
        LogString("WhereLogs: ", _LogFolder, "WhereLogs");
        LogString("WhereSnapshots: ", _snapshots.LocalSnapshotPath, "WhereLogs");
    }
    public void WhereLogs(string message)
    {
        LogString(message, _LogFolder, "WhereLogs");
    }
    public void Info(string message)
    {
        LogString("Info: ", message, _InfoTag);
    }
    public void Route(string message)
    {
        LogString("Route: ", message, _RouteTag);
    }
    public void Exception(string message)
    {
        LogString("EXCEPTION: ", message, _ExceptionTag);
    }
    public void Success(string message)
    {
        LogString("SUCCESS: ", message, _success);
    }

    public void Sensorapp(string message)
    {
        LogString("Sensorapp: ", message, _sendorapp);
    }
    public void Failure(string message)
    {
        LogString("FAILURE: ", message, _FailureTag);
    }
    public void Warning(string message)
    {
        LogString("WARNING: ", message, _WarningTag);
    }   

    public void SnapShot(string message)
    {
        LogString("Snapshot: ", message, _Snapshot);

    }
    public void TestCase(string message)
    {
        LogString("Tesst Case: ", NewLine + message, _Testcase);
    }
    public void Networtk(string message)
    {
        LogString("Connections: ", message, _Networtk);
    }

    public void InitLogger()
    {

        Write("has entered into InitLogger", "TraceRoute");
        _LogFolder = $"{Environment.CurrentDirectory}\\Logs\\{launchDateTime}\\";
        //string _userName = "gitlab-ci";
        //var currentDirectory = System.IO.Directory.GetCurrentDirectory();
        //_LogFolder = Path.Combine(Env.LogsFolder,_launchDate);
        Console.WriteLine($"WhERELogs: Logs Folder: {_LogFolder}");
        //string _LogFolder = $"FILESRV\\public\\QA\\autotest_sw\\{_launchDate}\\{_userName}";
        var dirInfo = new DirectoryInfo(_LogFolder);
        if (!dirInfo.Exists)
        {
            Directory.CreateDirectory(_LogFolder);
            testRunData.LogsFolder = _LogFolder;
        }

        using (FileStream strm = File.Create("T:\\WhereLogs.txt"))
        {
            using (StreamWriter sw = new StreamWriter(strm))
            {
                sw.WriteLine($"{_LogFolder}");
            }
        }
        WhereLogs();
    }


    public string GetLogsFolder()
    {
        return _LogFolder;
    }

    private void Write(string message, string TAG)
    {
        var DateStamp = DateTime.Now.ToString("yyyy-MM-dd;HH:mm:ss");

        string Outputline = $"{DateStamp}: {message}";

        _logFile = $"{_LogFolder}/{TAG}.log";
        bool lockTaken = false;
        try
        {
            if (Monitor.TryEnter(thisLock))
            {
                using StreamWriter sw = File.AppendText(_logFile);
                {
                    //string lineprefix = $"Call From {memberName}, code line {codeLine} at: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    sw.WriteLine($"{Outputline}");
                }
                //File.WriteAllText(_logFile, $"_>{LogPrefix} : {message}");
            }
        }
        finally
        {
            if (lockTaken)
            {
                Monitor.Exit(thisLock);
                lockTaken = false;
            }
        }
    }


}//end of class