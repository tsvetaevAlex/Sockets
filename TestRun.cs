
using Renci.SshNet;
using simicon.automation.Tests.Utils;

namespace simicon.automation.Tests;

public  class TestRun
{


    /*
     /summary 
     
     */
    protected string Host = string.Empty;
    protected string Login = string.Empty;
    protected string Password = string.Empty;
    //protected string CameraType= string.Empty;
    protected string snapshotsLocalFolder = string.Empty;
    protected int MAXTryCount = 5;
    protected Enums enums = new Enums();
    protected ReportRow reportRow= new ReportRow();
    protected Snapshot snapshots = new Snapshot();

    protected SshClient SshClient { get; set; }
    protected SftpClient SftpClient { get; set; }
    protected ShellStream CameraConsole { get; set; }

    protected Helper helper = new Helper();
    protected SharedTests sharedTests= new SharedTests();
    protected Logger log = new Logger();
    protected Connections connections = new Connections();
    protected SshCommandResponse Response = new SshCommandResponse();
    protected RemoteConsole remoteConsole = new RemoteConsole();
    protected string remoteDirectory = "/tftpboot/boot/";
    protected string authorizationApproval = "Processing /etc/profile... Done";
    protected string NewLine = Environment.NewLine;
    protected string LogsFolder = string.Empty;
    protected string SnapshotsFolder = string.Empty;
    private string launchDateTimeStamp = string.Empty;
    private DataHeap TestRunData = new DataHeap();
    public TestRun() {
        Console.WriteLine("TestRun.DefaultConstructor()");
    }

    public void Initialization()
    {

        /*
         * Summary 
         * 7/13/2023 1:45AM
         * Buildable
         * Blocker: The program '[10384] testhost.exe' has exited with code 3221225477 (0xc0000005) 'Access violation'.
         
         *===> investigation required

         */

        launchDateTimeStamp = DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
        TestRunData.launchDateTime = launchDateTimeStamp;
        Console.WriteLine("TestRun.Initialization()");
        Console.WriteLine($"TestRun.Initialization().launchDateTime: {launchDateTimeStamp}");
        SshClient = connections.GetDeviceSSH();
        SftpClient = connections.GEtDeviceSFTP();
        CameraConsole = connections.AuthorizePicocom();

    }
    public void Finalization()
    {
        Console.WriteLine("TestRun.Finalization()");
    }

    public string GetHost()
    {
        return Host;
    }
    public string GetLogin()
    {
        return Login;
    }
    public string GetPassword()
    {
        return Password;
    }
}
