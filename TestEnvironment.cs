using CameraTests;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace automation;
public static class TestEnvironment
{
    public static string DeviceIP = "192.168.10.102";
    public static Device device = new Device(DeviceIP);
    public static SshClient socket;
    public static ShellStream DataChannel;
    public TestEnvironment()
    {
        DeviceIP = "192.168.10.102";
        device.deatination.host = DeviceIP;
        device.deatination.login = "root";
        device.deatination.password = "test";

    }
}
    

    /*public static readonly Destination destination = new Destination
        (
            Host: DeviceIP,
            loginname: "root",
            pswd: "test"
        );
    */

