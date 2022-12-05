using System.Formats.Asn1;
using System.Runtime.CompilerServices;
using Renci.SshNet;
using simicon.automation;
using System.Text;

namespace simicon.automation.Tests.AT;

    public static class TestCase
    {

    public static void Run(string ATcommand, string ecpectedCOntent, string? TAG = null, int timeToWait = 0)
    {


        //RRecretae if null
        var Picocom = ConnectionPointers.Picocom;
        if (Picocom == null)
        {
            Logger.Write("receive Camera.NPE @ TC.Run. RecCreate Camera ShellStream connection","fixCameraNPE");
            var cfmSocket = ConnectionPointers.CameraSocket;
            ShellStream cameraStream = cfmSocket.CreateShellStream("", 0, 0, 0, 0, 0);
            Picocom = cameraStream;
        }

        Logger.Write($"Camera=P{Picocom} at received: {ATcommand} ","testCase.Run:");

        //public bool GetCaller(object caller, [CallerMemberName] string membername = "")
        //{
        //    Type callerType = caller.GetType();
        //    //This returns a value depending of type and method
        //    return true;
        //}

        #region Send AT command
        Logger.Write("Send at to camera through Camera.DataStream", "CameraWriteline");
        #endregion Send AT command
        //TODO: Camera is null
        Picocom.WriteLine(ATcommand);

        #region Receive response

        bool isContentReceived = false;
            bool IsOkReceived = false;


            while (Picocom.CanRead)
            {
                StringBuilder sb = new StringBuilder();
                string output = "";
                string buffer = "";
                buffer = Picocom.ReadLine();
                Logger.Write($"Response String received: {buffer}", TAG);

            sb.Append(buffer);
                if (buffer.Contains(ecpectedCOntent))
                {
                    isContentReceived = true;
                }

                if (buffer.Contains("OK("))
                {
                    IsOkReceived = true;
                }

                output = sb.ToString();
                Console.WriteLine(output);
            }

            if (isContentReceived && IsOkReceived)
            {
                Logger.Write($"AT command: {ATcommand}, Verification successfully passed. Expected content and OK #tag received", TAG);
                Assert.Pass();
            }

            #endregion

        }


    }

