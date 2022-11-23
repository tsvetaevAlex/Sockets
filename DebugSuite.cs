using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using automation;
using simicon.automation;
using automation.Tests.ATSF;

namespace simicon.Debug.automation;

public class DebugSuite
{
    public static string logPrefix = DateTime.Now.ToString("yyyy-MM-dd hh:mm");

    [Test, Order(1)]
    public static void PrepareEvironment()
    {
        Console.WriteLine(Base.GOP() + "<============================[ PrepareEvironment Srarted ]============================");
        ConnectionPointers.InitConnectionPointers("192.168.10.102", "root", "test");
        Sensorapp.Prepare();
    }

    [Test, Order(2)]
    public static void at001()
    {
        Console.WriteLine(Base.GOP() + "<============================[ [Test, Order(2)] / at001 ]=========================");
        Console.WriteLine(Base.GOP() + "<============================[ VerifyATсommand 'AT?' ]============================");
        Camera.VerifyATсommand("AT?");
    }

    ////[Test, Order(3)]
    //public static void at002()
    //{
    //    Camera.VerifyATсommand("ATB");
    //}
    //[Test, Order(4)]
    //public static void at003()
    //{
    //    Camera.VerifyATсommand("ATC");
    //}
    //[Test, Order(5)]
    //public static void at004()
    //{
    //    Camera.VerifyATсommand("ATF");
    //}
    //[Test, Order(6)]
    //public static void at005()
    //{
    //    Camera.VerifyATсommand("ATG");
    //}
    //[Test, Order(7)]
    //public static void at006()
    //{
    //    Camera.VerifyATсommand("ATC");
    //}


    //[Test, Order(4)]
    //[Test, Order(5)]
    //[Test, Order(6)]





}// end of class DebugSuite