using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Renci.SshNet;
using Xamarin.Forms;
using File = System.IO.File;

namespace simicon.automation;

public class Helper
{    //Commands executor ans others tuff

    public ConnectionPointers _Connections;

    public Helper(Device argdevice)
    {

        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint Helper constructor with Device");


        _Connections = argdevice.GetConnectionPointers();
    }
    public Helper(ConnectionPointers connections)
    {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint Helper constructor with ConnectionPointers");
        _Connections = connections;
    }

    public void Execute(Envelope ComData)
    {
        Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>> ExecuteCommand");
        Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>> Command: {0}", ComData.messageToSend);

        string TAG = ComData.testName;
        string output = "";
        int stat;

        // all in using{} on out from using {} all allocated memory will be released
        var cmd = _Connections.SshSocket.CreateCommand(ComData.messageToSend);
        {
            Debug.WriteLine("<============================[ START RunCommand ]============================>");
            Debug.WriteLine($"<====[ {ComData.messageToSend} ]====>");
            try
            {
                while (true)
                {
                    var CmdReturned = cmd.Execute();
                    output = cmd.Result;
                    if (cmd.ExitStatus != 0)
                    {
                        break;
                    }
                    output = output.Trim('\n');
                }
                Debug.WriteLine("\n" + ">>>>>>>>>>>>>>>>>>>>>>" + "Output: '{0}'", output);
                stat = cmd.ExitStatus;
                Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>" + "ExitStatus: {0}", stat);
                var err = cmd.Error;
                Debug.WriteLine("\nOutput: {0}", cmd.Result);
                Debug.WriteLine($"\n<=================================Exit status: [{stat}] ");
            } //try
            catch (Exception e)
            {
                Assert.Fail("<==========================Exceptioon in RunCommand: {0}", e.Message);
            }
            #region Verify output
            // if verification type is None, e will check only Exit Status should 0 to PASS
            Debug.WriteLine("<============================[ verify Results ]============================>");
            Debug.WriteLine($">>>>>>>>>>>>>>>>>>>>>ACTUAL  Content: {cmd.Result}");
            Debug.WriteLine($">>>>>>>>>>>>>>>>>>>>>EXPECTED Content: {ComData.expectedResponseContent}");
            Debug.WriteLine($">>>>>>>>>>>>>>>>>>>>> Comparison Type: {ComData.VerT}");

            switch (ComData.VerT)
            {
                case VerificationType.Equal:
                    {
                        Assert.IsTrue(output.Equals(ComData.expectedResponseContent));
                        Debug.WriteLine($">>>>>>>>>>>>>>>>>>>>> Equal verification PASSED");
                        break;
                    }
                case VerificationType.Contains:
                    {
                        Assert.IsTrue(output.Contains(ComData.expectedResponseContent));
                        Debug.WriteLine($">>>>>>>>>>>>>>>>>>>>> Contains verification PASSED");
                        break;
                    }
                case VerificationType.None:
                    {
                        Assert.IsTrue(cmd.ExitStatus.Equals(0));
                        break;
                    }
                default: { break; }
            }
            #endregion

        }// var cmd =
    }// end of Execute
}// end of class

