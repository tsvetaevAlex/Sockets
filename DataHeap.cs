using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation.Tests
{
    public class DataHeap
    {
        private bool onRun = false;
        public string remoteDirectory = "/tftpboot/boot/";
        public string authorizationApproval = "Processing /etc/profile... Done";
        public string NewLine = Environment.NewLine;
        public string LogsFolder = string.Empty;
        public string SnapshotsFolder = string.Empty;
        public string ReportFolder = string.Empty;
        public string launchDateTime = DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
        public string firmwareVersion = string.Empty;
        public string CameraType = string.Empty;

        public DataHeap()
        {
            if ((launchDateTime == "") && (onRun == false) )
            {

                launchDateTime = DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
                onRun = true;
                Console.WriteLine($"Get launchDateTime: {launchDateTime}");
            }
        }


}
}
