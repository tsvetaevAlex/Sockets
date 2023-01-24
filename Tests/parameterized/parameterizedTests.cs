using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simicon.automation.Utils;

namespace simicon.automation.Tests.parameterized;

public static class parameterizedTests
{

    [Test]
    public static void Atp01VerifyATG()
    {
        Snapshot.Get("OnStartATG.jpg", "ATG");
        Logger.Write("TEstCase: VerifyATG(0)", "TraceRoute");
        Helper.Verify(new RequestDetails(
                inputCommand: "ATG=0",
                expectedTextContent: "GAIN: ",
                TAG: "ATG",
                returnFullTextOfContent: false
            ));
    }
    [Test]
    public static void Atp02VerifyATG6000()
    {
        Logger.Write("TEstCase: Atp02VerifyATG6000", "TraceRoute");
        Snapshot.Get("Before_ATG6000On.jpg", "ATG");
        Logger.Write("has entered into VerifyATG(6000)", "TraceRoute");
        Helper.Verify("ATG=6000", "GAIN: 480", "ATG");
        Snapshot.Get("AFter_ATG6000On.jpg", "ATG");
    }

    [Test]
    public static void atp05VerifyATF0()
    {
        Logger.Write("TEstCase: atp05VerifyATF0", "TraceRoute");
        Snapshot.Get("Before_ATF0.jpg", "ATF");
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        Helper.Verify("ATF=0", "Cam flip: Off", "ATF");
        Snapshot.Get("After_ATF0.jpg", "ATF");
    }

    [Test]
    public static void atp06VerifyATF1()
    {
        Logger.Write("TEstCase: atp06VerifyATF1", "TraceRoute");
        Snapshot.Get("Before_ATF1.jpg", "ATF");
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        Helper.Verify("ATF=1", "Cam flip: On", "ATF");
        Snapshot.Get("After_ATF1.jpg", "ATF");

    }

    [Test]
    public static void atp07VerifyATF2()
    {
        Logger.Write("TEstCase: atp07VerifyATF2", "TraceRoute");
        
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=2",
            expectedTextContent: "Cam flip: Vert",
            TAG: "ATC"
        ));
        Snapshot.Get("After_ATF2.jpg", "ATF");
    }
    [Test]
    public static void atp07VerifyATF3()
    {
        Logger.Write("TEstCase: atp07VerifyATF3", "TraceRoute");
        //Snapshot.Get("Before_ATF3.jpg","ATF");
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=3",
            expectedTextContent: "Cam flip: Mirror",
            TAG: "ATf"
        ));
        Snapshot.Get("After_ATF3.jpg","ATF");
    }
    [Test]
    public static void atp07VerifyATFBelowZeroValues()
    {
        Logger.Write("TEstCase: atp07VerifyATFBelowZeroValues", "TraceRoute");
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        //Helper.Verify("ATF-1", "ATF=3\r\nCam flip: Mirror\r\n", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=-1",
            expectedTextContent: "ERR",
            TAG: "ATf"
        ));
    }
}

