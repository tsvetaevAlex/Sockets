
using Renci.SshNet.Common;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace simicon.automation;

public static class ConnectionPointers
{
    public static string Host = "192.168.10.102";
    public static string Login = "root";
    public static string Password = "test";
    private static SshClient _DeviceSSH;// directly fot device messages
    private static SftpClient _DeviceSFTP;
    private static ShellStream _DeviceStream;// secured ftp to device. to be able download files(snaphots) from device 
    private static ShellStream _cameraStream;
    public static string TAG = "ConnectionPointers";
    public static string fTag = "failure";





    /// <summary>
    /// initializa all reuqired connections: SSH  and SFTP to device, console to Camera
    /// </summary>
    /// <param name="host"></param>
    /// <param name="loginname"></param>
    /// <param name="pswd"></param>

    public static SshClient GetDeviceSSH()
    {
        return _DeviceSSH;
    }
    public static SftpClient GEtDeviceSFTP()
    {
        return _DeviceSFTP;
    }
    public static ShellStream GetDeviceShell()
    {   
        return _DeviceStream;
    }       
    public static ShellStream? GetCameraStream()
    {
        try
        {
            if (_cameraStream.CanRead)
            {
                Logger.Write("GetCameraStream.if (_cameraStream.CanRead) TRUE (inside if)", TAG);
            }
            else
            {
                Logger.Write("GetCameraStream.if (_cameraStream.CanRead) FALSE (inside if)", TAG);
                Logger.Write("attempt to create a nw pcamera connection(picocom)", TAG);
            Logger.Write($"we are in ConnectionPointers.GetCameraStream outbound stream is :'{_cameraStream}'.", TAG);
            Logger.Write("attempt to create a nw pcamera connection(picocom)", TAG);
                ShellStream pic1 = AuthorizePicocom();
                if (pic1 != null)
                {
                    Logger.Write($"New not null connection created: {pic1}", TAG);
                    if (pic1.CanRead)
                    {
                        Logger.Write("New nol null and .CanRead", TAG);
                        return pic1;
                    }
                }
            }
        }
        catch (Exception e)
        {
            _cameraStream.Dispose();
        }
        if ((_cameraStream != null) && (_cameraStream.CanRead))
        {
            Logger.Write($"We are in ConnectionPointers.GetCameraStream, if says tha cameraStrem is not null anda cameraStream readable; camera stream  ='[{_cameraStream}]'", TAG);
            return _cameraStream;
        }
        else
        {
            string errMessage = "We are in ConnectionPointers.GetCameraStream, No connectionObject to Camera it null and/or not CanRead.";
            Logger.Write(errMessage, TAG);
            Logger.Write("Attempt to picocom agagin...", TAG);
            ShellStream? pic2 = AuthorizePicocom();
            Assert.Fail("No connectionObject to Camera");
            //_cameraStream.Dispose;
            if ((pic2 != null) && (pic2.CanRead))
            {
                return pic2;
            }

        }

        return _cameraStream;
    }

    private static void SetCameraStream (ShellStream stream)
    {
        Logger.Write($"we are in SetCameraStream inbound stream is :'{stream}'.",TAG);
        if (stream is null)
        {
           
        }
        _cameraStream = stream;
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
                string sshResultMessage = $"result of attempt to create SSH connectionObject is: _DeviceSSH == {_DeviceSSH}";
                Logger.Write(sshResultMessage, TAG);
        }
        catch (Exception e)
        {
            string ExMessage = $"in rime of creation  SSH client connectionObject Exception has been thrown:{e.Message}";
            Console.WriteLine(ExMessage);
            Logger.Write(ExMessage,TAG);
        }

