using NUnit.Framework;
using Renci.SshNet;


namespace CameraTests;

public class TestSuiteHelper
{
    
    protected Device testDevicConnectionPoint = new Device();

    [SetUp]
    public void initialization()
    {
        Console.WriteLine("We are here ->TestSuiteHelper.initialization ");
        Destination cameraLocation = new Destination("192.168.10.31", 22, "root", "test");
        /// thi code shoul be invoked before each test yere we will connect to device
        SshClient testDevicConnectionPoint = new Device(cameraLocation).GetConnection(cameraLocation);
        //SshClient testSuiteSocket = testDevice.GetConnection(cameraLocation);

    }

    [TearDown]
    public void finalization()
    {
        Console.WriteLine("We are here ->TestSuiteHelper.finalization ");

    }

    protected Device targetDevice = new Device();
    protected VerificationReport report = new VerificationReport();
    public void RunTests()
    {
        var ct0001test = new ct001ZoomTest();
        ct0001test.Run(250);

        var ct0002test = new ct001ZoomTest();
        ct0001test.Run(2500);

    }


}
