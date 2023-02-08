
namespace simicon.automation;

public static class Snapshot
{
    public static SftpClient? actualSftp = null;
    public static string remoteDirectory = "/tftpboot/boot/";
    public static string host = ConnectionPointers.Host;
    public static string user = ConnectionPointers.Login;
    public static string pswd = ConnectionPointers.Password;
    private static string _tag = "Snapshots";
    private static string sTAG = "stability";
    private static SshClient DeviceSsocket = ConnectionPointers.GetDeviceSSH();
    private static SftpClient DeviceSFTP = ConnectionPointers.GEtDeviceSFTP();
    private static ShellStream DeviceStream = ConnectionPointers.GetCameraStream();
    private static string tPrefix = "at simicon.automation.Snapshot.";
    //TODO update to robeject return;
    private static object ChooseSFTPConnection()
    {
        Logger.Write("=====================>Verivication of [ConnectionPointers._DeviceSFTP]", _tag);

        Logger.Write("we are in Snapshot.ChooseSFTPConnection", "TraceRoute");
        if ((DeviceSFTP != null) && (DeviceSFTP.IsConnected))
        {
            Logger.Write("I am (Automation Bot (my name is rusty iron mind, I suggest initially to try pointer object: static [pointer _DeviceSFTP a few momenta go it was not null and beeing connected ))", _tag);
            actualSftp = DeviceSFTP;
            return actualSftp;
        }
        else if ((Globals.DeviceSFTP != null) && (Globals.DeviceSFTP.IsConnected))
        {
            Logger.Write("I'm Friend of rusty iron mind, he missed the choice of executor. I propose tou to ask: Globals._DeviceSFTP", _tag);
            actualSftp = Globals.DeviceSFTP;
            return actualSftp;
        }

        Logger.Write("our existing connections are not able to work due to personal reason nneed  to create a new one:", _tag);

        actualSftp = new SftpClient(host, user, pswd);
        actualSftp.Connect();
        if ((actualSftp != null) && (actualSftp.IsConnected))
        {
            return actualSftp;
        }
        return null;
    }


    public static void Create()
    {
        // 1.tp check creation dateTime: ls -l /tftpboot/boot/1.jpg | awk '{print $6$7$8}'
        // 2.-//- ls -l /tftpboot/boot/1.jpg | awk '{print $6} {print $7} {print $8}'
        //SftpClient _Sftp = (SftpClient)ChooseSFTPConnection();
        SftpClient actualSftp = new SftpClient(host, user, pswd);
        Logger.Write("onnectionPointers._DeviceSFTP is not null", _tag);
        Logger.Write($"Snapshot.Create.actualSftp: ->{actualSftp}<-", sTAG);
        DeviceSFTP = actualSftp;
        Thread.Sleep(1000);

        if (DeviceSFTP != null)
        {
            Logger.Write("onnectionPointers._DeviceSFTP is not null", _tag);
            Logger.Write($"onnectionPointers._DeviceSFTP is connected: {DeviceSFTP.IsConnected}", _tag);
            Logger.Write($"Check ConnectionPointers._DeviceSFTP== {DeviceSFTP}; is it connected?:{DeviceSFTP.IsConnected}", _tag);

            if (DeviceSFTP.IsConnected)
            {
                Logger.Write("INFO: ConnectionPointers._DeviceSFTP is not null and Connected!!!", _tag);
                Logger.Write("INFO: suggested it as valid connectionObject", _tag);
            }

        }
        Console.WriteLine($"------------------------------------------->we are in create snapshot, actualSftp connetion:[{DeviceSFTP}]");
        if (DeviceSFTP != null)
        {
            //SuggestedConnection.isNull = false;
            try
            {

                if (DeviceSFTP.IsConnected)
                {
                    //SuggestedConnection.isConnected = true;
                    //  SuggestedConnection.Connection = ConnectionPointers._DeviceSFTP;
                    //ConnectionPointers._DeviceSFTP.ChangeDirectory(Globals.remoteDirectory);
                }

            }
            catch (Exception e)
            {
                Logger.Write($"ConnectionPointers.SftpSocket is null in SnapShot.Create()in Attemp to use it exception was thrown:{e.ToString}", _tag);
            }
            Logger.Write($"{tPrefix}: ConnectionPointers.SftpSocket is null in SnapShot.Create()", _tag);
            Logger.Write($"{tPrefix}: Attempt to connect using saved Global pointer intead of local.", _tag);
            if (Globals.DeviceSFTP != null)
            {
                try
                {
                    Globals.DeviceSFTP.ChangeDirectory(Globals.remoteDirectory);

                }
                catch (Exception ex)
                {
                    Logger.Write($"{tPrefix} :Saved Global actualSftp pointer is also unsuitable", _tag);
                    Assert.Fail($"{tPrefix} : unavailable to continue automation tests execution due to  SFTP connectionObject to Device not established; {ex.ToString}");
                }


            }

        }
        /*NOTES:
        grt shanshot creatinon DateTime stamp: ls -l .1.jpg | awk '{print $6$7$8}'
        run dcript to refresh shapshot: nc 127.0.0.1 4070 -e ./1.sh
        // ls -l /tftpboot/boot/1.jpg | awk '{print $6} {print $7} {print $8}'
        remote workinfg directory: /tftpboot/boot/
        //
        <[root@MDXXXX boot]# ls -full-time 1.jpg | awk '{print $7}'
        >21:18:12
         */
        string remoteDirectory = "/tftpboot/boot/";
        string BornDate = "ls -full-time 1.jpg | awk '{print $7}'";
        string GetCReationDateTimeWQUery = "ls -l /tftpboot/boot/1.jpg | awk '{print $6$7$8}'";
        actualSftp.ChangeDirectory(remoteDirectory);
        string bornDateExistingFile = SshSocket.GetResponse(GetCReationDateTimeWQUery);
        Logger.Write($"{tPrefix}: Creation Date Time of existing snapshot image is: {bornDateExistingFile}",_tag);
        string CreateSnapshoQuery = "nc 127.0.0.1 4070 -e ./1.sh";
        DeviceStream.WriteLine(CreateSnapshoQuery);
        string bornDateRefreshedFile = SshSocket.GetResponse(GetCReationDateTimeWQUery);
        Thread.Sleep(1000);
        int attempt = 1;
        bool isOK = false;
        while (attempt++ <= Globals.TryCount)
        {
            if (bornDateExistingFile != bornDateRefreshedFile)
            {
                Logger.Write($"{tPrefix}: dateTime of shanshot before 1.sh: {bornDateExistingFile}", _tag);
                Logger.Write($"{tPrefix}: dateTime of refreshed shanshot after 1.sh: {bornDateRefreshedFile}", _tag);
                Logger.Write($"{tPrefix}: snapshoy file refreshed.", _tag);
                isOK = true;
                break;
            }
            else
            {
                attempt++;
            }
            if (!isOK)
            {

            }

        }

    }// end of create()

