using simicon.automation.Tests;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;

namespace simicon.automation;

public static class Helper
{
    //private StringBuilder entireResponse = new StringBuilder();

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
                    Logger.Write("receive Response ended due to stream dataAvailalbe is false.", "Helper");
                    break;
                }
            }

            catch (Exception e)
            {
                Logger.Write($"Send Command issue has been happend: {e.Message}", TAG);
                return output;

            }
            Logger.Write($"Response String received: " + output, TAG);

            Thread.Sleep(250);
        }// end of while
        return output;

    }

    public static void ClarifySensorType()
    {
        string fullOutput = Send("AT2", "", "ClarifySensorType", true);

        if (FullResponse.Contains("Night mode is for color sensors only!"))
        {
            Globals.CameraType = SensorType.BW;
        }
        else if (FullResponse.Contains("Saturation"))
        {
            Globals.CameraType = SensorType.Color;
        }
    }

    public static void Verify(string ATcommand, string expectedContent, string _tag)
    {
        Verify(new RequestDetails(
            inputCommand: ATcommand,
            expectedTextContent: expectedContent,
            TAG: _tag
        ));
    }
    public static string Verify(RequestDetails request)
    {
        AutomationPrepareEnvironment.VerifyTestEnvironment();
        Logger.Write("has entered into Verify()", "TraceRoute");

        
        string TAG = request.tag;
        string Command = request.Command;
        string expectedTextContent = request.ExpectedContent;

        string fullOutput = String.Empty;
        fullOutput= Send(Command, expectedTextContent, TAG,true);
        Snapshot.Get("Before_ATF2.jpg", request.Command);
Logger.Write("-------------------- Cut Line ----------------------------------------", TAG);
        Logger.Write($"Verify 'AT': [{Command}]", TAG);
        Logger.Write($"where Expected response COntent is: [{expectedTextContent}]", TAG);
        Logger.Write("--------------------", TAG);


        ShellStream camera = ConnectionPointers.GetCameraStream();

        //SEND
        camera.WriteLine(Command);
        Thread.Sleep(1000); // Prevents the shell from losing the output if it becomes too slow

        #region Send AT to picocom
        bool isContentReceived = false;
        bool isOkReceived = false;
        StringBuilder entireResponse = new StringBuilder();
        //ShellStream camera = ConnectionPointers;
        try
        {

            string buffer = "";
            bool isKeyWordReceived = false;
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
                            Logger.Write($"\r\nresponse line: {buffer}", TAG);
                            entireResponse.Append(buffer);
                            //VERIFICATION
                            #region instant verification
                            if (buffer.Contains("ERR"))
                            {
                                string message =
                                    $"TEstCase: AT Command: {Command} Test execution FAIL," +
                                    $"due to ERR token has been received: '{buffer}'";
                                Logger.Write(message, TAG);
                                Assert.Fail(message);
                                break;
                            }
                            if (fullOutput.Contains(expectedTextContent))
                            {

                                Logger.Write($"excepted text content {buffer}", TAG);
                                Logger.Write("Expected text content receeived; test Step:Passed", TAG);
                                isKeyWordReceived = true;
                            }
                            if (buffer.Contains("OK"))
                            {
                                isOkReceived = true;
                                Logger.Write($"'OK' token received: {buffer}; test Step:Passed", TAG);
                                break;
                            }
                            //CONCLUSION
                            #region conclusion
                            Logger.Write($"Conclusionbased on Flags:\r\n isKeyWordReceived= {isKeyWordReceived}'\r\nisOkReceived={isOkReceived}", TAG);
                            if (isContentReceived && isOkReceived)
                            {
                                string conclusionMessage =
                                    $"AT Command: {Command}, Verification successfully passed. Expected content and OK #tag received";
                                Logger.Write(conclusionMessage, TAG);
                                Assert.Pass(conclusionMessage);
                            }
                            #endregion

                        }// if buffer != ""
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
//CONCLUSION
            #region conclusion
            Logger.Write($"Conclusionbased on Flags:\\r\\n isKeyWordReceived= {isKeyWordReceived}' and isOkReceived={isOkReceived}", TAG);
            if (isContentReceived && isOkReceived)
            {
                string conclusionMessage =
                    $"AT Command: {Command}, Verification successfully passed. Expected content and OK #tag received";
                Logger.Write(conclusionMessage, TAG);
                Assert.Pass(conclusionMessage);
            }
            #endregion

        }
        catch (Exception e)
        {
            Logger.Write($"during: sending AT to Camera using camera data channel. Exception has been thrown.", TAG);
            Logger.Write($"Exception message:  {e.Message}", TAG);

        }
        return entireResponse.ToString();
    }// end of verify

}
/*
 instatnt verification backup
       buffer = camera.Read();
                        if (buffer != "")
                        {
                            Logger.Write($"response line: {buffer}", TAG);
                            entireResponse.Append(buffer);
                            #region instatnt verification

                            if (buffer.Contains(expectedTextContent))
                            {
                                Logger.Write($"is excepted content received: {buffer.Contains(expectedTextContent)}", TAG);
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
                                    $"TEstCase: AT Command: {Command} execution FAIL," +
                                    $"due to ERR token has been received: '{buffer}'";
                                Logger.Write(message, TAG);
                                Assert.Fail(message);
                            }



                        }
                        #endregion
 
 */