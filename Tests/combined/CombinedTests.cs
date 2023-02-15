using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simicon.automation.Tests.NonParameterized;
using simicon.automation.Tests.parameterized;
using simicon.automation.Utils;
using Xamarin.Forms;

namespace simicon.automation.Tests.combined;

public static class CombinedTests
{

    [Test, Description("Verify that aTG changeable When_ATA0.")]
    public static void TestCase00024_Combined_VerifyThat_ATG_channableWhen_ATA0()
    {
        parameterizedTests.TestCase0022_Parameterized_SetATA0();
        Logger.Write("has entered into TestCase0001_Parameterized_SetATA0()", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATG = 500",
            expectedTextContent: "GAIN: 480",
            TAG: "ATA0_ATG"
        ));
    }

    [Test, Description("Verify that aTG changeable When_ATA1.")]
    public static void TestCase00025_Combined_VerifyThat_ATG_NotChannableWhen_ATA1()
    {
        parameterizedTests.TestCase0023_Parameterized_SetATA1();
        Logger.Write("has entered into TestCase0001_Parameterized_SetATA0()", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATG = 500",
            expectedTextContent: "ERR",
            TAG: "ATA1_ATG"
        ));
    }


    [Test, Description("Verify that ATS changeable When_ATA1.")]
    public static void TestCase00024_Combined_VerifyThat_ATG_NotChannableWhen_ATA1()
    {
        parameterizedTests.TestCase0023_Parameterized_SetATA1();
        Logger.Write("has entered into TestCase00024Combined_VerifyThat_ATG_channableEhen_ATA1()", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATS=7",
            expectedTextContent: "ERR",
            TAG: "ATA1_ATS"
        ));
    }
    [Test, Description("Verify that ATS changeable When_ATA0.")]
    public static void TestCase00029_Combined_VerifyThat_ATS_channableWhen_ATA0()
    {
        parameterizedTests.TestCase0022_Parameterized_SetATA0();
        Logger.Write("has entered into TestCase0001_Parameterized_SetATA0()", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATS=7",
            expectedTextContent: "SHUT: 7",
            TAG: "ATA_ATS"
        ));
    }
    [Test, Description("Verify that ATS changeable When_ATA1.")]
    public static void TestCase00030_Combined_VerifyThat_ATS_NotChannableWhen_ATA1()
    {
        parameterizedTests.TestCase0023_Parameterized_SetATA1();
        Logger.Write("has entered into TestCase00024Combined_VerifyThat_ATG_channableEhen_ATA1()", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATS=7",
            expectedTextContent: "SHUT: 7",
            TAG: "ATA0_ATS"
        ));
    }


    [Test, Description("Verify that ATL changeable When_ATA0.")]
    public static void TestCase00026_Combined_VerifyThat_ATL_channableWhen_ATA0()
    {//Max exp
        parameterizedTests.TestCase0022_Parameterized_SetATA0();
        Logger.Write("has entered into TestCase0001_Parameterized_SetATA0()", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATL=5",
            expectedTextContent: "Max exp: ",
            TAG: "ATA_ATL"
        ));
    }
    [Test, Description("Verify that ATL changeable When_ATA1.")]
    public static void TestCase00027_Combined_VerifyThat_ATL_NotChannableWhen_ATA1()
    {
        parameterizedTests.TestCase0023_Parameterized_SetATA1();
        Logger.Write("has entered into TestCase00024Combined_VerifyThat_ATG_channableEhen_ATA1()", "TraceRoute");
        Helper.Verify(new RequestDetails
        (
            inputCommand: "ATL=7",
            expectedTextContent: "ERR",
            TAG: "ATA1_ATL"
        ));
    }

}// end of class
