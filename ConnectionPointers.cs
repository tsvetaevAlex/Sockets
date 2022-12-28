
using System.IO;

namespace simicon.automation;

public static class ConnectionPointers
    {
    public static string Host;
    public static string Login;
    public static string Password;
    public static SshClient     DeviceSocket;// directly fot device messages
    public static SftpClient    SftpSocket;
    public static ShellStream   DeviceStream;// secured ftp to device. to be able download files(snaphots) from device 
    public static SshClient     DevicebackDoor;// directly fot device messages
    public static ShellStream   BackDoorStream;
    private static ShellStream  cameraStream;
    public static string TAG = "ConnectionPointers";

    /// <summary>
    /// initializa all reuqired connections: SSH  and SFTP to device, console to Camera
    /// </summary>
    /// <param name="host"></param>
    /// <param name="loginname"></param>
    /// <param name="pswd"></param>

    public static ShellStream GetCameraStream()
    {
        Logger.Write($"we are in ConnectionPointers.GetCameraStream outbound stream is :'{cameraStream}'.",TAG);

        if (cameraStream != null)
            Logger.Write($"We are in ConnectionPointers.GetCameraStream, if says tha cameraStrem not null, cameraStream ='{cameraStream}'", TAG);

        else
            Assert.Fail("No connection to Camera");

        return cameraStream;
    }

    private static void SetCameraStream (ShellStream stream)
    {
        Logger.Write($"we are in SetCameraStream inbound stream is :'{stream}'.",TAG);
        if (stream is null)
        {
            Logger.Write($"EPIC FAIL ET FILED we are in SetCameraStream inbound stream is null WTF", TAG);

        }
        cameraStream = stream;
    }

    public static void InitConnectionPointers(string host, string loginname, string pswd)
    {


        Logger.Write("has entered into InitConnectionPointers", "TraceRoute");


        Logger.Write("has entered into PrepareEnvironment -> ConnectionPointers.InitConnectionPointers", TAG);
        Host = host;
            Globals.Host = host;
            Login = loginname;
            Globals.Login = loginname;
            Password = pswd;
            Globals.Password = Password;


        try
        {
                IniSSHConnection(host, loginname, pswd);
                string sshResultMessage = $"result of attempt to create SSH connection is: DeviceSocket == {DeviceSocket}";
                Logger.Write(sshResultMessage, TAG);
        }
        catch (Exception e)
        {
            string ExMessage = $"in rime of creation  SSH client connection Exception has been thrown:{e.Message}";
            Console.WriteLine(ExMessage);
            Logger.Write(ExMessage,TAG);
        }

        if (!DeviceSocket.IsConnected)
        {
            Logger.Write($"SSH connection to Device({host}) is not established", TAG);
        }
        Logger.Write($"SSh connection to device is {DeviceSocket}.", TAG);
////////////////////////////////////////////////<---------[ InitSFTPnnection-Connection ]--------->");
        Console.WriteLine("\n<---------[ InitSFTPnnection-Connection ]--------->");
        Logger.Write("\n<---------[ InitSFTPConnection ]--------->",TAG);
        InitSftpConnection(host, loginname, pswd);
        string sshMessage = $"result of attempt to create SFTP connection is: DeviceSocket == {SftpSocket}";
        Logger.Write($"sftp Connection: {SftpSocket}, created", TAG);

////////////////////////////////////////////////<---------[ Init Picocom->Device.Camera ]--------->");
        Console.WriteLine("\n<---------[ Init Picocom->Device.Camera ]--------->");
        Logger.Write("\n<---------[ Init Picocom->Device.Camera Connection ]--------->",TAG);
        AuthorizePicocom();
        Logger.Write($"Picocom connection to device is: [{cameraStream}]", TAG);

    }
    /// <summary>
    /// create connection ro camera console using picocom command
    /// </summary>
    private static void AuthorizePicocom()
    {
        Logger.Write("has entered into AuthorizePicocom", "TraceRoute");

        ShellStream cameraStream = DeviceSocket.CreateShellStream("picocom Terminal", 80, 180, 800, 600, 1024);

        string picocomRequest = "picocom -b 115200 /dev/tts/camera --imap lfcrlf";
        string expectedContent = "Terminal ready";

        StringBuilder sb = new StringBuilder();
        string buffer = "";
        Logger.Write($"send Camera connection string to create camera data channel: '{picocomRequest}'.", TAG);
        //send auth request
        cameraStream.Flush();
        cameraStream.WriteLine(picocomRequest);
        Thread.Sleep(1000); // Prevents the shell from losing the output if it becomes too slow


        //receive response. expected content is: "Terminal ready";
        ShellStream stream = cameraStream;
        SetCameraStream(cameraStream);
        if (stream.DataAvailable)
        {
            while (stream.DataAvailable)
            {
                Logger.Write($"AuthorizePicocom,stream.DataAvailable:{stream.DataAvailable} ", TAG);
                buffer = stream.ReadLine();
                Console.WriteLine($"Response String received: {buffer}");
                Logger.Write($"Response String received: '{buffer}'", TAG);
                sb.Append(buffer);
                if (buffer.Contains(expectedContent))
                {
                    if (buffer.Contains("FATAL: cannot lock "))
                    {
                        string fatalMessage = "continue is not available\\r\\nConnectionFAIL.FATAL Error Message received, that Resource temporarily unavailable.";
                        Logger.Write(fatalMessage, "_FATAL");
                        Assert.Fail(fatalMessage);
                        Environment.Exit(503);
                    }
                    Logger.Write("!!!Camera Console Connected: 'Terminal Ready' message received.",
                        TAG);
                    cameraStream = stream;
                    Logger.Write($"!!!\\n1st variable 'stream':{stream}"
                                 + $"2nd variable 'cameraStream': {cameraStream}",
                        TAG);
                }
            }// end of while (stream.DataAvailable)
        }// end of if (stream.DataAvailable)
    }//end of AuthorizePicocom

    /// <summary>
    /// create SSH connection to Device
    /// </summary>
    /// dvice credentials as  poarams
    /// <param name="ip"></param>
    /// <param name="loginname"></param>
    /// <param name="pswd"></param>
    private static void IniSSHConnection(string ip, string loginname, string pswd)
        {

        Logger.Write("has entered into IniSSHConnection", "TraceRoute");

        #region create SSH connection with device
        Logger.Write("Attempt to create SSH Bridge to Device (Device Console)", TAG);
            try
            {
            SshClient _client = new SshClient(ip, loginname, pswd);
            Logger.Write($"SSH Device Connection created. Pointer  is: {_client}.", TAG);
            _client.Connect();
                DeviceSocket = _client;
                DevicebackDoor = _client;
            }
            catch (SocketException e)
            {
                string ExSSHmessage = $"Exception in time of creation SSHClient:Exception: {e.Message}";

                Console.WriteLine(ExSSHmessage);
                Logger.Write(ExSSHmessage, TAG);
                Assert.Fail(ExSSHmessage);
            }

            ShellStream stream = DeviceSocket.CreateShellStream("", 0, 0, 0, 0, 0);
            DeviceStream = stream;
            BackDoorStream= stream;
        
            #region wait for connection acknowledgment

            string authorizationApproval = "Processing /etc/profile... Done";
            if (stream.DataAvailable)
            {
                while (stream.CanRead)
                {
                    string acknowledgment = stream.ReadLine();

                    if (acknowledgment.Contains(authorizationApproval))
                    {
                        Logger.Write("initConnectionPointers, response received: 'Processing /etc/profile... Done'",
                            TAG);
                        Console.Write($"resp: '{acknowledgment}'");
                        Logger.Write($"!!!SSH CONNECTION APPROVED!!!", TAG);
                    }
                }
            }

            #endregion
        #endregion
        }// End of initConnection

/// <summary>
///  crete SFTP connection to Device
/// </summary>
/// dvice credentials as  poarams
/// <param name="ip"></param>
/// <param name="lohinname"></param>
/// <param name="pswd"></param>
    private static void InitSftpConnection(string ip, string lohinname, string pswd)
    {
        #region create SFTP connection with Device

        Logger.Write("has entered into InitSFTPConnection", "TraceRoute");

        Logger.Write("Device InitSftpConnection", TAG);
        try
        {
            SftpClient sftp = new SftpClient(ip, lohinname, pswd);
            sftp.Connect();
            SftpSocket = sftp;
        }
        catch (Exception e)
        {
            string sftpExMessage =$"In time of creation SFTP connection Exception has been thrown:{e.Message}";
            Console.WriteLine(sftpExMessage);
            Logger.Write(sftpExMessage, TAG);
            Assert.Fail(sftpExMessage);
        }

        #endregion
    }
}// end o class ConnectionPointers