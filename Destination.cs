using Microsoft.VisualBasic;
using System;
using System.Text;
using Renci.SshNet;

namespace CameraTests;

     public class Destination
    {
        public string host;
        public string login;
        public readonly int port = 22;
        public string password;
        public SshClient socket;
        public ShellStream DataChannel;

    public Destination(){ }
    public Destination(string IP)
    {
        host = IP;
    }
    public Destination(string Host, string loginname, string pswd)
    {
        host = Host;
        login = loginname;
        password = pswd;
    }
}//class destination
