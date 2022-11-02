using Renci.SshNet;
using simicon.automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Device = simicon.automation.Device;

namespace automation
{
    public class Camera
    {
        private readonly Device _device;
        private readonly Helper _helper;
        public ConnectionPointers _connectionPointers;
        public Camera(ConnectionPointers cp)
        {
            _helper = new Helper(cp);
            _connectionPointers = cp;
        }
        public Camera(Device device)
        {
            _device = device;
            _helper = new Helper(device.Connections);
            _connectionPointers = device.Connections;
        }

        public void Conect()
        {

            Console.WriteLine("!!!!!!!!!!!!!!Checkpoint Camera.Conect");

            Envelope message = new Envelope
            (
                testname: "CamTest_picocom_vonnection",
                request: "picocom -b 115200 /dev/tts/camera",
                expectedContent: "Terminal ready",
                vt: VerificationType.Contains
            );

            _helper.Execute(message);
        }



        public void CreateSnapshot()
        {

            string GetSnapshoQuery = "/tftpboot/boot/1.sh";
            _device.Connections.SshSocket.CreateCommand(GetSnapshoQuery).Execute();
        }

        public void GetSnapshot(string localFilename)
        {
            SftpClient sftp = _device.Connections.SftpSocket;
            string remoteDirectory = "/tftpboot/boot/";

            sftp.Connect();
            sftp.ChangeDirectory(remoteDirectory);
            var outputFilePath = $"c:\\SnapShots";
            Console.WriteLine("outputFolder: {0}", outputFilePath);
            Stream ouputFile = File.Create(localFilename);
            string remoteFileName = "1.jpg";
            sftp.DownloadFile(remoteFileName, ouputFile);

        }
        //getSnaphot
        //create SNapshot


    }
}
