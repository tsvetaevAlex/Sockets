using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

    public static class SshSocket
    {
        public static void Send(SshClient conn, string message, string expectedKeyWord, string TAG = "Send. SshSocket")
        {
            Logger.Write($"message to send: {message}", "SshSocket");

            bool isKeyWOrdReceived = false;
            bool IsOkReceived = false;
            string _output = "";
        using (var cmd = conn.CreateCommand(message))
        {
            cmd.Execute();
            _output = cmd.Result;

            OutputVerification.Verify(
                command: message,
                output: _output,
                expectedContent: expectedKeyWord);


            if (_output.Contains(expectedKeyWord))
            {
                isKeyWOrdReceived = true;
            }

            if (_output.Contains("OK("))
            {
                IsOkReceived = true;
            }

            if (isKeyWOrdReceived && IsOkReceived)
            {
                Logger.Write($"AT command: {message}, Verification successfully passed. Expected content and OK #tag received", TAG);
                Assert.Pass();
            }

        }



    }
}

