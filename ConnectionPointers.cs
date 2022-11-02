using Renci.SshNet;
using System.Runtime.CompilerServices;
namespace simicon.automation;

public class ConnectionPointers
    {
    public string Host;
    public string Login;
    public string Password;
    public SshClient SshSocket;
    public SftpClient SftpSocket;
    public ShellStream SshDataChannel;
    public string prefix = ">>>>>>>>>>>>>>>>>>>>>>";

    public ConnectionPointers() { }
        public ConnectionPointers(string host, string loginname, string pswd)
        {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint Connectionpointers param onstrcutors");

                Host = host;
                Login = loginname;
                Password = pswd;
                SshSocket = InitSshConnection(host, loginname, pswd);
                SftpSocket = InitSftpConnection(host, loginname, pswd);
        }

    public ConnectionPointers GetConnectionPointers()
    {
        return this;
    }


        private SshClient InitSshConnection(string ip, string lohinname, string pswd)
        {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint ConnectionPointers.InitSshConnection");

        #region create SSH connection with device
        //[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",

        try
        {
                SshClient sshCLient = new SshClient(ip, lohinname, pswd);
                Console.WriteLine("{0} SSH Client: {1};", prefix, sshCLient.ToString());
                this.SshSocket = sshCLient;

            }
            catch (Exception e)
            {
                Console.WriteLine(prefix + "Exception in GetConnection line 26[SshClient sshCLient = new SshClient]: exceptiom{0}", e.Message);
            }
        SshSocket.Connect();
        ShellStream stream = SshSocket.CreateShellStream("", 0, 0, 0, 0, 0);
        this.SshDataChannel = stream;

        #region wait for connection acknowledgment
            while (true)
            {
                Thread.Sleep(750);
                string acknowledgment = stream.ReadLine();

                if (acknowledgment.Contains("Processing /etc/profile... Done"))
                {

                Console.Write($"resp: '{acknowledgment}'");
                Console.WriteLine("\n" + prefix + "!!!SSH CONECTION APPROVED!!!");
                    break;
                }
            }
        #endregion
        #endregion
        return SshSocket;
    }// End of initConnection



    private SftpClient InitSftpConnection(string ip, string lohinname, string pswd)
    {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint ConnectionPointers.InitSftpConnection");

        #region create SCP connection with Device
        SftpClient sftp = new SftpClient(ip, lohinname, pswd);
        SftpSocket = sftp;
        SftpSocket.Connect();
        #endregion
        return sftp;
    }
}// end o class ConnectionPointers