using automation;

namespace simicon.automation;
public class TestSuite
{
    public TestSuite() { }

    protected string Host = "192.168.10.102";
    protected string Login = "root";
    protected string Passwword = "test";
    protected string prefix = ">>>>>>>>>>>>>>>>>>>>>>";
    protected Helper _helper;
    protected Camera _camera;
    protected Device _device;



    [Test, Order(1)]
    public void PrepareEvironment()
    {
        Console.WriteLine("\n<============================[ PrepareEvironment Srarted ]============================");
        _device = new Device(
            ip: "192.168.10.102",
            lohinname: "root",
            pswd: "test");
        _helper = _device._helper;
    }


    public Helper GetHelper()
    {
        return _helper;
    }


    public Camera GetCamera()
    {
        return _camera;
    }


    public Device GetDevice()
    {
        return _device;
    }

    /*
    [Test, Order(2)]
    public void ct001_GetDefaultSnapshot()
    {
        _camera = _device.GetCamera();
        _camera.Conect();
        _camera.CreateSnapshot();
        _camera.GetSnapshot("default.jpg");
    }
    */
}//end of class TestSuite