        if (!_DeviceSSH.IsConnected)
        {
            Logger.Write($"SSH connectionObject to Device({host}) is not established", TAG);
        }
        Logger.Write($"SSh connectionObject to device is {_DeviceSSH}.", TAG);
////////////////////////////////////////////////<---------[ InitSFTPnnection-Connection ]--------->");
        Console.WriteLine("\n<---------[ InitSFTPnnection-Connection ]--------->");
        Logger.Write("\n<---------[ InitSFTPConnection ]--------->",TAG);
        InitSftpConnection(host, loginname, pswd);
        string sshMessage = $"result of attempt to create SFTP connectionObject is: _DeviceSSH == {_DeviceSFTP}";
        Logger.Write($"actualSftp Connection: {_DeviceSFTP}, created", TAG);

////////////////////////////////////////////////<---------[ Init Picocom->Device.Camera ]--------->");
        Console.WriteLine("\n<---------[ Init Picocom->Device.Camera ]--------->");
        Logger.Write("\n<---------[ Init Picocom->Device.Camera Connection ]--------->",TAG);
        AuthorizePicocom();
        Logger.Write($"Picocom connectionObject to device is: [{_cameraStream}]", TAG);

    }
    /// <summary>
    /// create connectionObject to camera console using picocom command
    /// </summary>
    private static ShellStream? AuthorizePicocom()
    {
        Logger.Write("has entered into AuthorizePicocom", "TraceRoute");

        ShellStream cameraStream = _DeviceSSH.CreateShellStream("picocom Terminal", 80, 180, 800, 600, 1024);

        string picocomRequest = "picocom -b 115200 /dev/tts/camera --imap lfcrlf";
        string expectedContent = "Terminal ready";

        StringBuilder sb = new StringBuilder();
        string output = "";
        string buffer = "";
        Logger.Write($"send Camera connectionObject string to create camera data channel: '{picocomRequest}'.", TAG);
        //send auth request
        //cameraStream.Flush();
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
                    Logger.Write("!!!Camera Console Connected: 'Terminal Ready' message received.",
                        TAG);
                    cameraStream = stream;
                    Globals.Picocom = stream;
                    Logger.Write($"!!!\\n1st variable 'stream':{stream}"
                                 + $"2nd variable 'cameraStream= _DeviceSSH.CreateShellStream()': {cameraStream}",
                        TAG);
                    Globals.isEnvironmentPrepared= true;
                    return stream;
                }
                if (buffer.Contains("'FATAL"))
                {
                    string ERRmsg = "TestRun Failure. Ccnnot connect to camera service. Stream port busy.";
                    Logger.Write(ERRmsg, fTag);
                    Assert.Fail(ERRmsg);
                }
            }// end of while (stream.DataAvailable)
        }// end of if (stream.DataAvailable)
        return stream;
    }//end of AuthorizePicocom

    /// <summary>
    /// create SSH connectionObject to Device
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
                _DeviceSSH = _client;
            }
            catch (SocketException e)
            {
                string ExSSHmessage = $"Exception in time of creation SSHClient:Exception: {e.Message}";

                Console.WriteLine(ExSSHmessage);
                Logger.Write(ExSSHmessage, TAG);
                Assert.Fail(ExSSHmessage);
            }

            ShellStream stream = _DeviceSSH.CreateShellStream("", 0, 0, 0, 0, 0);
            _DeviceStream = stream;
        
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
///  crete SFTP connectionObject to Device
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
            _DeviceSFTP = new SftpClient(ip, lohinname, pswd);
            _DeviceSFTP.Connect();
            Globals.DeviceSFTP = _DeviceSFTP;
        }
        catch (Exception e)
        {
            string sftpExMessage =$"In time of creation SFTP connectionObject Exception has been thrown:{e.ToString}";
            Console.WriteLine(sftpExMessage);
            Logger.Write(sftpExMessage, TAG);
            Assert.Fail(sftpExMessage);
        }

        #endregion
    }
    public static void Dispose()
    {
        Logger.Write("we are in ConnectionPointers.Dispose()", "TraceRoute");

        _DeviceSSH.Dispose();
        _DeviceSFTP.Dispose();
        _DeviceStream.Dispose();
        _DeviceStream.Dispose();
    }
}// end o class ConnectionPointers