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

namespace simicon.automation;

public  class CommandHelper
    //execute Command
{
    private ConnectionPointers ConP { get; set; }
    public string prefix = ">>>>>>>>>>>>>>>>>>>>>>";
    ExecutionResult execRes = new ExecutionResult();
    public CommandHelper(ConnectionPointers conP)
    {
        ConP = conP;
        this.prefix = "=================================>>>";
    }


    public static DirectoryInfo GetSolutionDirectory(string currentPath = null)
    {
        var directory = new DirectoryInfo(
            currentPath = Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory;
    }
    public void GetSnapdhot()
    {
        Debug.WriteLine(prefix + "Attempt to GetSnaphot [1.jpg] from Device");
        //TODO var path = TryGetSolutionDirectory();
        //using (var cmd = ConP.socket.CreateCommand()
        ConP.DataChannel.WriteLine(".cat ./tftpboot/boot/1.jpg/1.jpg");
        string content = "";

        string filename = GetSolutionDirectory().FullName + "/ReportFolder/ct006.jpg";

            while (ConP.DataChannel.CanRead)
            {
            string temp =  ConP.DataChannel.Read();
            content = content + temp;
            }
        FileStream imageStream = new FileStream(filename, FileMode.Create);
        byte[] buffer = Encoding.Default.GetBytes(content);
        // запись массива байтов в файл
        imageStream.WriteAsync(buffer, 0, buffer.Length);

        // операции с fstream
        //var image = new File.Create(filename);



        /*
        DirectoryInfo path = TryGetSolutionDirectory();
        var returned = cmd.Execute();
        var Snaphot = cmd.Result;
        Console.WriteLine("\n" + prefix + "Output: '{0}'", Snaphot);
        var stat = cmd.ExitStatus;
        Console.WriteLine(prefix + "ExitStatus: {0}", stat);
        string Filename = DateTime.Now.ToString("MM/dd/yyyy_h:mm:tt");
        Filename = Filename + ".jpg";
        using (FileStream fs = File.Create($"{path.FullName}\\ReportFoLder/{Filename}"));
        */
    }



    public void Execute(ConnectionPointers cp, string command)
    {
        Envelope testData = new Envelope(
    testname: "Reest SOcket console",
    request: "Reset",
    expectedContent: "",
    vt: VerificationType.None
    );
    }

        public void Execute(ConnectionPointers cp,Envelope ComData)
            {
            Debug.WriteLine(prefix + "ExecuteCommand");
            Debug.WriteLine(prefix + "Command: {0}", ComData.messageToSend);

            string TAG = ComData.testName;


        // all in using{} on out from using {} all allocated memory will be released
        using (var cmd = ConP.socket.CreateCommand(ComData.messageToSend))
        {
            Console.WriteLine("<============================[ RunCommand ]============================>");
            Console.WriteLine($"<====[ {ComData.messageToSend} ]====>");
            try
            {
                var returned = cmd.Execute();
                var output = cmd.Result;
                if (output.Contains('\n'))
                {
                    execRes.output = output.Trim('\n');
                }
                    Console.WriteLine("\n"+ prefix + "Output: '{0}'", output);
                var stat = cmd.ExitStatus;
                Console.WriteLine(prefix + "ExitStatus: {0}", stat);
                var err = cmd.Error;
                var result = ConP.socket.RunCommand(ComData.messageToSend);
                Console.WriteLine("\nOutput: {0}", result.Result);
                var ExitStat = ConP.socket.RunCommand($"{TAG} echo $?");
                Console.WriteLine($"\n<=================================Exit status: {ExitStat.Result} | {stat}");
                var exit = ExitStat.Result;

                var arrRsp = exit.Split(new[] { "\n" }, StringSplitOptions.None);

                if (arrRsp.Length > 0)
                    if (arrRsp[0] == "0")
                        Console.WriteLine(prefix + $"echo $?: is... {arrRsp[0]};");

            } //try
            catch (Exception e)
            {
                Assert.Fail("<==========================Exceptioon in RunCommand: {0}", e.Message);
                //Logger.Warn("Exception extracting archive: " + e.Message);
            }
            #region Verify output
            // if verification type is None, e will check only Exit Status should 0 to PASS

            switch (ComData.VerT)
            {
                case VerificationType.Equal:
                    {
                        Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>output: {execRes.output}");
                        Assert.IsTrue(execRes.output.Equals(ComData.expectedResponseContent));
                        break;
                    }
                case VerificationType.Contains:
                    {
                        Assert.IsTrue(execRes.output.Contains(ComData.expectedResponseContent));
                        break;
                    }
                case VerificationType.None:
                    {
                        Assert.IsTrue(execRes.ExitStatus.Equals(0));
                        break;
                    }
            }
            #endregion

            Console.WriteLine("<============================[ RunCommand ]============================>");
            Console.WriteLine($"<====[Reset Consolr Buffer ]====>");

            var ResetCommand = ConP.socket.CreateCommand("Reset");
            var ResetResult = ResetCommand.Execute();
            Console.WriteLine($"\n<=================================Exit status: {ResetCommand.ExitStatus}.");

        }//end of using
    }// end of Execute
    }// end of class

