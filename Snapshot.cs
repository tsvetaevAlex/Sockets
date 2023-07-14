using Renci.SshNet;
using simicon.automation.Tests;

namespace simicon.automation;

public class Snapshot : TestRun
{
    public SftpClient? actualSftp = null;
    private string remoteDirectory = "/tftpboot/boot/";
    public string LocalSnapshotPath = "";
    private string remoteImage = "/tftpboot/boot/1.jpg";
    private readonly string tPrefix = "at simicon.automation.Snapshot. code line: ";
    //public string host = GetHost();
    //public string user = GetLogin();
    //public string pswd = GetPassword();
    //TODO update to robeject return;

    public Snapshot()
    {

        DataHeap dataHeap = new DataHeap();

        LocalSnapshotPath = Path.Combine(log._LogFolder, "Snapshots");
        snapshotsLocalFolder = LocalSnapshotPath;
        var dirInfo = new DirectoryInfo(LocalSnapshotPath);
        if (!dirInfo.Exists)
        {
            Directory.CreateDirectory(LocalSnapshotPath);
            dataHeap.SnapshotsFolder = snapshotsLocalFolder;
            dataHeap.ReportFolder = snapshotsLocalFolder;
        }
    }
        public void Init()
    {
        var dirInfo = new DirectoryInfo(LocalSnapshotPath);
        if (!dirInfo.Exists)
        {
            log.WhereLogs($"Where Logs; log Log Folder: {log._LogFolder}");
            log.Info($"Where Snapshots; log snapshots Folder: {LocalSnapshotPath}");
            Directory.CreateDirectory(LocalSnapshotPath);
        }
    }
        public void Get(string localFilename)
    {
        string sftpHost = GetHost();
        string sftpLofin = GetLogin();
        string sftpPassword = GetPassword();
        int i = 1;// counter;
        int limit = 6; //mam attempts QTY// Because of i dtarts from 1. summary it will take 5 attempts мin accordance with agreement 
        #region Attempt to create sftp connection to work with snapshots with 5 try count

        SftpClient SnapSftp = SftpClient;
        if (SshClient is not null)
        {
            log.Info($"{tPrefix}80; Valid sftp client exists poiner is: {SshClient}.");
            SnapSftp = SftpClient;
        }
        else
        {
            log.Networtk($"{tPrefix}85; Valid sftp client to work with snapshots is not found. ");
            log.Networtk($"{tPrefix}86; goes  creating of  new sftp connection");
        while (i < limit)
        {
                log.Networtk($"{tPrefix}89; {i} of 5, Attempt to create new connection");
            try
            {
                SnapSftp = new SftpClient(sftpHost, sftpLofin, sftpPassword);
            }
            catch (Exception e)
                {
                    log.Exception($"{tPrefix}100; {i} of 5, Attempt to create new connection FAILED due-to Exception {e.ToString}");
                    i++;
                    if (i < limit)
                    continue;
                    else
                    {
                            string failureMessage = "test run failed due-to unable to create sftp connection to work with snapshots. Maximum number of attempts to connect has bee taken.";
                        log.Failure(failureMessage);
                        Assert.Fail(failureMessage);
                    }
            }
            i++;
        }//end of while
        }
        #endregion


        if (SnapSftp is not null)
        {
            if (SnapSftp.IsConnected == false)
            {
                SnapSftp.Connect();
                //SilentRrefreshSnapshot();
                log.Info($" {tPrefix}119; Sftp Connetion to work with dnapshota created. Pointer is: {SnapSftp};Is Connection alive: {SnapSftp.IsConnected}");
            }
        }

        // refgresh snapshot
        Response.Wipe();
        var ssh = connections.GetDeviceSSH();
        remoteConsole.GetResponseSSH("cd /tftpboot/boot &&  nc 127.0.0.1 4070 -e ./1.sh");
        if (Response.output == "Done.")
        {
            log.SnapShot("snapshot refrershed by spcript 1.sh. response:'Done.' received");
        }
        string LocalSnapshotPath = Path.Combine(log._LogFolder, "Snapshots");
        snapshotsLocalFolder = LocalSnapshotPath;
        var dirInfo = new DirectoryInfo(LocalSnapshotPath);
        if (!dirInfo.Exists)
        {
            log.WhereLogs($"log Log Folder: {log._LogFolder}");
            log.WhereLogs($"snapshots Folder: {LocalSnapshotPath}");
            Directory.CreateDirectory(LocalSnapshotPath);
        }

        SftpClient ConPointSFTP = connections.GEtDeviceSFTP(); //sftp from ConnectionPointers ;
        string localImageFilename = $"{LocalSnapshotPath}\\{localFilename}.jpg";


        SftpClient sftp = null; 
        if (ConPointSFTP is not null)
        {
            sftp = ConPointSFTP;
            sftp.ChangeDirectory(remoteDirectory);
        }
        //localImageFilename 

            if (sftp is not null)
            {
            //cd /tftpboot/boot &&  nc 127.0.0.1 4070 -e ./1.sh
            Response.Wipe();
                remoteConsole.GetResponseSSH("cd /tftpboot/boot &&  nc 127.0.0.1 4070 -e ./1.sh");
                if (Response.output != "")
                    log.SnapShot($"{tPrefix}93, call to 1.dh has response: {Response.output}");
                if (Response.output == "Done.")
                    log.SnapShot($"{tPrefix}95 Snapshot refreshed.");

                using (Stream fileStream = System.IO.File.Create(localImageFilename))
                {
                    sftp.DownloadFile(remoteImage, fileStream);
                    log.Info($"Snapshots Folder: {LocalSnapshotPath}");
                    log.SnapShot($"snapshot filename  to create: {localFilename}");

                }
            }
        }// end of Get()
    }//end of calss Snapshot

