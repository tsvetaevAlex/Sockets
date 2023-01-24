using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simicon.automation.Utils;
using Xamarin.Forms;

namespace simicon.automation.Tests.combined;

public static class CombinedTests
{//Description("Test description here")



    [Test, Description("set ATA value to 0.")]
    public static void atc001_Set_ATA0()
    {
        Logger.Write("has entered into atc001_Set_ATA0()", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATA=0",
            expectedTextContent: "AGC: (0), OFF",
            TAG: "ATA0"
        ));
    }
    [Test, Description("Verify that atg channable when ATA is 0.")]
    public static void atc002_ATG0_ATA0()
    {
        Logger.Write("has entered into VerifyATG(0)", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATG=0",
            expectedTextContent: "GAIN: 0",
            TAG: "ATG"
        ));
    }
    [Test, Description("set ATA value to 1")]
    public static void atc003_Set_ATA1()
    {
        Logger.Write("has entered into atc003_Set_ATA1()", "TraceRoute");
        //string resp = Helper.Send("AT=0", "CombinedTests", true);
        Helper.Verify(new RequestDetails(
            inputCommand: "ATA=1",
            expectedTextContent: "AGC: (1), ON",
            TAG: "ATA1"
            ));

    }
    [Test, Description("Verify that ATG non changeable when ATA is 1. Expeted response is 'ERR'")]
    public static void Atp004_ATG6000_ATA1()
    {
        Logger.Write("has entered into Atp02VerifyATG6000_ATA1()", "TraceRoute");
        Helper.Verify(new RequestDetails
            (
                inputCommand: "ATG=6000",
                expectedTextContent: "ERR",
                TAG: "ATG"
            ));
    }

    [Test, Description("verify that atg displays status when ATA is 1")]
    public static void Atc005_ATG_ATA1()
    {
        Logger.Write("has entered Atp02VerifyATG_ATA1()", "TraceRoute");
        Helper.Verify(new RequestDetails(
            inputCommand: "",
            expectedTextContent: "",
            TAG: "",
            returnFullTextOfContent: false
            ));
    }
}
