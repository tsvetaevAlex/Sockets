using Microsoft.VisualBasic;
using System;
using System.Text;
using Renci.SshNet;

namespace CameraTests;

    public class Destination
    {
        public string IP = String.Empty;
        public readonly Int32 port;
        public readonly string login;
        public readonly string password;
        public SshClient SocketPointer;

        public Destination(string ip, int Port, string loginname, string pswd)
        {
            IP = ip;
            port = Port;
            login = loginname;
            password = pswd;
        }

        public void SetSocketPointer(SshClient socket)
        {
            SocketPointer = socket;
        }
    }//class destination
