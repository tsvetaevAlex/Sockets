using simicon.automation.Tests.AT;

namespace simicon.automation.camera.Tests;

public static class VerifyCameraSuite
{
[Test, Order(1)]
    public static void PrepareEnvironment()
    {
        Logger.InitLogfilename();
        Logger.Write(("\n<============================[ PrepareEnvironment Stared ]============================"),
            "PrepareEnvironment");
        ConnectionPointers.InitConnectionPointers("192.168.10.102", "root", "test");
        Sensorapp.Prepare();
        Logger.InitLogfilename();
    }

    [Test, Order(2)]
    public static void VerifyATG()
    {
    ATG.verifyATG();
    }

[Test, Order(3)]
    public static void VerifyATB()
    {
        ATB.verifyATB();
    }

[Test, Order(4)]
        public static void VerifyATJ()
        {
            ATJ.verifyATJ();
        }

}

