using Renci.SshNet;
using System.Text;
using simicon.automation.Tests;

namespace simicon.automation;

public static class Camera
{

    private static readonly string _host = ConnectionPointers.Host;
    private static readonly string _loginname = ConnectionPointers.Login;
    private static readonly string _password = ConnectionPointers.Password;
    private static readonly string TAG = "Camera";
    public static void Connect()
    {
        Logger.Write("Attempt to create Camera connection,(picococm)", TAG);
        Console.WriteLine("------------------------------------------->we are in Camera.Connect.");
        Picocom();
        Logger.Write("------------------------------------------->Camera Verify Connection By sending AT? command",TAG);
        VerifyConnection();
    }

    private static void VerifyConnection()
    {
        SocketStream.Send("AT?", "AT?     - this help",  "Camera");
    }

    public static void Picocom()
    {
        //host = ConnectionPointers.Host;
        //loginname = ConnectionPointers.Login;
        //password = ConnectionPointers.Password;
        SshClient cameraSocket = new SshClient(_host, _loginname, _password);
        cameraSocket.Connect();
        ConnectionPointers.CameraSocket = cameraSocket;
        ShellStream cameraSocketStream = cameraSocket.CreateShellStream("test", 80, 24, 800, 600, 1024);
        ConnectionPointers.Picocom = cameraSocketStream;// save camera strean to connetionPointers;
        string picocomRequest = "picocom -b 115200 /dev/tts/camera";
        string expectedContent = "Terminal ready";
        SocketStream.Send(picocomRequest, expectedContent, TAG);
        StringBuilder sb = new StringBuilder();
        string output = "";
        string buffer = "";
        while (true)
        {
            buffer = cameraSocketStream.ReadLine();
            Console.WriteLine($"Response String received: {buffer}.");
            sb.Append(buffer);
            if (buffer.Contains("Terminal ready"))
            {
                break;
            }
        }
        output = sb.ToString();
        Console.WriteLine(output);
    }
    public static void SendAT(string atCommand)
    {
        string expectedKeyWord = ATkeyWords.KeyWords[atCommand];
        //host = ConnectionPointers.Host;
        //loginname = ConnectionPointers.Login;
        //password = ConnectionPointers.Password;
        SshClient CameraSocket = new SshClient(_host, _loginname, _password);
        CameraSocket.Connect();
        ConnectionPointers.CameraSocket = CameraSocket;
        ShellStream CameraSocketStream = CameraSocket.CreateShellStream("test", 80, 24, 800, 600, 1024);
        CameraSocketStream.WriteLine("picocom -b 115200 /dev/tts/camera");
        StringBuilder sb = new StringBuilder();
        string output = "";
        string buffer = "";
        while (CameraSocketStream.CanRead)
        {
            buffer = CameraSocketStream.ReadLine();
            Console.WriteLine($"Response String received: {buffer}.");
            sb.Append(buffer);
            if (buffer.Contains("Terminal ready"))
            {
                break;
            }
        }
        output = sb.ToString();
        Console.WriteLine(output);
    }

    public static bool CreateSnapshot()
    {
        Console.WriteLine("------------------------------------------->we are in create snapshot");
        string CreateSnapshoQuery = "nc 127.0.0.1 4070 -e ./1.sh";
        Console.WriteLine("------------------------------------------->we are in create snapshot, before stream writeline");
        ConnectionPointers.SshDataChannel.WriteLine(CreateSnapshoQuery);
        //SshSocket.Send(CreateSnapshoQuery,"",VerificationType.None, "CreateSnapshot");
        Console.WriteLine("------------------------------------------->we are in create snapshot, after stream writeline");
        //TODO move return to verification if err in resp return false;
        return true;
    }

    public static void GetSnapshot(string localFilename)
    {
        string filename = localFilename.Trim(' ').Trim('\n').Trim('#').Trim('$');
        using (Stream fileStream = File.Create(filename))
        {
            SftpClient sftp = ConnectionPointers.SftpSocket;
            string remoteDirectory = "/tftpboot/boot/";
            sftp.ChangeDirectory(remoteDirectory);
            sftp.DownloadFile("1.jpg", fileStream);
        }

        //sftp.ChangeDirectory(remoteDirectory);
        //var LocalOutputFilePath = $"c:\\SnapShots\\";
        //Console.WriteLine("outputFolder: {0}", LocalOutputFilePath+ localFilename);
        //Stream ouputFile = File.Create(localFilename, );
        //Helper.StringExecute("ls");
        try
        {
            Console.WriteLine("-------------------------------------------DOWNLOAD SNAPSHOT --------------------------------");

            //
            //{
            //}

            //string remoteFileName = "1.jpg";
            //Stream SNAPSHOT  = File.Create(localFilename);
            //sftp.DownloadFile(remoteFileName, SNAPSHOT);
            //sftp.
            Console.WriteLine("------------------------------------------>1.JPG##############################################");

        }
        catch (Exception e)
        {
            Console.WriteLine($"------------------------------------------->download FAILED due-to Exception: {e.Message}");

        }
    }//getSnaphot

    public static void RequestProcessing<T>(T connection, string expectedConTent, String TAG)
    {
        //1 for sshClient  work with 
        //using (var cmd = ConnectionPointers.SshSocket.CreateCommand(ATcommand))
        //cmd.Execute();
        //2to work with ShellStream, ONLY strings applicable.
        int socketWorkflowLogic;

        if (connection.GetType() == typeof(SshClient))
        {
            socketWorkflowLogic = 1;
        }
        else
        {
            if (connection.GetType() == typeof(ShellStream))
                socketWorkflowLogic = 2;
        }
    }


    public static string sendAT(string ATcommand, int timeout = 0)
    {
        string atKeyWord = ATkeyWords.KeyWords[ATcommand];
        string _output = "";
<<<<<<< Camera.cs
        using (var cmd = ConnectionPointers.DeviceSocket.CreateCommand(ATcommand))
=======
        using (var cmd = ConnectionPointers.DeviceSshSocket.CreateCommand(ATcommand))
>>>>>>> Camera.cs
        {
            Console.WriteLine("<============================[ SEND AT Command ]============================>");
            Console.WriteLine($"<====[ messageToSend: {ATcommand} ]====>");
            try
            {
                var returned = cmd.Execute();
                _output = cmd.Result;
                Console.WriteLine($" Camera.cs: CodeLine 128;Exit Status: {cmd.ExitStatus} Output: {_output}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Camera.cs:sendAT;  CodeLines 98-100; Status: Exception thrown: {ex.Message}");
                return "Exception thrown.";
            }

            if ((_output.Contains("OK")) && (_output.Contains(atKeyWord)))
            {
                Assert.Pass($"ATA Command '{ATcommand}' Verification completed successfully.");
            }

            if (_output.Contains("ERR"))
            {
                return "Error 'ERR' content received.";
            }
            Thread.Sleep(timeout);
            return _output;                                                                                                                         
        }// end of uing();

    }// end of send AT
}// ond of class Camera.