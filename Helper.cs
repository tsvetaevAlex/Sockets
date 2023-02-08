namespace simicon.automation;

public static class Helper
{
    //private StringBuilder entireResponse = new StringBuilder();

    private static string TAG = "automation.Helper";
    private static string FullResponse = "";


    public static string Send(string message, string ExpectedKeyWord, string TAG, bool returnResponse = false)
    {
        Logger.Write("has entered into Send()", "TraceRoute");
        ShellStream camera = ConnectionPointers.GetCameraStream();

        //VARIABLES
        string buffer = "";
        StringBuilder entireResponse = new StringBuilder();
        string output = string.Empty;

        //SEND
        camera.WriteLine(message);

        //Receive response
        while (true)
        {
            try
            {
                if (camera.DataAvailable)
                {
                    buffer = camera.Read();
                    if (buffer != "")
                    {

                        Logger.Write($"response line: {buffer}", TAG);
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
                    Logger.Write("receive Response ended due to stream dataAvailalbe is false.","Helper");
                    break;
                }
            }

            catch (Exception e)
            {
                Logger.Write($"Send command issue has been happend: {e.Message}", TAG);
                return output;

            }
            Logger.Write($"Response String received: " + output, TAG);

            Thread.Sleep(250);
        }// end of while
        return output;

    }

    public static void ClarifySensorType()
    {
        Verify(
            input: "AT2",
            ExpectedKeyWord: "",
            _TAG: "ClarifySensorType",
            returnResponse: true
            );
        if (FullResponse.Contains("Night mode is for color sensors only!"))
        {
            Globals.CameraType = SensorType.BW;
        }
        else if (FullResponse.Contains("Saturation"))
        {
            Globals.CameraType = SensorType.Color;
        }
    }

    public static void Verify(string input, string ExpectedKeyWord, string _TAG, bool returnResponse = false)
    {
        Logger.Write("has entered into Verify()", "TraceRoute");

        TAG = _TAG;
        string Command = input;

        string fullOutput = String.Empty;
        fullOutput= Send(Command, ExpectedKeyWord, _TAG,true);
        
        Logger.Write("-------------------- Cut Line ----------------------------------------", TAG);
        Logger.Write($"Verify 'AT': [{Command}]", TAG);
        Logger.Write($"where Expected response COntent is: [{ExpectedKeyWord}]", TAG);
        Logger.Write("--------------------", TAG);


        ShellStream camera = ConnectionPointers.GetCameraStream();

        //SEND
        camera.WriteLine(Command);
        Thread.Sleep(1000); // Prevents the shell from losing the output if it becomes too slow

        #region Send AT to picocom
        bool isContentReceived = false;
        bool isOkReceived = false;
        //ShellStream camera = ConnectionPointers;
        try
        {

            string buffer = "";
            bool isKeyWordReceived = false;
            StringBuilder entireResponse = new StringBuilder();
            Logger.Write($"connection VerifyCameraSuite. camera stream pointer:  '{camera}'", TAG);
//WHILE

            while (true)
            {
                try
                {
                    if (camera.DataAvailable)
                    {
                        buffer = camera.Read();
                        if (buffer != "")
                        {
                            Logger.Write($"response line: {buffer}", TAG);
                            entireResponse.Append(buffer);
//VERIFICATION
                            #region instant verification

                            if (fullOutput.Contains(ExpectedKeyWord))
                            {
                                Logger.Write($"is excepted content received: {buffer.Contains(ExpectedKeyWord)}", TAG);
                                isKeyWordReceived = true;
                            }
                            if (buffer.Contains("OK"))
                            {
                                isOkReceived = true;
                                Logger.Write($"'OK' token received: {buffer}", TAG);
                                Logger.Write($"is OK tag received: {buffer.Contains("OK")}", TAG);
                                break;
                            }

                            if (fullOutput.Contains("ERR"))
                            {
                                string message =
                                    $"TEstCase: AT command: {Command} execution FAIL," +
                                    $"due to ERR token has been received: '{buffer}'";
                                Logger.Write(message, TAG);
                                Assert.Fail(message);
                            }
//CONCLUSION
                            #region conclusion
                            Logger.Write($"Conclusionbased on Flags:\\r\\n isKeyWordReceived= {isKeyWordReceived}' and isOkReceived={isOkReceived}", TAG);
                            if (isContentReceived && isOkReceived)
                            {
                                string conclusionMessage =
                                    $"AT command: {Command}, Verification successfully passed. Expected content and OK #tag received";
                                Logger.Write(conclusionMessage, TAG);
                                Assert.Pass(conclusionMessage);
                            }
                            #endregion

                        }
                        #endregion

                    }
                    FullResponse = entireResponse.ToString();
                }

                catch (Exception e)
                {
                    Logger.Write($"exception has been happend: {e.Message}", TAG);
                }
                Logger.Write($"Response String received: " + buffer, TAG);

                Thread.Sleep(250);
            }// end of while
            Thread.Sleep(500);

            #endregion


        }
        catch (Exception e)
        {
            Logger.Write($"during: sending AT to Camera using camera data channel. Exception has been thrown.", TAG);
            Logger.Write($"Exception message:  {e.Message}", TAG);

        }
    }// en of verify

}
/*
 instatnt verification backup
       buffer = camera.Read();
                        if (buffer != "")
                        {
                            Logger.Write($"response line: {buffer}", TAG);
                            entireResponse.Append(buffer);
                            #region instatnt verification

                            if (buffer.Contains(ExpectedKeyWord))
                            {
                                Logger.Write($"is excepted content received: {buffer.Contains(ExpectedKeyWord)}", TAG);
                                isKeyWordReceived = true;
                            }
                            if (buffer.Contains("OK"))
                            {
                                isOkReceived = true;
                                Logger.Write($"'OK' token received: {buffer}", TAG);
                                Logger.Write($"is OK tag received: {buffer.Contains("OK")}", TAG);
                                break;
                            }

                            if (buffer.Contains("ERR"))
                            {
                                string message =
                                    $"TEstCase: AT command: {Command} execution FAIL," +
                                    $"due to ERR token has been received: '{buffer}'";
                                Logger.Write(message, TAG);
                                Assert.Fail(message);
                            }

                            #region conclusion
                            Logger.Write($"Conclusionbased on Flags:\\r\\n isKeyWordReceived= {isKeyWordReceived}' and isOkReceived={isOkReceived}", TAG);
                            if (isContentReceived && isOkReceived)
                            {
                                string conclusionMessage =
                                    $"AT command: {Command}, Verification successfully passed. Expected content and OK #tag received";
                                Logger.Write(conclusionMessage, TAG);
                                Assert.Pass(conclusionMessage);
                            }
                            #endregion

                        }
                        #endregion
 
 */