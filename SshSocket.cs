namespace simicon.automation;

public static class SshSocket
{
    private static readonly string TAG = "SshSocket";


    public static string GetResponse(string message)
    {

        SshClient conn = ConnectionPointers.DeviceSocket;
        using (var cmd = conn.CreateCommand(message))
        {
            cmd.Execute();
            string output = cmd.Result;
            bool exit0 = cmd.ExitStatus == 0;
            if (cmd.ExitStatus == 0)
            {
                return output;
            }
            else
            {
                return "400";
            }
        }

    }


        public static void Send(string message, string expectedKeyWord)
    {
        Logger.Write($"we are in SshSocket.Send\\n message is: {message};\\nexpectedKeyWord is: {expectedKeyWord}", TAG);
        bool isKeyWOrdReceived = false;
        bool isExpectedEmpty = false;
        bool IsOkReceived = false;
        if (expectedKeyWord == "") isExpectedEmpty = true;
        string _output = "";
        Logger.Write($"message to send: {message}", TAG);

            SshClient conn = ConnectionPointers.DeviceSocket;
        using (var cmd = conn.CreateCommand(message))
        {
            cmd.Execute();
            _output = cmd.Result;
            bool exit0 = cmd.ExitStatus == 0;
            if (cmd.ExitStatus == 0)
            {
                if (!isExpectedEmpty)
                {
                    if (_output.Contains(expectedKeyWord))
                    {
                        isExpectedEmpty = true;
                        isKeyWOrdReceived = true;
                    }
                }

                if (isExpectedEmpty && exit0)
                {

                    string summaryMessage =
                        "Verification successfully passed. Exit Code is 0 and expected content has not been  provided.";
                        Logger.Write(summaryMessage, TAG);
                        Assert.Pass(summaryMessage);
                        Console.WriteLine(summaryMessage);

                }

                if (isKeyWOrdReceived && IsOkReceived && exit0)
                {
                    string summaryMessage =
                        $"AT command: {message}, Verification successfully passed. Expected content and OK #tag received";
                    Logger.Write(summaryMessage, TAG);
                    Assert.Pass(summaryMessage);
                }
                else
                {
                    if (!isKeyWOrdReceived)
                    {
                        Logger.Write("FAIL. output does not contains Expected Keyword.", TAG);
                    }
                }
                //if (_output.Contains("ERR"))
                    //{
                    //    string errMessage = $"Request: {message}, Failed due to output contains ERR token.";
                    //    Logger.Write(errMessage,TAG);
                    //    Assert.Fail(errMessage);
            }//if ExitCOde ==0
        }// end of using{};

    }// end of send(){}
}// end of class

