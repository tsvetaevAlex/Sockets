using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simicon.automation.Utils;

namespace simicon.automation.Tests.parameterized;

public static class ATF
{
    [Test]
    public static void TestCase00017Parameterized_ATF0()
    {
        Logger.Write("has entered into TestCase00017Parameterized_ATF0()", "TraceRoute");
        Helper.Verify("ATF", "Cam flip: Off", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=0",
            expectedTextContent: "Cam flip: Off",
            TAG: "ATF"
            ));
    }

    [Test]
    public static void TestCase00018Parameterized_ATF1()
    {
        Snapshot.Get("BeforeATF1.jpg", "ATF");
        Logger.Write("has entered into TestCase00018Parameterized_ATF1()", "TraceRoute");
        Helper.Verify("ATF=1", "Cam flip: On", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=1",
            expectedTextContent: "Cam flip: On",
            TAG: "ATF"
        ));
        Snapshot.Get("AfterATF1.jpg", "ATF");
    }

    [Test]
    public static void TestCase00019Parameterized_ATF2()
    {
        Logger.Write("has entered into TestCase0019Parameterized_ATF2()", "TraceRoute");
        Helper.Verify("ATF", "Cam flip: Off", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=2",
            expectedTextContent: "Cam flip: Vert",
            TAG: "ATF"
            ));
    }

    [Test]
    public static void TestCase00020Parameterized_ATF3()
    {
        Snapshot.Get("BeforeATF1.jpg", "ATF");
        Logger.Write("has entered into TestCase00020Parameterized_ATF3()", "TraceRoute");
        Helper.Verify("ATF=1", "Cam flip: On", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=3",
            expectedTextContent: "CCam flip: Mirror",
            TAG: "ATF"
        ));
        Snapshot.Get("AfterATF1.jpg", "ATF");
    }

    [Test]
    public static void TestCase00021Parameterized_ATF4()
    {
        Logger.Write("has entered into TestCase00020Parameterized_ATF4()", "TraceRoute");
        Helper.Verify("ATF=-1", "Cam flip: Mirror", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=-4",
            expectedTextContent: "Cam flip: Off",
                TAG: "ATF"
        ));
    }
}