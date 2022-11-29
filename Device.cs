using automation;
using Renci.SshNet;
using System.Data;

namespace simicon.automation;


public class Device
    {
        public static string Host;
        public static string Login;
        public static string Password;

        public static void InitDevice(string ip, string lohinname, string pswd)
        {
            Host = ip;
            Login = lohinname;
            Password = pswd;
        }
    }// end o class Device