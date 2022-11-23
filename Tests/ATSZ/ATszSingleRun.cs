using simicon.automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automation.Tests.ATSZ;

/// <summary>
/// run single FOCUS comands AT$ SZ; Formst AT$ SF 5000
/// </summary>
public static class ATszSingleRun
{
    public static void Run(int value)
    {
        string ATcomand = "AT$ SZ " + value;
        Helper.StringExecute(ATcomand, ConnectionPointers.CameraSocket);
        Console.WriteLine($"at protocol command to send: {ATcomand}.");
        if (Camera.CreateSnapshot())
            Camera.GetSnapshot("C:\\SnapShots\\ATSZ\\" + ATcomand + ".jpg");
    }
}