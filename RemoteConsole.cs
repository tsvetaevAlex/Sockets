using simicon.automation.Tests;

namespace simicon.automation;

public class RemoteConsole : TestRun
{
    private string logPrefix = "==RemoteConsole==>@simicon.automation.RemoteConsole; COde Line: ";
    public void GetResponseSSH(string message)
    {   
        Response.Wipe();
        
        using (var cmd = SshClient.CreateCommand(message))
        {
            cmd.Execute();
            string output = cmd.Result;
            Response.output = cmd.Result;
            Response.ExitCode = cmd.ExitStatus;
            if (cmd.ExitStatus == 0)
            {
                log.Info($"GetResponse. Request: {message}");
                log.Info($"Response: {Response.output}");
            }
            else
            {
                log.Warning($"{logPrefix}25; GetResponseSSH({message}), return exit code: {cmd.ExitStatus}, with output: {cmd.Result}.");
            }
        }
    }

    //
    //{
    //    ShellStream shell = ConnectionPointers.GetDeviceShell();
    //    shell.WriteLine(message);
    //    StringBuilder sb = new StringBuilder();
    //    string buffer = string.Empty;
    //    while (shell.DataAvailable)
    //    {
    //        buffer = shell.ReadLine();
    //        if (buffer != "")
    //        {
    //            sb.Append(buffer);
    //        }
    //    }
    //    return sb.ToString();

    //}

    public void ExecuteBashCommand(string command)
    {
        using (var cmd = SshClient.CreateCommand(command))
        {
            cmd.Execute();
            Response.output = cmd.Result;
            Response.ExitCode = cmd.ExitStatus;
        }
    }

    public void SendBashCommand(string command)
    {
        log.Route($"RemoteConsole.SendBashCommand({command})");
        Response.Wipe();
        string _output = string.Empty;
        using (var cmd = SshClient.CreateCommand(command))
        {
            cmd.Execute();
            _output = cmd.Result;
            bool exit0 = cmd.ExitStatus == 0;
            Response.output = cmd.Result;
            Response.ExitCode = cmd.ExitStatus;
            if (cmd.ExitStatus == 0)
            {
                log.Info($"{logPrefix} Performed Command: {command}");
                log.Info($"{logPrefix} ResponseMessage: {_output}");
            }
        }
    }
    //public void SendBashShellCommand(string command)
    //{
    //    try
    //    {

    //        log.Info($"RemoteConsole.SendBashShellCommand: {command}");
    //        ShellStream shell = ConnectionPointers.GetDeviceShell();
    //        shell.WriteLine(command);
    //        StringBuilder sb = new StringBuilder();
    //        string buffer = string.Empty;
    //        while (shell.DataAvailable)
    //        {
    //            buffer = shell.ReadLine();
    //            if (buffer != "")
    //            {
    //                sb.Append(buffer);
    //            }
    //            log.Info($"RemoteConsole.SendBashShellCommand.Response: {sb.ToString()}");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Exception($"RemoteConsole.SendBashShellCommand: Exception has been thrown {ex.ToString}");
    //    }
    //}
    public void Send(string parent, string message, string expectedKeyWord)
    {
        log.Route($"---------->RemoteConsole.Send\\n message is: {message};\\nexpectedKeyWord is: {expectedKeyWord}");
        bool isKeyWOrdReceived = false;
        bool isExpectedEmpty = false;
        bool IsOkReceived = false;
        if (expectedKeyWord == "") isExpectedEmpty = true;
        string _output = "";
        log.Info($"---------->message to send: {message}");

        if (SshClient.IsConnected)
        {
            log.Info($"INFO@simicon.automation.RemoteConsole.Send is SSH connation alive?: {SshClient.IsConnected}");
            //log.Info("INFO@simicon.automation.RemoteConsole.Send attempt to attempt to make connection again");
        }
        using (var cmd = SshClient.CreateCommand(message))
        {
            cmd.Execute();
            _output = cmd.Result;
            bool exit0 = cmd.ExitStatus == 0;
            if (cmd.ExitStatus == 0)
            {
                if (_output == "")
                {
                    log.Info($"---------->RemoteConsole: executed command does not imply any response");
                }
                log.Info($"---------->RemoteConsole: ResponseMessage: {_output}");
                if (!isExpectedEmpty)
                {
                    if (_output.Contains(expectedKeyWord))
                    {
                        isExpectedEmpty = true;
                        isKeyWOrdReceived = true;
                    }
                    log.Info("---------->RemoteConsole: Response contains expected ressponse content and got Exit code 0,");
                }

                if (isExpectedEmpty && exit0)
                {

                    string summaryMessage =
                        "Verification successfully passed. Exit Code is 0 and expected Content has not been  provided.";
                    log.Info(summaryMessage);

                    log.Info(summaryMessage);

                }

                if (isKeyWOrdReceived && IsOkReceived && exit0)
                {
                    string summaryMessage =
                        $"AT command: {message},SUCCESS Verification successfully passed. Expected Content and OK #tag received";
                    log.Info(summaryMessage);
                }
                else
                {
                    if ((expectedKeyWord == "") && exit0)
                    {
                        log.Info("SUCCESS. ExitStatus is 0. output does not contains Expected Keyword.");
                    }
                }
                //if (_output.Contains("ERR"))
                //{
                //    string errMessage = $"Request: {message}, Failed due to output contains ERR token.";
                //    log.Write(errMessage,TAG);
                //    Assert.Fail(errMessage);
            }//if ExitCOde ==0
        }// end of using{};

    }// end of send(){}
}// end of class