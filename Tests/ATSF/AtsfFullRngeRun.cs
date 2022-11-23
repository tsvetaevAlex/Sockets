using simicon.automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automation.Tests.ATSF
{
    public static class AtsfFullRngeRun
    {
        
//        [Test]
        public static void asfTesFullRange([Range(-200,300 , 10)] int number)
        {

            string ATcomand = "AT$ SF=" + number;
            Helper.StringExecute(ATcomand, ConnectionPointers.CameraSocket);
            Console.WriteLine($"at protocol command to send: {ATcomand}.");
            if (Camera.CreateSnapshot())
            {
                Camera.GetSnapshot("C:\\SnapShots\\ATJ\\" + ATcomand + ".jpg");
                Thread.Sleep(2500);
            }



        }
    }
}
