
using NUnit.Framework.Internal;
using System.Runtime.ConstrainedExecution;

namespace simicon.automation.Gen2;

public static class VerifyCameraSuite
{
    private static readonly string Host = "192.168.10.102";
    private static readonly string Login = "root";
    private static readonly string Password = "test";

    private static string TAG = "CameraTest";
    [Test]
    public static void at01PrepareConnections()
    {
        Logger.InitLogger();
        Logger.Write("has entered into at01PrepareConnections", "TraceRoute");
        ConnectionPointers.InitConnectionPointers(Host, Login, Password);

    } // end of PrepareEnvironment


    [Test]
    public static void at02UpdateSensorappProperty()
    {

        Logger.Write("has entered into PrepareEnvironment.UpdateSensorappProperty", "sensorapp");
        Logger.Write("has entered into PrepareEnvironment.UpdateSensorappProperty", TAG);

        #region Update sensorapp property

        string SensorappUdateRequest =
            "sqlite3 /tftpboot/boot/conf/kris.sql3 \"insert or replace into tblSettings (tName,tValue) values ('SENSORAPP_MSENSORATPORT','')\"";

        string SensorappSelectExpectedContent = "";

        SshSocket.Send(SensorappUdateRequest, "");
        SshSocket.Send(SensorappUdateRequest, "");

        #endregion
    }// end of SensorappUpdateProperty

    [Test]
    public static void at03VerifySensorappProperty()
    {

        Logger.Write("has entered into PrepareEnvironment.VerifySensorappProperty", "sensorapp");
        Logger.Write("has entered into PrepareEnvironment.VerifySensorappProperty", TAG);
        #region Verify value of updated Sensorapp Property

        string SensorappSelectRequest = "sqlite3 /tftpboot/boot/conf/kris.sql3 \"select * from tblSettings\" | grep SENSORAPP_MSENSORATPORT";
        string expectedContent = "SENSORAPP_MSENSORATPORT|";
        SshSocket.Send(SensorappSelectRequest, expectedContent);

        #endregion
    }



    [Test]
    public static void at04prepareSensorappUPDATEProperty()
    {
        Sensorapp.UpdateSensorappProperty();
    }

    [Test]
    public static void at05prepareSensorappVerifyProperty()
    {
        Sensorapp.VerifySensorappProperty();
    }

    [Test]
    public static void at06PrepareSensorappRestartSensorapp()
    {
        Sensorapp.SensorappRestart();
    }

    //public static async void at006VerifyATG()
    //{
    //    Task<bool> result = VerifyATG();
    //    // send atg
    //    Assert.IsTrue(result);
    //}

    //static async Task<bool> VerifyATG()
    //{
    //    string buffer = "";
    //    //read response using cat
    //    bool isContentReceived = false;
    //    bool isOkReceived = false;
    //    StringBuilder sb = new StringBuilder();

    //    return true;
    //}
    [Test]
    public static void at07VerifyATC()
    {
        Logger.Write("has entered into VerifyATC()", "TraceRoute");
        Verify("ATC", "ApCorr: ", "ATC");
    }




    [Test]
    public static void at09VerifyATG()
    {
        Logger.Write("has entered into VerifyATG()", "TraceRoute");
        Verify("ATG", "GAIN: ", "ATG");
    }
    [Test]
    public static void at10VerifyATG0()
    {
        Logger.Write("has entered into VerifyATG(0)", "TraceRoute");
        Verify("ATG=0", "GAIN: 0", "ATG");
    }
    [Test]
    public static void at11VerifyATG6000()
    {
        Logger.Write("has entered into VerifyATG(6000)", "TraceRoute");
        Verify("ATG=6000", "GAIN: 480", "ATG");
    }
    [Test]
    public static void at12VerifyATG()
    {
        Logger.Write("has entered into VerifyATG()", "TraceRoute");
        Verify("ATG", "GAIN: 480", "ATG");
    }
    [Test]
    public static void at13VerifyATF()

    {
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        Verify("ATF", "Cam flip: ", "ATF");
    }
    [Test]
    public static void at14VerifyATJ()

    {
        Logger.Write("has entered into VerifyATJ()", "TraceRoute");
        Verify("ATJ", "Offset: ", "ATJ");
    }

    [Test]
    public static void at15VerifyATK()

    {
        Logger.Write("has entered into VerifyATK)", "TraceRoute");
        Verify("ATK", "Hpoint: ", "ATK");
    }

    [Test]
    public static void at16VerifyATL()

    {
        Logger.Write("has entered into VerifyATL()", "TraceRoute");
        Verify("ATL", "Max exp: ", "ATL");
    }

    [Test]
    public static void at17VerifyATP()

    {
        Logger.Write("has entered into VerifyATP()", "TraceRoute");
        Verify("ATP", "Cur exp: ", "ATP");
    }

    [Test]
    public static void at18VerifyATS()

    {
        Logger.Write("has entered into VerifyATS()", "TraceRoute");
        Verify("ATS", "SHUT: ", "ATS");
    }



    public static void Verify(string input, string Expected,string _TAG)
    {
        Logger.Write("has entered into Verify()", "TraceRoute");

    TAG = _TAG;
    string Command = input;
    string ExpectedKeyWord = Expected;

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

            StringBuilder sb = new StringBuilder();
            ;
            string buffer = "";
            bool isKeyWordReceived = false;
            Logger.Write($"connection VerifyCameraSuite. camera stream pointer:  '{camera}'", TAG);
//WHILE

        while (true)
        {
                //at2 bw sensor  response =="Color disabled!"
                //COlor sensor == OK(
                try
                {
                    if (camera.DataAvailable)
                    {
                        buffer = camera.Read();
                        if (buffer != "")
                        {
                            Logger.Write($"response line: {buffer}", TAG);

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
                            Logger.Write($"Conclusionbased on Flags:\\r\\n isKeyWordReceived= {isKeyWordReceived}' and isOkReceived={isOkReceived}",TAG);
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

} //end of Class
