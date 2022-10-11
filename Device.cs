
using Renci.SshNet;

namespace automation
{
    public static class Device
    {
        public static string DeviceIP;
        public static string Login;
        public static int port = 22;
        public static string password;
        public static SshClient socket;
        public static ShellStream DataChannel;
        //private static readonly SshClient sshCLient;

        public static void init(string host, string loginname, string pswd)
        {
            DeviceIP = host;
            Login = loginname;
            password = pswd;
        }

        public static bool GetConnection()
        {
            #region create connection to device via ssh
            try
            {
                SshClient sshCLient = new SshClient(DeviceIP, Login, password);
                Console.WriteLine(sshCLient.ToString());
                socket = sshCLient;
                socket.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in GetConnection line 26[SshClient sshCLient = new SshClient]: exceptiom{0}",e.Message);
            }
            ShellStream stream = socket.CreateShellStream("", 0, 0, 0, 0, 0);
            DataChannel = stream;
            #endregion 

            #region wait for connection acknowledgment
            while (true)
            {
                Thread.Sleep(750);
                string acknowledgment = stream.ReadLine();
                if (acknowledgment.Contains("Processing /etc/profile... Done"))
                {
                    Console.WriteLine("CONECTION APPROVED");
                    DataChannel = stream;
                    return true;
                }
            }

            #endregion
        }// end of GetConnection



        /// <summary>
        /// send Command to recipient without awating any response
        /// </summary>
        /// <param name="message"></param> - conmmand to send
        public static void SendCommand(string message)
        {
            DataChannel.Write(message);
        }//end of Send Command

        public static void SendMessage(Envelope envelope)
        {

        }

        /*
         * public void sendMesssage(string message);
         * public void sendCommand (string instruciton);
         */

    }
}
