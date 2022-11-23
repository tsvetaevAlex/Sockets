
using Renci.SshNet;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static simicon.automation.Base;
namespace simicon.automation;

public static class Camera
{
    public static void Conect()
    {
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in Camera.COnnect.");
        Picocom();
        Console.WriteLine(Base.GOP() + "------------------------------------------->Camera Verify COnnection By sending AT command 'AT?'.");
        if (VerifyConnection())
        {
            Console.WriteLine(Base.GOP() + "=======================================>camera connected successfully.");
        }
        else
        {
            Assert.Fail(Base.GOP() + "some impediments with camera connection");
        }
    }
    private static bool VerifyConnection()
    {
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in Camera.VerifyConnection.");

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
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in Camera.Picocom.");

        string host = ConnectionPointers.Host;
        string loginname = ConnectionPointers.Login;
        string password = ConnectionPointers.Password;
        SshClient CameraSocket = new SshClient(host, loginname, password);
        CameraSocket.Connect();
        ConnectionPointers.CameraSocket = CameraSocket;
        ShellStream CameraSocketStream = CameraSocket.CreateShellStream("test", 80, 24, 800, 600, 1024);
        ConnectionPointers.CameraDataChannel = CameraSocketStream;
        CameraSocketStream.WriteLine("picocom -b 115200 /dev/tts/camera");
        StringBuilder sb = new StringBuilder();
        string output = "";
        string buffer = "";
        while (true)
        {
            buffer = CameraSocketStream.ReadLine();
            Console.WriteLine(Base.GOP() + $"Response String received: {buffer}.");
            sb.Append(buffer);
            if (buffer.Contains(Base.GOP() + "Terminal ready"))
            {
                Assert.Pass();
                break;
            }//wndof if
        }//end of while
        output = sb.ToString();
        Console.WriteLine(output);
    }
    public static void SendAT(string atC)
    {
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in Camera.SendAT.");

        VerificationRecord AtVerificationData = VerificationList.GetVerificationForAT("AT?");

        string host = ConnectionPointers.Host;
        string loginname = ConnectionPointers.Login;
        string password = ConnectionPointers.Password;
        SshClient _Socket = ConnectionPointers.CameraSocket;
        _Socket.Connect();
        ShellStream CameraSocketStream = _Socket.CreateShellStream("test", 80, 24, 800, 600, 1024);
        CameraSocketStream.WriteLine("picocom -b 115200 /dev/tts/camera");
        StringBuilder sb = new StringBuilder();
        string output = "";
        string buffer = "";
        while (true)
        {
            buffer = CameraSocketStream.ReadLine();
            Console.WriteLine($"Response String received: {buffer}.");
            sb.Append(buffer);
            if (buffer.Contains("Terminal ready"))
            {
                Console.WriteLine(Base.GOP() + $"Picocom response: 'Terminal ready' received.Camera Connection PASSED.");
                break;
            }
        }
        output = sb.ToString();
        Console.WriteLine(output);
    }

    public static bool CreateSnapshot()
    {
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in create Camera.CreateSnapshot");
        Helper.StringExecute("pwd");
        Helper.StringExecute("ls");
        string CreateSnapshoQuery = "nc 127.0.0.1 4070 -e ./1.sh";
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in create snapshot, before stream writeline");
        ConnectionPointers.SshDataChannel.WriteLine(CreateSnapshoQuery);
        Helper.StringExecute("pwd");
        Helper.StringExecute("ls");
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in create snapshot, after stream writeline");
        //TODO move return to vwrification if err in resp return false;
        return true;
    }

