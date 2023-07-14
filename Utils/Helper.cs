using simicon.automation.Tests;

namespace simicon.automation;

public class Helper : TestRun
{
    //private StringBuilder entireResponse = new StringBuilder();

    private string FullResponse = "";
    private string resultmessage = "";
    private string logPrefix = "simicon.automation.Helper, code line: ";
    private DataHeap DataHeap = new DataHeap();


    public Helper()
    {
        FullResponse = string.Empty;
        resultmessage = string.Empty;
        connections.InitCameraConsole();
        ShellStream connection = connections.GetPicococm();
        logPrefix = "simicon.automation.Helper, code line: ";

    }

    //TODO  killall picocom as bool wil log output
    // if ""then no active ipcocom found else log.info(output)


    public string Send(dynamic connection, string message, string ExpectedKeyWord, bool returnResponse = false)
    {
        log.Route($"Helper.Send({message})");

        //VARIABLES
        string buffer = "";
        StringBuilder entireResponse = new StringBuilder();
        string output = string.Empty;

        //SEND
        connection.WriteLine(message);

        //Receive response
        while (true)
        {
            try
            {
                if (connection.DataAvailable)
                {
                    buffer = connection.Read();
                    if (buffer != "")
                    {

                        log.Info($"response line: {buffer}");
                        entireResponse.Append(buffer);
                        if (buffer.Contains(ExpectedKeyWord))
                        {
                            break;
                        }
                    }
                    output = entireResponse.ToString();
                }
                else
                {

                    break;
                }
            }

            catch (Exception e)
            {
                log.Failure($"Send Command issue has been happend: {e.Message}");
                return output;

            }
            log.Info($"Response String received: " + output);

            Thread.Sleep(250);
        }// end of while
        return output;

    }


    public void Send(string command)
    {
        log.Route($"Helper.Send({command})");


        try
        {
            if (CameraConsole is not null)
            {
                CameraConsole.WriteLine(command);
            }
        }
        catch (Exception ex)
        {
            log.Exception($"Helper.Send({command}) Exception: {ex.ToString}");
        }
        log.Info($"command '{command}': successfully sent.");
    }


    public string ClarifySensorType()
    {

        string returnValue = string.Empty;

        string fullOutput = Send(
            connection: CameraConsole,
            message: "AT2",
            ExpectedKeyWord: "",
            returnResponse: true);

        if (fullOutput.Contains("Night mode is for color sensors only!"))
        {
            returnValue = "BW";
            
        }
        else if (fullOutput.Contains("Saturation"))
        {
            returnValue = "Color";
            
        }
        DataHeap.CameraType = returnValue;
        return returnValue;
    }

