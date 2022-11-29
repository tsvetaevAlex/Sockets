using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Xml;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework.Internal;
using Renci.SshNet;
using Renci.SshNet.Security.Cryptography;
using simicon.automation;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Shapes;
using System.Net.WebSockets;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using System.Security.Cryptography.X509Certificates;

namespace simicon.automation;

public static class Helper
//execute Command
{
    public static string outupFolder = "C:\\SnapShots";
    public static string prefix = "---------------------------------------->";
    private static string requestOutput = "";
    private static int _ExitStatus = 1;
    /// <summary>
    /// <param name="filename">Snapshot filename of new local file</param>
    public static void GetSnapdhot(string filename)
    {
        string host = ConnectionPointers.Host;
        string username = ConnectionPointers.Login;
        string password = ConnectionPointers.Password;
        string localFileName = System.IO.Path.GetFileName(filename);
        string remoteDirectory = "/tftpboot/boot/";
        string reportDir = "C:\\SnapShots";
        ConnectionPointers.SftpSocket.Connect();
        ConnectionPointers.SftpSocket.ChangeDirectory(remoteDirectory);

        Console.WriteLine("outputFolder: {0}", reportDir);
        //Stream ouputFile = File.Open(outputFilePath, FileMode.OpenOrCreate);
        Stream ouputFile = File.Create(outupFolder + filename);
        ConnectionPointers.SftpSocket.DownloadFile("1.jpg", ouputFile);




        //FileStream imageStream = 
        //var ImageStream = new FileStream(localdir, FileMode.Create);

        //string remotePath = "/tftpboot/boot/1.jpg";
        //ConP.ScpSocket.Download(remotePath, ImageStream);

        //Console.WriteLine(prefix + "cat /tftpboot/boot/1.jpg");
        //var Responsedata = cmd.Execute();
        //var output = cmd.Result;
        //int stat = cmd.ExitStatus;
        //Console.WriteLine(prefix + "ExitStatus: {0}", stat);
        //Console.WriteLine(prefix + "Result: {0}", output);
        //Console.WriteLine(prefix + "ResponseData: {0}", Responsedata);
        //DirectoryInfo solutionPath = GetSolutionDirectory();
        //string imagePath = solutionPath.FullName + "/ReportFolder/ct006.jpg";
        ////byte[] byteData = Encoding.ASCII.GetBytes(output);
        //File.AppendAllText(imagePath, output);

    }
    public static void StringExecute(string message)
    {
        prefix = $"Helper:StringExecute:> {message};";
        Envelope query = new Envelope(
                testname: "just a string comand execution on socket",
                request: message,
                expectedContent: "",
                vt: VerificationType.None
        );
        EnvelopeExecute(query);
    }
    public static void StringExecute(string message, SshClient _socket)
    {
        prefix = $"Helper:StringExecute:> {message};";
        Envelope query = new Envelope(
                testname: "just a string comand execution on socket",
                request: message,
                expectedContent: "",
                vt: VerificationType.None,
                socket: _socket
        );
        EnvelopeExecute(query);
    }
    public static void StringExecute(string message, VerificationType vt)
    {
        prefix = $"Helper:StringExecute:> {message};";
        Envelope query = new Envelope(
                request: message,
                vt: vt
        );
        EnvelopeExecute(query);
    }
    public static void EnvelopeExecute(Envelope ComData)
    {
        string RequestMsg = ComData.messageToSend;
        prefix = "Helper:EnvelopeExecute:> ";
        Debug.WriteLine(prefix + "ExecuteCommand");
        Debug.WriteLine(prefix + "Command: {0}", RequestMsg);

        string TAG = ComData.testName;


        // all in using{} on out from using {} all allocated memory will be released
        using (var cmd = ConnectionPointers.CameraSocket.CreateCommand(ComData.messageToSend))
        {
            Console.WriteLine("<============================[ RunCommand ]============================>");
            Console.WriteLine($"<====[ Test name: {ComData.testName} ]====>");
            Console.WriteLine($"<====[ messageToSend: {ComData.messageToSend} ]====>");
                try
                {
                    var returned = cmd.Execute();
                    var _output = cmd.Result;
                    Console.WriteLine($" Helper:CodeLine 106; Output before Trim: {_output}");
                    if (ComData.testName == "CamTest_picocom_vonnection")
                    {
                        Console.WriteLine("<============================[ CamTest_picocom_vonnection ]============================>");
                        Console.WriteLine(_output);
                    }

                    if (_output.Contains('\n'))
                    {
                        _output = _output.Trim('\n');
                    }
                    if (_output.Contains("ERR"))
                    {
                        Assert.Fail($"Request: {RequestMsg}, Failed due to ERR message received");
                        outupFolder = "\"C:\\\\SnapShots\\Failed";
                    }
                    Console.WriteLine($" Helper:CodeLine 112; Output After Trim: {_output}");
                    requestOutput = _output;
                    string prefix = Helper.prefix;
                    Console.WriteLine("\n" + prefix + $" Helper:CodeLine 115; requestOutput: '{0}'", requestOutput);
                    _ExitStatus = cmd.ExitStatus;
                    Console.WriteLine(prefix + $" Helper:CodeLine 117; ExitStatus: {0}", _ExitStatus);
                    var err = cmd.Error;
                } //try
                catch (Exception e)
                {
                    Assert.Fail("<==========================Exceptioon in RunCommand: {0}", e.Message);
                    //Logger.Warn("Exception extracting archive: " + e.Message);
                }
            #region Verify output
            // if verification type is None, e will check only Exit Status should 0 to PASS
            Console.WriteLine($" Helper:CodeLine 136; Actual result: {requestOutput}");
            Console.WriteLine($" Helper:CodeLine 137; Expected result: {ComData.expectedResponseContent}");

            int VerificationResult = 0;

                switch (ComData.VerT)
                {
                case VerificationType.Equal:
                    {
                        Console.WriteLine($"Verification Type [Equals]");
                        Assert.IsTrue(requestOutput.Equals(ComData.expectedResponseContent));
                        VerificationResult = 1;
                        Console.WriteLine($"Verification Type [Equals]: PASSED");

                        break;
                    }
                case VerificationType.Contains:
                    {
                        Console.WriteLine($"Verification Type [Contains]");
                        Assert.IsTrue(requestOutput.Contains(ComData.expectedResponseContent));
                        VerificationResult = 1;
                        Console.WriteLine($"Verification Type [Contains]: PASSED");

                        break;
                    }
                case VerificationType.None:
                {
                    Console.WriteLine($"Verification Type [None]");

                    if (requestOutput.Contains(ComData.expectedResponseContent)
                        && _ExitStatus.Equals(0))
                    {
                        bool result = true;
                        Assert.IsTrue(result);
                        VerificationResult = -1;
                        Console.WriteLine($"Verification Type [ExitStatus is 0]: PASSED");
                    }//if
                    break;
                }//case
                case VerificationType.NoResp: { break; }

                }// end of switch VerificationType

                switch (VerificationResult)
                { 
                case 0:
                    {
                        Console.WriteLine("Verification Result is FALSE");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("Verification Result is TRUE");
                        break;
                    }
                case -1:
                    {
                        Console.WriteLine("Verification Result is NONE, due-to it was request with no expected response");
                        break;
                    }
                }// end of switch VerificationResult

        }
        #endregion

            //Console.WriteLine("<============================[ RunCommand ]============================>");
            //Console.WriteLine($"<====[Reset Consolr Buffer ]====>");

            //var ResetCommand = ConnectionPointers.SshSocket.CreateCommand("Reset");
            //var ResetResult = ResetCommand.Execute();
            //Console.WriteLine($"\n<=================================Exit status: {ResetCommand.ExitStatus}.");

        }//end of using
}// end of class
