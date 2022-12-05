using Renci.SshNet;

namespace simicon.automation;

public static class ConnectionPointers
    {
    public static string Host;
    public static string Login;
    public static string Password;
    public static SshClient SshSocket;
    public static SshClient CameraSocket;
    public static ShellStream Picocom;
    public static SftpClient SftpSocket;
    public static ShellStream SshDataChannel;
    public static string prefix = ">>>>>>>>>>>>>>>>>>>>>>";

        public static void InitConnectionPointers(string host, string loginname, string pswd)
        {
            Host = host;
            Login = loginname;
            Password = pswd;
        Console.WriteLine("\n<----------------------------[ InitSshConnection->Device ]-------------------------------->");
            SshSocket = InitSshConnection(host, loginname, pswd);
            SftpSocket = InitSftpConnection(host, loginname, pswd);
        Console.WriteLine("\n<----------------------------[ InitSshConnection->Device.Camera ]------------------------->");
        CameraSocket = InitSshConnection(host, loginname, pswd);
        ShellStream Picocom = CameraSocket.CreateShellStream("", 0, 0, 0, 0, 0);
        Console.WriteLine("\n<----------------------------[ Device.Camera Connection]----------------------------------->");
        Camera.Conect();
        //Envelope dataPack = new Envelope
        //(
        //    testname: "Verify Landing page on Connection",
        //    request: "pwd",
        //    expectedContent: "/root",
        //    vt: VerificationType.Equal
        //);
            Console.WriteLine("{0} SSH Client: {1};", prefix, SftpSocket.ToString());

    }


    private static SshClient InitSshConnection(string ip, string lohinname, string pswd)
        {
        #region create SSH connection with device
            try
            {
            Logger.Write("Device InitSshConnection", "InitConnectionPointers");
            SshClient sshCLient = new SshClient(ip, lohinname, pswd);
            Console.WriteLine("{0} SSH Client: {1};", prefix, sshCLient.ToString());
            ConnectionPointers.SshSocket = sshCLient;

            }
            catch (Exception e)
            {
                Console.WriteLine(prefix + "Exception in GetConnection line 26[SshClient sshCLient = new SshClient]: exceptiom: {0}", e.Message);
            }
            SshSocket.Connect();
            ShellStream stream = SshSocket.CreateShellStream("", 0, 0, 0, 0, 0);
            ConnectionPointers.SshDataChannel = stream;

            #region wait for connection acknowledgment
                while (true)
                {
                    Thread.Sleep(750);
                    string acknowledgment = stream.ReadLine();

                    if (acknowledgment.Contains("Processing /etc/profile... Done"))
                    {

                        Console.Write($"resp: '{acknowledgment}'");
                        Console.WriteLine("\n" + prefix + "!!!SSH CONNECTION APPROVED!!!");
                        break;
                    }
                }

            #endregion
        #endregion
        return SshSocket;
        }// End of initConnection



    private static SftpClient InitSftpConnection(string ip, string lohinname, string pswd)
    {
        #region create SCP connection with Device
        Logger.Write("Device InitSftpConnection", "InitConnectionPointers");
        SftpClient sftp = new SftpClient(ip, lohinname, pswd);
        SftpSocket = sftp;
        ConnectionPointers.SftpSocket.Connect();
        #endregion
        return sftp;
    }
}// end o class ConnectionPointers