    public static void Get(string localFilename, string ATtag = "")
    {
        Create();

        string imageFolder = Logger._LogFolder;
        var LocalOutputFilePath = $"{imageFolder}\\SnapShots\\{ATtag}";
        var dirInfo = new DirectoryInfo(LocalOutputFilePath);
        if (!dirInfo.Exists)
        {
            Directory.CreateDirectory(LocalOutputFilePath);
        }

        string filename = localFilename.Trim(' ').Trim('\n').Trim('#').Trim('$');
        string imagefilename = $"{LocalOutputFilePath}\\{filename}.jpg";
        string remoteFlle = "/tftpboot/boot/1.jpg";
        DateTime creation = File.GetCreationTime(remoteFlle);
        Logger.Write($"{tPrefix}: Snapshot crection time: {creation.ToString("dd/mm/yyyy: HH:mm:ffff")}", _tag);
        using (Stream fileStream = File.Create(imagefilename)) 
        {
            SftpClient sftp = DeviceSFTP;
            sftp.ChangeDirectory(remoteDirectory);
            sftp.DownloadFile("1.jpg", fileStream);
            Logger.Write($"Snapshots Folder: {LocalOutputFilePath}", _tag);
            Logger.Write($"snapshot filename  to create: {localFilename}", _tag);

            Logger.Write($"Snapshot crection time: {creation.ToString("dd/mm/yyyy: HH:mm:ffff")}",_tag);
        }

        //Console.WriteLine("outputFolder: {0}", LocalOutputFilePath+ localFilename);
        //Stream ouputFile = File.Create(localFilename, );
        //Helper.StringExecute("ls");
        //try
        //{
        //    Console.WriteLine("-------------------------------------------DOWNLOAD SNAPSHOT --------------------------------");

        //    //
        //    //{
        //    //}
        //    string remoteFileName = "1.jpg";
        //    Stream SNAPSHOT = File.Create(localFilename);
        //    SftpClient sftp = (SftpClient)ChooseSFTPConnection();
        //    if ((sftp != null) && (sftp.IsConnected))
        //    {
        //        Logger.Write($"Download snapshot through connection:[{sftp}]; target local filename: {localFilename}", _tag);
        //        Console.WriteLine("------------------------------------------>1.JPG##############################################");
        //    }

        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine($"------------------------------------------->download FAILED due-to Exception: {e.Message}");

        //}
    }//getSnaphot


}
