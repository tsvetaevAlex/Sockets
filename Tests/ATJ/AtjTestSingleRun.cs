using simicon.automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace automation.Tests.ATJ
{
    public static class AtjTestSingleRun
    {
        public static void Run(int value)
        {
            string ATcomand = "ATJ=" + value;
            Helper.StringExecute(ATcomand, ConnectionPointers.CameraSocket);
            Console.WriteLine($"at protocol command to send: {ATcomand}.");
            if (Camera.CreateSnapshot())
                Camera.GetSnapshot("C:\\SnapShots\\ATJ\\" + ATcomand + ".jpg");
        }
    }
}
