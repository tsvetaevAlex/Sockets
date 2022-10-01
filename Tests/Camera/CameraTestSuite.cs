using Renci.SshNet;

namespace CameraTests;

public class CameraTestSuite : TestSuiteHelper
{

    public static Destination TargetDeviceData;
    public Device GeneralTargetDevice;
    protected int CameraPort = 4030;
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //string request = "echo -e 'PROTOCOL 1 \\nGET MODEM INFO' | nc 0 4099"; // reaponse 200
        //string request = "GET MODEM INFO";//response: GET: command not found

        static Destination DeviceLocation()
        {
            return new Destination(
                            ip: "192.168.10.31",
                            Port: 22,
                            loginname: "root",
                            pswd: "test"
                            );
        }//end of DeviceLocation


    }//end main

    private static void RunTests1()
    {
        var test = new ct001ZoomTest();
        test.Run(250);
        test.Run(2500);
    }//TestSuite.RunTests();
}// end of class TestSuite
    
