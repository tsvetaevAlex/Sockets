using automation;
using Renci.SshNet;
using System.Data;

namespace simicon.automation;


public class Device
{
        public string Host;
        public string Login;
        public string Password;
        //public SshClient SshSocket;
        //public SftpClient SftpSocket;
        //public ShellStream DataChannel;
        public string prefix = ">>>>>>>>>>>>>>>>>>>>>>";
        public ConnectionPointers Connections;
        public Sensorapp _sensor;
        public Camera _camera;
        public Helper _helper;

    public Device(string ip, string lohinname, string pswd)
    {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint Device param constructor");
        Host = ip;
        Login = lohinname;
        Password = pswd;
        Connections = new ConnectionPointers("192.168.10.102", "root", "test");
        Console.WriteLine("-----------------------------[ConnectionPointers Created]---------------------------------");
        _helper = new Helper(Connections);
        Console.WriteLine("-----------------------------[new Helper(Connections) Done]-------------------------------");
        _sensor = new Sensorapp(Connections);
        Console.WriteLine("---------------------------[new sensopapp Connections]------------------------------------");
        _sensor.Prepare();
        Console.WriteLine("-----------------------------[sensopapp Prepare Done]-------------------------------------");
        _camera = new Camera(Connections);
        Console.WriteLine("-----------------------------[new Camera (Connections) Done]------------------------------");
    }
    public Camera GetCamera()
    {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint GetCamera");
        return _camera;
    }
    public ConnectionPointers GetConnectionPointers()
    {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint GetConnectionPointers");

        return Connections;
    }
    }// end o class Device