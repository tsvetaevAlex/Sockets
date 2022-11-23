using simicon.automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace automation.Tests.ATSF;


/// <summary>
/// run single ZOOM comands AT$ SF; Formst AT$ SF 5000
/// </summary>
public static class ATsfSIngleRun
{

//    public static void Run<T>(T value)
    public static void Run(int value)
    {
        //string ATcomand = "";
        //if (typeof(T) == typeof(string))
        //{
        //    ATcomand = "AT# SF " + value;
        //}
        //if (typeof(T) == typeof(int))
        //{
        //    ATcomand = "AT# SF " + value;
        //}
        string ATcomand = "AT$ SF " + value.ToString();
            Helper.StringExecute(ATcomand, ConnectionPointers.CameraSocket);
        Console.WriteLine($"at protocol command to send: {ATcomand}.");
        if (Camera.CreateSnapshot())
            Camera.GetSnapshot("C:\\SnapShots\\ATSF\\" + ATcomand + ".jpg");
        Thread.Sleep(2000);

    }
}
