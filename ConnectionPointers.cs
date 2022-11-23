using Renci.SshNet;

namespace simicon.automation;

public static class ConnectionPointers
    {
    public static string Host;
    public static string Login;
    public static string Password;
    public static SshClient SshSocket;
    public static SshClient CameraSocket;
    public static SftpClient SftpSocket;
    public static ShellStream SshDataChannel;
    public static ShellStream CameraDataChannel;
    public static string logPrefix = DateTime.Now.ToString("yyyy-MM-dd hh:mm");

    public static void InitConnectionPointers(string host, string loginname, string pswd)
        {
            Host = host;
            Login = loginname;
            Password = pswd;
        Console.WriteLine($"\n{logPrefix}<----------------------------[ InitSshConnection->DEvice ]-------------------------------->");
            SshSocket = InitSshConnection(host, loginname, pswd);
        Console.WriteLine($"{logPrefix}: DEvice SshSocket: {SshSocket}");
            SftpSocket = InitSftpConnection(host, loginname, pswd);
        Console.WriteLine($"{logPrefix}: DEvice SftpSocket: {SftpSocket}");
        Console.WriteLine("\n<----------------------------[ InitSshConnection->DEvice.Camera ]------------------------->");
        CameraSocket = InitSshConnection(host, loginname, pswd);
        Console.WriteLine("\n<----------------------------[ VerificationList.InitVerificationCriteria ]------------------------->");
        Console.WriteLine($"\n{logPrefix}: <----------------------------[ DEvice.Camera Connetion]----------------------------------->");
        Console.WriteLine($"Camera Socket: {CameraSocket}");
        Console.WriteLine($"\n{logPrefix}: <----------------------------[ VerificationList InitVerificationCriteria ]----------------------------------->");
        VerificationList.InitVerificationCriteria();
        Camera.Conect();      
        //Envelope dataPack = new Envelope
        //(
        //    testname: "Verify Landing page on Connection",
        //    request: "pwd",
        //    expectedContent: "/root",
        //    vt: VerificationType.Equal
        //);
            Console.WriteLine("{0} SSH Client: {1};", logPrefix, SftpSocket.ToString());

    }


    private static SshClient InitSshConnection(string ip, string lohinname, string pswd)
        {
    #region create SSH connection with device
            try
            {
                SshClient sshCLient = new SshClient(ip, lohinname, pswd);
                Console.WriteLine("{0} SSH Client: {1};", logPrefix, sshCLient.ToString());
                ConnectionPointers.SshSocket = sshCLient;



        }
        catch (Exception e)
            {
                Console.WriteLine(logPrefix + "Exception in GetConnection line 26[SshClient sshCLient = new SshClient]: exceptiom{0}", e.Message);
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
                Console.WriteLine("\n" + logPrefix + "!!!SSH CONECTION APPROVED!!!");
                    break;
                }
            }

        #endregion
        #endregion
        return SshSocket;
        }// End of initConnection

    public static SshClient InitSshConnection()
    {

        #region create SSH connection with device
        try
        {
            string ip = Host;
            string lohinname = Login;
            string pswd = Password;
            
            SshClient sshCLient = new SshClient(ip, lohinname, pswd);
            Console.WriteLine("{0} SSH Client: {1};", logPrefix, sshCLient.ToString());
            ConnectionPointers.SshSocket = sshCLient;
        }
        catch (Exception e)
        {
            Console.WriteLine(logPrefix + "Exception in GetConnection line 26[SshClient sshCLient = new SshClient]: exceptiom{0}", e.Message);
        }
        SshSocket.Connect();
        ShellStream stream = SshSocket.CreateShellStream("", 0, 0, 0, 0, 0);
        ConnectionPointers.SshDataChannel = stream;

        #region wait for connection acknowledgment
        while (true)
        {
            Thread.Sleep(750);
            string acknowledgment = stream.ReadLine();

            if (acknowledgment.Contains($"{logPrefix}: Processing / etc/profile... Done"))
            {

                Console.Write($"{logPrefix}: resp: '{acknowledgment}'");
                Console.WriteLine("\n" + logPrefix + "!!!SSH CONECTION APPROVED!!!");
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
        SftpClient sftp = new SftpClient(ip, lohinname, pswd);
        SftpSocket = sftp;
        ConnectionPointers.SftpSocket.Connect();
        #endregion
        return sftp;
    }
}// end o class ConnectionPointers