    public static void GetSnapshot(string localFilename)
    {
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in create Camera.GetSnapshot");
        string filename = localFilename.Trim(' ').Trim('\n').Trim('#').Trim('$');
        using (Stream fileStream = File.Create(filename))
        {
            SftpClient sftp = ConnectionPointers.SftpSocket;
            string remoteDirectory = "/tftpboot/boot/";
            sftp.ChangeDirectory(remoteDirectory);
            sftp.DownloadFile("1.jpg", fileStream);
            Thread.Sleep(2000);
        }

        //sftp.ChangeDirectory(remoteDirectory);
        //var LocalOutputFilePath = $"c:\\SnapShots\\";
        //Console.WriteLine("outputFolder: {0}", LocalOutputFilePath+ localFilename);
        //Stream ouputFile = File.Create(localFilename, );
        //Helper.StringExecute("ls");

    }//getSnaphot



    private static VerificationRecord GetVerificationCriteriaForAT(string at, VerificationRecord criteriaRecord)
    {
        VerificationRecord CriteriaRecord;
     try
        {
            CriteriaRecord = VerificationList.GetVerificationForAT(at);
        }
        catch(NullReferenceException e)
        {
            Console.WriteLine(e.Message);
            string issueMessage = "NPE exception issued cause  AT command has been not found in Verification list.\\nWould you please verify actuality of VerificationList in[VerificationList.cs] and/or verify you requested AT command.";
            Console.WriteLine(issueMessage);
        }
        return criteriaRecord;
    }
    public static void VerifyATсommand(string At)
    {
        Console.WriteLine(GOP() + "------------------------------------------->we are in Camera.VerifyATсommand.");
        Console.WriteLine(GOP() + $"AT command; {At}");
        int Controlline = 1;
        VerificationRecord criteriaRecord;
       

        string SecondAttemptMessage = "WARNING!!! In order to keep the stability of automation application work we made retry count.\\n2nd attempt to get Verification Criteria for AT command";
        Console.WriteLine(SecondAttemptMessage);
        criteriaRecord = VerificationList.GetVerificationForAT(At);
        Console.WriteLine(GOP() + $"assigned CriteriaList: {criteriaRecord}");
        if (criteriaRecord.AT.Equals(At))
        {
            Console.WriteLine(GOP() + $"Verification record: {criteriaRecord}");
        }

        #region Sending AT Command
        ShellStream _Camera = ConnectionPointers.CameraDataChannel;
        _Camera.WriteLine(At);
        #endregion
        #region receive AT Response
        int ResponseStringNumber = 1;
        string[] ResponseStrings = new string[25];
        StringBuilder sb = new StringBuilder();
        String expectedContent = criteriaRecord.ExpecteContent;
        Controlline = criteriaRecord.ControlLine;
        string output = "";
        string buffer = "";
        while (_Camera.CanRead)
        {
            buffer = _Camera.ReadLine();
            if (ResponseStringNumber == Controlline)
            {
                Assert.IsTrue(buffer.Contains(expectedContent));
            }
            ResponseStrings.Append(buffer);
            ResponseStringNumber++;

            Console.WriteLine(GOP() + $"Response String received: {buffer}.");
            sb.Append(buffer);
            ResponseStringNumber++;
            output = sb.ToString();
            Console.WriteLine(GOP() + $"AT comand output: {output}");
        }// end of while

        #endregion

        #region //TODO Verification

        #endregion
        //todo implement
    }

    public static string sendAT(string atC)
    {
        using (var cmd = ConnectionPointers.SshSocket.CreateCommand(atC))
        {
            Console.WriteLine(Base.GOP() + "<============================[ SEND AT Command ]============================>");
            Console.WriteLine(Base.GOP() + $"<====[ messageToSend: {atC} ]====>");
            try
            {
                var returned = cmd.Execute();
                var _output = cmd.Result;
                Console.WriteLine(Base.GOP() + $" Camera.cs: CodeLine 128;Exit Status: {cmd.ExitStatus} Output: {_output}");
                if (_output.Contains("ERR"))
                {
                    return "Error 'ERR' content received.";
                }
                return _output;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Camera.cs:sendAT;  CodeLines 98-100; Status: Exception thrown: {ex.Message}");
                return "Exception thrown.";
            }
        }// enfd of uing();

    }// end of send AT
}// ond of class Camera.