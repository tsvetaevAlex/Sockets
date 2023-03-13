
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
    public static string fTag = "failure";
    private static SshClient DeviceSsocket = ConnectionPointers.GetDeviceSSH();
    private static SftpClient DeviceSFTP = ConnectionPointers.GEtDeviceSFTP();
    private static ShellStream DeviceStream = ConnectionPointers.GetCameraStream();
    private static string tPrefix = "at simicon.automation.Snapshot.";
    //TODO update to robeject return;


    public static bool RrefreshShapshot()
    {
        string GetTimeQUery = "ls --full-time 1.jpg | awk '{print $7}'";
        int maxTC = Globals.MAXTryCount; //maximum ammount of attempts to download shanpshot;
        Logger.Write("we are in Snapshot.RrefreshShapshot", "TraceRoute");
        
        int i = 0;// current attempt number;
        var initial = SshSocket.GetResponse(GetTimeQUery);
        string current = String.Empty;
        bool isRefreshed = false;
        Logger.Write($"{tPrefix}RrefreshShapshot: existing image.creatino time is: {initial}", _tag);
        Logger.Write($"{tPrefix}RrefreshShapshot: existing image.creatino time is: {initial}", sTAG);

        while (i < maxTC)
        {
            current = SshSocket.GetResponse(GetTimeQUery);
            i++;
            if (current != initial)
            {
                isRefreshed = true;
                Logger.Write($"{tPrefix}RrefreshShapshot:current snapshot time is:{current}, id dnspdhot refreshed: {isRefreshed}", _tag);
                break;
            }
        }
        if (isRefreshed)
        {
            return isRefreshed;
        }
        else
        {
            string ERRmsg = " Exit Code: 503. Run Failure. Ccnnot refresh snapshot from sensor. pleate verify camera and/or service/scpirt 1.sh.";
            Logger.Write(ERRmsg, fTag);
            Assert.Fail(ERRmsg);
            return false;
        }
    }//End of refreshShapshot

    public static void Create()
    {
        if (!RrefreshShapshot())
        {
            Logger.Write($"{tPrefix}Create: RrefreshShapshot Return False",fTag);
            Environment.Exit(503);
        }
        else
        {
            // 1.tp check creation dateTime: ls -l /tftpboot/boot/1.jpg | awk '{print $6$7$8}'
            // 2.-//- ls -l /tftpboot/boot/1.jpg | awk '{print $6} {print $7} {print $8}'
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

        }//end of if (!RrefreshShapshot())

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
        while (attempt++ <= Globals.MAXTryCount)
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

    public static void Get(string localFilename, string ATtag = "",bool iscreated = false)
    {
        if (!iscreated)
        {
            Create();
        }

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
