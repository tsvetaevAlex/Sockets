using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simicon.automation.Utils;

namespace simicon.automation.Tests.withoutParams.ATF;

public static class ATF
{

    [Test]

    /*
     
            Helper.Verify(new RequestDetails(
            inputCommand: "",
            expectedTextContent: "",
            TAG: "",
            returnContent: false,
            createSnapShot: false
            ));
     */
    public static void atp04VerifyATF()
    {
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        //        Helper.Verify("ATF=0", "ATF\r\nCam flip: ", "ATF");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATF=0",
            expectedTextContent: "Cam flip: ",
            TAG: "ATF"
        ));
    }

    [Test]
    public static void atp05VerifyATF0()
    {
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        //Helper.Verify("ATF", "Cam flip: Off", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF",
            expectedTextContent: "Cam flip: Off",
            TAG: "ATF"
            ));
    }

    [Test]
    public static void atp06VerifyATF1()
    {
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        //Helper.Verify("ATF=1", "Cam flip: On", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF",
            expectedTextContent: "Cam flip: On",
            TAG: "ATF"
        ));
    }

    [Test]
    public static void atp07VerifyATFBelowZeroValues()
    {
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        //Helper.Verify("ATF=-1", "Cam flip: Mirror", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF",
            expectedTextContent: "Cam flip: Off",
            TAG: "ATF"
        ));
    }

    [Test]
    public static void atp08VerifyATF1()
    {
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        //Helper.Verify("ATF=1", "Cam flip: On", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=1",
            expectedTextContent: "Cam flip: On",
            TAG: "ATF"
        ));

    }

    [Test]
    public static void atp10VerifyATF3()
    {
        Snapshot.Get("Before_ATF3", "ATF");
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        //Helper.Verify("ATF=3", "Cam flip: Mirror", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF=3",
            expectedTextContent: "Cam flip: Mirror",
            TAG: "ATF"
        ));
        Snapshot.Get("After_ATF3", "ATF");
    }
}
