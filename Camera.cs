using Renci.SshNet;
using System.Text;

namespace simicon.automation;

public static class Camera
{

    private static readonly string _host = ConnectionPointers.Host;
    private static readonly string _loginname = ConnectionPointers.Login;
    private static readonly string _password = ConnectionPointers.Password;
    public static void Conect()
    {
        Console.WriteLine("------------------------------------------->we are in Camera.Connect.");
        Picocom();
        Console.WriteLine("------------------------------------------->Camera Verify Connection By sending AT command 'AT?'.");
        if (VerifyConnection())
        {
            Console.WriteLine("=======================================>camera connected successfully.");
        }
        else
        {
            Assert.Fail("some impediments with camera connection");
        }
    }
    private static bool VerifyConnection()
    {
        bool result = true;
        string response = sendAT("AT?");
        if (response.Equals("Error 'ERR' content received."))
        {
            result = false;
        }
        if (response.Equals("Exception thrown."))
        {
            result = false;
        }
        return result;

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
        cameraSocketStream.WriteLine("picocom -b 115200 /dev/tts/camera");
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
        Helper.StringExecute("pwd");
        Helper.StringExecute("ls");
        string CreateSnapshoQuery = "nc 127.0.0.1 4070 -e ./1.sh";
        Console.WriteLine("------------------------------------------->we are in create snapshot, before stream writeline");
        ConnectionPointers.SshDataChannel.WriteLine(CreateSnapshoQuery);
        Helper.StringExecute("pwd");
        Helper.StringExecute("ls");
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

    public static string sendAT(string ATcommand, int timeout = 0)
    {
        string atKeyWord = ATkeyWords.KeyWords[ATcommand];
        string _output = "";
        using (var cmd = ConnectionPointers.SshSocket.CreateCommand(ATcommand))
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