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
            ShellStream camera= ConnectionPointers.SshCameraStream;


        //public bool GetCaller(object caller, [CallerMemberName] string membername = "")
        //{
        //    Type callerType = caller.GetType();
        //    //This returns a value depending of type and method
        //    return true;
        //}

        #region Send AT command
        camera.WriteLine(ATcommand);
        #endregion Send AT command

            #region Receive response

            bool isContentReceived = false;
            bool IsOkReceived = false;


            while (camera.CanRead)
            {
                StringBuilder sb = new StringBuilder();
                string output = "";
                string buffer = "";
                buffer = camera.ReadLine();
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

