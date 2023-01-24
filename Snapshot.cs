using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using simicon.automation.Utils;

namespace simicon.automation;

public static class Snapshot
{
    private static string _tag = "Snapshots";
    public static bool Create()
    {
        Thread.Sleep(1000);
        Console.WriteLine("------------------------------------------->we are in create snapshot");
        Logger.Write("Let`s check our connections",_tag);
        ConnectionPointers.summarize();
        SftpClient sftp = ConnectionPointers.SftpSocket;
        Logger.Write($"Reuqired SFTP connection pointer is: [{sftp}]",_tag);

        string remoteDirectory = "/tftpboot/boot/";
        ConnectionPointers.SftpSocket.ChangeDirectory(remoteDirectory);
         string CreateSnapshoQuery = "nc 127.0.0.1 4070 -e ./1.sh";
        ConnectionPointers.DeviceStream.WriteLine(CreateSnapshoQuery);
        //SshSocket.Send(CreateSnapshoQuery,"",VerificationType.None, "CreateSnapshot");
        //TODO move return to verification if err in resp return false;
        Thread.Sleep(1000);
        return true;
    }

    public static void Get(string localFilename, string ATtag="")
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
        string imagefilename = $"{LocalOutputFilePath}\\{filename}";
        using (Stream fileStream = File.Create(imagefilename))
        {
            SftpClient sftp = ConnectionPointers.SftpSocket;

            Logger.Write($"Snapshots Folder: {LocalOutputFilePath}", _tag);
            Logger.Write($"snapshot filename  to create: {localFilename}",_tag);

            string remoteDirectory = "/tftpboot/boot/";
            sftp.ChangeDirectory(remoteDirectory);
            sftp.DownloadFile("1.jpg", fileStream);      
        }


        //Console.WriteLine("outputFolder: {0}", LocalOutputFilePath+ localFilename);
        //Stream ouputFile = File.Create(localFilename, );
        //Helper.StringExecute("ls");
        try
        {
            Console.WriteLine("-------------------------------------------DOWNLOAD SNAPSHOT --------------------------------");

            //
            //{
            //}

            //string remoteFileName = "1.jpg";
            //Stream SNAPSHOT  = File.Create(localFilename);
            //sftp.DownloadFile(remoteFileName, SNAPSHOT);
            //sftp.
            Console.WriteLine("------------------------------------------>1.JPG##############################################");

        }
        catch (Exception e)
        {
            Console.WriteLine($"------------------------------------------->download FAILED due-to Exception: {e.Message}");

        }
    }//getSnaphot


}