    public string Verify(RequestDetails request)
    {
        log.TestCase($"______________________________Test Case: {reportRow.ID}______________________________ss");
        log.TestCase($"Veerify> Command:{request.Command}");
        log.TestCase($"Veerify> Expected Content/KeyWords:{request.ExpectedContent}");

        connections.InitCameraConsole();
        log.TestCase($"Verify: connection(connection as ShellStream): {CameraConsole}");
        string TAG = reportRow.ID;
        string Command = request.Command;
        string expectedTextContent = request.ExpectedContent;
        string fullOutput = String.Empty;
        fullOutput = Send(CameraConsole, Command, expectedTextContent, true);

        
        //SEND
        CameraConsole.WriteLine(Command);
        Thread.Sleep(1000); // Prevents the shell from losing the output if it becomes too slow

        bool isContentReceived = false;
        bool isOkReceived = false;
        StringBuilder entireResponse = new StringBuilder();
        //ShellStream connection = ConnectionPointers;
        try
        {
            string buffer = "";
            bool isExpectedKeyWordReceived = false;
            bool isTestCaseSuccessfullyCompleted = false;
            bool isERRreceived = false;
            if (CameraConsole is not null)
            {
                //log.Warning("connection connection is null(broken)");
                //WHILE Receiving responsw strings
                if (expectedTextContent == "")
                {
                    isExpectedKeyWordReceived = true;
                }

                while (true)
                {
                    try
                    {
                        if (CameraConsole.DataAvailable)
                        {
                            buffer = CameraConsole.Read();
                            //Instant Verification
                            if (buffer != "")
                            {
                                if (Command == "ATV")
                                {
                                    if (buffer.StartsWith("Imx"))
                                    {
                                        log.TestCase("$AT Command: {Command}, Allowed Key Words Received.");
                                        log.TestCase($"Firmware Version: {buffer}");
                                        DataHeap.firmwareVersion = buffer;
                                    }
                                    log.Info($"\r\nresponse line: {buffer}");
                                }
                                entireResponse.Append(buffer);
                                if (fullOutput.Contains("nothing to change"))
                                {
                                    log.Info($"repsonse: {buffer}; accepted as positive");
                                    isExpectedKeyWordReceived = true;
                                    log.Info($"{logPrefix}169. isAllowedContentceived chsnge state to : {isExpectedKeyWordReceived}");
                                    log.TestCase("$AT Command: {Command}, Allowed Key Words Received.");
                                }
                                if (fullOutput.Contains(expectedTextContent))

                                {
                                    log.TestCase ("Success: Expected text Content receeived; test Step:Passed");

                                    isExpectedKeyWordReceived = true;
                                    resultmessage = $"AT Command: {Command}, Expected Key Words Received; ";
                                }
                                if (buffer.Contains("OK"))
                                {
                                    isOkReceived = true;
                                    log.TestCase($"Success:'OK' token received: {buffer}; test Step:Passed");
                                    resultmessage = resultmessage + " + 'OK' token received.";
                                    log.TestCase($"Success: AT Command: {Command}, Allowed Key Words Received.");
                                    isTestCaseSuccessfullyCompleted = true;
                                    break;
                                }
                                if (buffer.Contains("ERR ("))
                                {
                                    isERRreceived = true;
                                    if (isExpectedKeyWordReceived == false)
                                    {
                                        log.Info($"{logPrefix}190. isExpectedKeyWordReceived chsnge state to : {isExpectedKeyWordReceived}; due-to ERR token received");
                                        resultmessage =
                                        $"ASSERT TEstCase: AT Command: {Command} Test execution FAIL, expected key words was not received" +
                                        $"AND ERR token has been received: '{buffer}';";
                                        log.Failure("ERR");
                                        break;
                                    }
                                }

                                log.Info("________________________________Conclusion ________________________________");

                                //CONCLUSION
                                log.Info($"{logPrefix}204. Trace Verification flags.");
                                if (isExpectedKeyWordReceived && isOkReceived)
                                {
                                    isTestCaseSuccessfullyCompleted = true;

                                    resultmessage = "PASSED. in accordingly with received answers I can approve that Test Case successfully completed.";
                                    break;
                                }
                                if (isExpectedKeyWordReceived && isOkReceived)
                                {
                                    isTestCaseSuccessfullyCompleted = true;
                                    log.Info($"{logPrefix}227conclusion, based on the obtained data");
                                    log.Info($"Flags expectedTextContent={expectedTextContent} and isERRreceived={isERRreceived}");
                                    log.Info($"{logPrefix}229. isTestCaseSuccessfullyCompleted chsnge state to : {isTestCaseSuccessfullyCompleted}");
                                    resultmessage = "PASSED. in accordingly with received answers I can approve that Test Case successfully completed.";
                                    break;
                                }
                                if (fullOutput.Contains("nothing to change"))
                                {
                                    log.Info($"Flags expectedTextContent={expectedTextContent} and isERRreceived={isERRreceived}");
                                    log.Info($"repsonse: {buffer}; Allowrd content received.Accepted as positive");
                                    isExpectedKeyWordReceived = true;
                                    log.Info($"{logPrefix}218. isTestCaseSuccessfullyCompleted chsnge state to : {isTestCaseSuccessfullyCompleted}");
                                    log.Success($"AT Command: {Command}, Allowed Key Words Received.");
                                }
                                // nwe case from 4-11-2023
                                if (isExpectedKeyWordReceived && isERRreceived)
                                {
                                    log.Info($"Flags expectedTextContent={expectedTextContent} and isERRreceived={isERRreceived}");
                                    isTestCaseSuccessfullyCompleted = true;
                                    log.Info($"{logPrefix}218. isTestCaseSuccessfullyCompleted chsnge state to : {isTestCaseSuccessfullyCompleted}");
                                    resultmessage = "PASSED. in accordingly with received answers I can approve that Test Case successfully completed.";
                                    break;
                                }
                                if (!isExpectedKeyWordReceived && isERRreceived)
                                {   
                                    log.Info($"Flags expectedTextContent={isExpectedKeyWordReceived} and isERRreceived={isERRreceived}");
                                    isTestCaseSuccessfullyCompleted = false;
                                    log.Info($"{logPrefix}218. isTestCaseSuccessfullyCompleted chsnge state to : {isTestCaseSuccessfullyCompleted}");
                                    resultmessage = "FAILURE. in accordingly with received answers I can approve that Test Case Failed.";
                                    break;
                                }
                                if ((expectedTextContent == "") && (isOkReceived))
                                {
                                    log.Info($"Flags expectedTextContent={expectedTextContent} and isERRreceived={isERRreceived}");
                                    isTestCaseSuccessfullyCompleted = true;
                                    log.Info($"{logPrefix}218. isTestCaseSuccessfullyCompleted chsnge state to : {isTestCaseSuccessfullyCompleted}");
                                    resultmessage = "PASSED. in accordingly with received answers I can approve that Test Case successfully completed.";
                                    break;
                                }
                                if ((expectedTextContent == "") && (isERRreceived))
                                {
                                    log.Info($"Flags expectedTextContent={expectedTextContent} and isERRreceived={isERRreceived}");
                                    isTestCaseSuccessfullyCompleted = false;
                                    log.Info($"{logPrefix}218. isTestCaseSuccessfullyCompleted chsnge state to : {isTestCaseSuccessfullyCompleted}");
                                    resultmessage = "FAILURE. in accordingly with received answers I can approve that Test Case Failed.";
                                    break;
                                }

                            }// if buffer != ""
                        }
                        Assert.IsTrue(isTestCaseSuccessfullyCompleted);
                        FullResponse = entireResponse.ToString();
                    }// end of try
                    catch (Exception e)
                    {
                        log.Failure($"exception has been happend: {e.Message}");
                    }

                    Thread.Sleep(250);
                }// end of while
                Thread.Sleep(500);
            }//end of if(connection is not null)
//CONCLUSION
            log.Info($"Conclusion based on Flags:\\r\\n isExpectedKeyWordReceived= {isExpectedKeyWordReceived}' and isOkReceived={isOkReceived}");
            if (isContentReceived && isOkReceived)
            {
                string conclusionMessage =
                    $"AT Command: {Command},SUCCESS Verification successfully passed. Expected Content and OK #tag received";
                log.Info(conclusionMessage);
            }
        }
        catch (Exception e)
        {
            log.Failure($"during: sending AT to connection using connection data channel. Exception has been thrown.");
            log.Failure($"Exception message:  {e.Message}");
        }
        return entireResponse.ToString();
        }// end of verify

}