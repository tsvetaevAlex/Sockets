using Newtonsoft.Json.Linq;
using simicon.automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automation.Tests.ATJ
{

    /// <summary>
    /// brightness [-200,300]; Format ATJ=150
    /// </summary>
    public static class AtjTestFullRange
    {
        [Test]
        public static void atjTesFullRange([Range(200, 300, 10)] int number)
        {
            
            string ATcomand = "ATJ=" + number;
            Helper.StringExecute(ATcomand, ConnectionPointers.CameraSocket);
            Console.WriteLine($"at protocol command to send: {ATcomand}.");
            if (Camera.CreateSnapshot())
                Camera.GetSnapshot("C:\\SnapShots\\ATJ\\" + ATcomand + ".jpg");

        }
    }
}
