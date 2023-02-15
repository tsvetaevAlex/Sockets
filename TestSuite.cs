
namespace simicon.automation;
public static class TestSuite
{
    public static string Host = "192.168.10.102";
    public static string Login = "root";
    public static string Passwword = "test";

    [Test, Order(1)]
    public static void PrepareEnvironment()
    {
        Console.WriteLine("\n<============================[ PrepareEvironment Srarted ]============================");
        ConnectionPointers.InitConnectionPointers("192.168.10.102","root","test");
        Sensorapp.Prepare();

        #region Camera Test. placed here during debbug period. will be extrscted to [Test, Order(2)]
        //CameraTest.atjTestWithInRange();
        #endregion
    }

    [Test, Order(2)]
    public static void at002ATSF_TestWithInRange([Range(-5300, 5300, 100)] int number)
    {
        //System.Threading.Thread.Sleep(500);
        string ATcomand = "ATJ=" + number;
            //Helper.StringExecute(ATcomand, ConnectionPointers.CameraSocket);
        Console.WriteLine($"Command to RUn: {ATcomand}.");
            if (Camera.CreateSnapshot())
                Camera.GetSnapshot("C:\\SnapShots\\" + ATcomand + ".jpg");

    }
}//end of class TestSuite